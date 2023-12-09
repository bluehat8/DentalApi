using DentalApi.DTOs;
using DentalApi.HelperModels;
using DentalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DentalApi.Controllers
{
    [ApiController]
    [Route("api/citas")]
    public class CitasController : ControllerBase
    {
        private readonly DentalContext _dbContext;

        public CitasController(DentalContext dbContext)
        {
            _dbContext = dbContext;
        }

   

        [HttpGet("solicitudes-cita/{usuarioId}")]
        public async Task<IActionResult> GetSolicitudesCitaByUsuarioId(int usuarioId)
        {
            // Validar usuario
            var usuario = await _dbContext.Usuarios
                                      .Where(u => u.Id == usuarioId && u.Rol == 1)
                                      .FirstOrDefaultAsync();

            if (usuario == null)
                return BadRequest("Usuario no encontrado o no es cliente");

            var solicitudesDto = await (from c in _dbContext.Clientes
                                        join sc in _dbContext.SolicitudCita
                                        on c.Id equals sc.PacienteId
                                        where c.Usuario == usuarioId
                                        select new SolicitudCitumDto
                                        {
                                            Id = sc.Id,
                                            Fecha = sc.Fecha,
                                            Hora = sc.Hora.ToString(),
                                            TipoCita = sc.TipoCita,
                                            MotivoCita = sc.MotivoCita,
                                            Estado = sc.Estado,
                                            EstadoValue = HelpherMethods.GetStatusText(sc.Estado)
                                        }).ToListAsync();

            return Ok(solicitudesDto);
        }



        [HttpGet("getSolicitudesCita")]
        public async Task<IActionResult> GetSolicitudesCita()
        {
            var solicitudesDto = await (from sc in _dbContext.SolicitudCita
                                        join c in _dbContext.Clientes on sc.PacienteId equals c.Id
                                        join u in _dbContext.Usuarios on c.Usuario equals u.Id
                                        select new SolicitudCitumDto
                                        {
                                            Userid = u.Id,
                                            PacienteId = c.Id,
                                            NombrePaciente = u.Nombre,
                                            ApellidoPaciente = u.Apellidos,
                                            Id = sc.Id,
                                            Fecha = sc.Fecha,
                                            Hora = sc.Hora.ToString(),
                                            TipoCita = sc.TipoCita,
                                            MotivoCita = sc.MotivoCita,
                                            Estado = sc.Estado,
                                            EstadoValue = HelpherMethods.GetStatusText(sc.Estado)
                                        }).Where(x => x.Estado != (Int32)Constants.DentalSolicitudCitaStatus.cancelada).ToListAsync();

            return Ok(solicitudesDto);
        }


        [HttpPost]
        public async Task<IActionResult> SolicitarCita([FromBody] SolicitudCitumDto solicitudDto)
        {
            
                var cliente = await _dbContext.Clientes.FirstOrDefaultAsync(c => c.Usuario == solicitudDto.Userid);
                if (cliente == null)
                {
                    return NotFound("El cliente no existe");
                }

                var solicitud = new SolicitudCitum
                {
                    PacienteId = cliente.Id,
                    Fecha = solicitudDto.Fecha,
                    MotivoCita = solicitudDto.MotivoCita,
                    TipoCita = solicitudDto.TipoCita
                };

                if (TimeSpan.TryParseExact(solicitudDto.Hora, "HH:mm", CultureInfo.InvariantCulture, out TimeSpan hora))
                {
                    solicitud.Hora = hora;
                }
                else
                {
                    
                    solicitud.Hora = TimeSpan.Zero;
                }

            solicitud.Estado = (Int32)Constants.DentalSolicitudCitaStatus.pendiente;
                solicitud.FechaCreacion = DateTime.Now;
                solicitud.FechaModificacion = DateTime.Now;
                solicitud.Activo = true;
                _dbContext.SolicitudCita.Add(solicitud);
                await _dbContext.SaveChangesAsync();


                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(c => c.Id == solicitudDto.Userid);

                Notificacione notificacion = new Notificacione()
                {
                    Usuario = solicitudDto.Userid,
                    Asunto = "Nueva solicitud de cita",
                    Fecha = solicitudDto.Fecha,
                    Cuerpo = $"{usuario.Nombre} {usuario.Apellidos} ha solicitado una nueva cita el {solicitudDto.Fecha}",
                    Estado = (Int32)Constants.DentalNotificationsStatusEnum.pendiente,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                };   
            
                _dbContext.Notificaciones.Add(notificacion);
                await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Solicitud enviada correctamente"});
            }


        [HttpPut("updateSolicitud/{id}")]
        public async Task<IActionResult> UpdateSolicitudCita(int id, [FromBody] SolicitudCitumDto solicitudDto)
        {

            var solicitudExiste = await _dbContext.SolicitudCita.FindAsync(id);

            if (solicitudExiste == null)
            {
                return NotFound();
            }


            solicitudExiste.Estado = solicitudExiste.Estado;
            solicitudExiste.MotivoCita = solicitudDto.MotivoCita;
            solicitudExiste.TipoCita = solicitudDto.TipoCita;
            solicitudExiste.Fecha = solicitudDto.Fecha;

            DateTime fecha = DateTime.ParseExact(solicitudDto.Hora, "HH:mm", CultureInfo.InvariantCulture);
            TimeSpan hora = fecha.TimeOfDay;
            solicitudExiste.Hora = hora;

            solicitudExiste.FechaModificacion = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpPut("cancelarCita/{id}")]
        public async Task<IActionResult> CancelarCita(int id)
        {
            try
            {
                var solicitudCita = await _dbContext.SolicitudCita.FindAsync(id);

                if (solicitudCita == null)
                {
                    return NotFound();
                }

                solicitudCita.Estado = (Int32)Constants.DentalSolicitudCitaStatus.cancelada;
                solicitudCita.FechaModificacion = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("aceptarCita/{id}")]
        public async Task<IActionResult> AprobarCita(int id)
        {
            try
            {
                var solicitudCita = await _dbContext.SolicitudCita.FindAsync(id);

                if (solicitudCita == null)
                {
                    return NotFound();
                }

                solicitudCita.Estado = (Int32)Constants.DentalSolicitudCitaStatus.aceptada;
                solicitudCita.FechaModificacion = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                var usuario = await _dbContext.SolicitudCita
                .Where(s => s.Id == id)
                .Join(
                    _dbContext.Clientes,
                    solicitud => solicitud.PacienteId,
                    cliente => cliente.Id,
                    (solicitud, cliente) => new { solicitud, cliente }
                )
                .Join(
                    _dbContext.Usuarios,
                    sc => sc.cliente.Usuario,
                    usuario => usuario.Id,
                    (sc, usuario) => usuario
                )
                .FirstOrDefaultAsync();

                Notificacione notificacion = new Notificacione()
                {
                    Usuario = usuario.Id,
                    Asunto = "Solicitud de Cita Aceptada",
                    Fecha = solicitudCita.Fecha,
                    Cuerpo = $"Su solicitud de cita por el motivo de '{solicitudCita.MotivoCita}' ha sido aceptada para el día {solicitudCita.Fecha}. Lo estaremos esperando",
                    Estado = (Int32)Constants.DentalNotificationsStatusEnum.pendiente,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                };

                _dbContext.Notificaciones.Add(notificacion);
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("RechazarCita/{id}")]
        public async Task<IActionResult> RechazarCita(int id)
        {
            try
            {
                var solicitudCita = await _dbContext.SolicitudCita.FindAsync(id);

                if (solicitudCita == null)
                {
                    return NotFound();
                }

                solicitudCita.Estado = (Int32)Constants.DentalSolicitudCitaStatus.rechazada;
                solicitudCita.FechaModificacion = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                var usuario = await _dbContext.SolicitudCita
                .Where(s => s.Id == id)
                .Join(
                    _dbContext.Clientes,
                    solicitud => solicitud.PacienteId,
                    cliente => cliente.Id,
                    (solicitud, cliente) => new { solicitud, cliente }
                )
                .Join(
                    _dbContext.Usuarios,
                    sc => sc.cliente.Usuario,
                    usuario => usuario.Id,
                    (sc, usuario) => usuario
                )
                .FirstOrDefaultAsync();

                Notificacione notificacion = new Notificacione()
                {
                    Usuario = usuario.Id,
                    Asunto = "Solicitud rechazada",
                    Fecha = solicitudCita.Fecha,
                    Cuerpo = $"Su solicitud de cita por el motivo de '{solicitudCita.MotivoCita}' ha sido rechazada para el día {solicitudCita.Fecha}. Lo sentimos",
                    Estado = (Int32)Constants.DentalNotificationsStatusEnum.pendiente,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                };

                _dbContext.Notificaciones.Add(notificacion);
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



    }
}

