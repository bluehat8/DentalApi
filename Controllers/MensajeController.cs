using DentalApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajesController : ControllerBase
    {
        private readonly DentalContext _context;
        private bool MessagesExists(int id)
        {
            return _context.Mensajes.Any(e => e.Id == id);
        }
        public MensajesController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }

        [HttpPost]
        [Route("EnviarMensaje")]
        public async Task<ActionResult<Mensaje>> EnviarMensaje(Mensaje mensaje)
        {

            if (mensaje.Usuarioremitente == mensaje.Usuariordestinatario)
            {
                return BadRequest("Los IDs de usuario no pueden ser iguales.");
            }

            bool remitenteExiste = await _context.Usuarios.AnyAsync(u => u.Id == mensaje.Usuarioremitente);
            if (!remitenteExiste)
            {
                return NotFound($"El usuario remitente con ID {mensaje.Usuarioremitente} no existe.");
            }

            bool destinatarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == mensaje.Usuariordestinatario);
            if (!destinatarioExiste)
            {
                return NotFound($"El usuario destinatario con ID {mensaje.Usuariordestinatario} no existe.");
            }

            mensaje.FechaCreacion = DateTime.Now;
            //mensaje.EstadoDestinatario = (int)Constants.DentalMessageStatusEnum.pendiente;
            //mensaje.EstadoRemitente = (int)Constants.DentalMessageStatusEnum.leido;
            mensaje.FechaModificacion = DateTime.Now;


            _context.Mensajes.Add(mensaje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ObtenerConversacion", new { usuarioId = mensaje.Usuarioremitente, otroUsuarioId = mensaje.Usuariordestinatario }, mensaje);
        }



        [HttpGet]
        [Route("ObtenerUsuariosConversaciones")]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerUsuariosConversaciones(int usuarioId)
        {
            bool usuarioActualExiste = await _context.Usuarios.AnyAsync(u => u.Id == usuarioId);
            if (!usuarioActualExiste)
            {
                return NotFound("El usuario actual no existe.");
            }

            var remitentes = await _context.Mensajes
                .Where(m => m.Usuariordestinatario == usuarioId)
                .Select(m => m.UsuarioremitenteNavigation)
                .Distinct()
                .ToListAsync();

            var destinatarios = await _context.Mensajes
                .Where(m => m.Usuarioremitente == usuarioId)
                .Select(m => m.UsuariordestinatarioNavigation)
                .Distinct()
                .ToListAsync();

            var usuarios = remitentes.Concat(destinatarios).Distinct().ToList();

            return usuarios;
        }

        [HttpGet]
        [Route("ObtenerUsuariosConRoles/{usuarioId}")]
        public async Task<ActionResult<List<Usuario>>> ObtenerUsuariosDoctoresAsistentes(int usuarioId)
        {
            bool usuarioActualExiste = await _context.Usuarios.AnyAsync(u => u.Id == usuarioId);
            if (!usuarioActualExiste)
            {
                return NotFound("El usuario actual no existe.");
            }

            var usuarios = await _context.Usuarios.ToListAsync();
            // Filtrar usuarios por roles específicos ("Asistente")
            var usuariosConRoles = usuarios.Where(u => u.Rol == (Int32)Constants.DentalRole.asistente).ToList();

            return usuariosConRoles;
        }

        [HttpGet]
        [Route("Conversacion")]
        public async Task<ActionResult<List<Mensaje>>> ObtenerConversacion(int usuarioId, int otroUsuarioId)
        {
            if (usuarioId == otroUsuarioId)
            {
                return BadRequest("Los IDs de usuario no pueden ser iguales.");
            }

            bool usuarioActualExiste = await _context.Usuarios.AnyAsync(u => u.Id == usuarioId);
            if (!usuarioActualExiste)
            {
                return NotFound("El usuario actual no existe.");
            }

            bool otroUsuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == otroUsuarioId);
            if (!otroUsuarioExiste)
            {
                return NotFound("El otro usuario no existe.");
            }

            var mensajes = await _context.Mensajes
                .Where(m => (m.Usuarioremitente == usuarioId && m.Usuariordestinatario == otroUsuarioId) || (m.Usuarioremitente == otroUsuarioId && m.Usuariordestinatario == usuarioId))
                .ToListAsync();

            return mensajes;
        }

        [HttpPut("{id}/LeerMensaje")]
        public async Task<IActionResult> MarcarMensajeLeido(int id)
        {
            var mensaje = await _context.Mensajes.FindAsync(id);
            if (mensaje == null)
            {
                return NotFound();
            }
            //mensaje.EstadoDestinatario = (Int32)Constants.DentalMessageStatusEnum.leido;

            mensaje.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
