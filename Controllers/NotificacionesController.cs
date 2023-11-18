using DentalApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DentalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionesController : ControllerBase
    {
        private readonly DentalContext _context;

        public NotificacionesController(DentalContext context)
        {
            _context = context;
        }

        [HttpGet("ListarNotificaciones/{usuarioId}")]
        public ActionResult<IEnumerable<Notificacione>> ListarNotificacionesPorUsuario(int usuarioId)
        {
            var notificaciones = _context.Notificaciones
                .Where(n => n.Usuario == usuarioId)
                .ToList();

            return Ok(notificaciones);
        }


        [HttpPost("EnviarNotificacion")]
        public async Task<ActionResult<Notificacione>> EnviarNotificacion(Notificacione notificacion)
        {
            notificacion.FechaCreacion = DateTime.Now;
            notificacion.FechaModificacion = DateTime.Now;
            //**notificacion.Estado = (Int32)Constants.DentalNotificationsStatusEnum.pendiente;
            _context.Notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ObtenerNotificacion", new { id = notificacion.Id }, notificacion);
        }

        [HttpPost("EnviarNotificacionCita")]
        public async Task<ActionResult<Notificacione>> EnviarNotificacionCita(int usuarioId, DateTime fechaCita, bool aprobada)
        {
            string asunto = aprobada ? "Solicitud de cita aprobada" : "Solicitud de cita rechazada";
            string cuerpo = aprobada
                ? $"Su solicitud de cita para el {fechaCita.ToString("dd/MM/yyyy")} ha sido aprobada."
                : $"Lamentamos informarle que su solicitud de cita para el {fechaCita.ToString("dd/MM/yyyy")} " +
                $"ha sido rechazada.";

            var notificacion = CrearNotificacion(usuarioId, asunto, cuerpo);

            _context.Notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ObtenerNotificacion", new { id = notificacion.Id }, notificacion);
        }

        private Notificacione CrearNotificacion(int usuarioId, string asunto, string cuerpo)
        {
            return new Notificacione
            {
                Usuario = usuarioId,
                Fecha = DateTime.Now,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Asunto = asunto,
                Cuerpo = cuerpo,
                //**Estado = (Int32)Constants.DentalNotificationsStatusEnum.pendiente;
            };
        }

    }
}
