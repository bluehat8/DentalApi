using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using DentalApi.HelperModels;
using DentalApi.Cifrado;
using DentalApi.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DentalApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DentalContext _context;

        public UsuariosController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }

        // GET: api/Usuarios
        [HttpGet]
        [Route("ListarUsuarios")]
        public async Task<ActionResult<IEnumerable<Models.Usuario>>> GetUsuarios()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var usuarios = await _context.Usuarios.Where(s =>s.Activo)
                .ToListAsync();

            return Ok(usuarios);
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.Include(u => u.TelefonoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            UsuarioDto usuarioDto = new UsuarioDto()
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                Username = usuario.Username,
                Telefono = usuario.TelefonoNavigation.Numero,
                Contraseña = usuario.Contraseña,
                Correo = usuario.Correo,
                FechaCreacion = usuario.FechaCreacion,
                FechaModificacion = usuario.FechaModificacion,
                FechaNacimiento = usuario.FechaNacimiento.ToString("yyyy-MM-dd"),
                Cedula = usuario.Cedula,
                Rol = usuario.Rol,
                Activo = usuario.Activo,
            };

            return usuarioDto;
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Models.Usuario>> CreateUsuario(Usuario usuario)
        {
            usuario.Contraseña = Encriptado.EncryptPassword(usuario.Contraseña);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, UsuarioDto usuario)
        {
            var existingUsuario = await _context.Usuarios
                .Include(u => u.TelefonoNavigation)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (existingUsuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }

            var user = await _context.Usuarios
                .Include(u => u.Dentista)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user != null && user.Dentista != null && user.Dentista.Any())
            {
                var dentistaDelUsuario = user.Dentista.FirstOrDefault(d => d.Usuario == id);

                if (dentistaDelUsuario != null)
                {
                    dentistaDelUsuario.Especialidad = usuario.Especialidad;

                    await _context.SaveChangesAsync();
                }
            }

            // Actualizar solo las propiedades que se envían
            existingUsuario.TelefonoNavigation.Numero = usuario.Telefono ?? existingUsuario.TelefonoNavigation.Numero;
            existingUsuario.Apellidos = usuario.Apellidos ?? existingUsuario.Apellidos;
            existingUsuario.Nombre = usuario.Nombre ?? existingUsuario.Nombre;
            existingUsuario.Correo = usuario.Correo ?? existingUsuario.Correo;
            existingUsuario.Cedula = usuario.Cedula ?? existingUsuario.Cedula;
            //*existingUsuario.FechaNacimiento = DateTime.Parse(usuario.FechaNacimiento.ToString());
            existingUsuario.FechaModificacion = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Usuario actualizado con éxito." });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound(new { message = "Usuario no encontrado." });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Error interno del servidor al actualizar el usuario." });
            }
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //****Retorna objeto Usuario para manipular del otro lado y otorgar permisos para acceder a páginas***//
        [HttpPost]
        [Route("api/Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _context.Usuarios
                            .Include(u => u.TelefonoNavigation) 
                            .FirstOrDefaultAsync(x => (x.Username == model.UsernameOrEmail || x.Correo == model.UsernameOrEmail) && x.Contraseña == model.Password);

                if (user != null)
                {

                    UsuarioDto usuarioDto = new UsuarioDto() { 
                        Id= user.Id,
                        Nombre = user.Nombre,
                        Apellidos = user.Apellidos,
                        Username = user.Username,
                        Telefono = user.TelefonoNavigation.Numero,
                        Contraseña = user.Contraseña,
                        Correo = user.Correo,
                        FechaCreacion = user.FechaCreacion,
                        FechaModificacion = user.FechaModificacion,
                        FechaNacimiento = user.FechaNacimiento.ToString("yyyy-MM-dd"),
                        Cedula = user.Cedula,
                        Rol = user.Rol,
                        Activo = user.Activo,
                    };

                    return Ok(new { response = usuarioDto, message = "Inicio de sesión exitoso" });
                }
                else
                {
                    return BadRequest(new {response = user, message = "Credenciales incorrectas o cuenta no registrada" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
