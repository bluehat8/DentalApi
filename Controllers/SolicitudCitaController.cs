using DentalApi.DTOs;
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
                                        }).ToListAsync();

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

                DateTime fecha = DateTime.ParseExact(solicitudDto.Hora, "HH:mm", CultureInfo.InvariantCulture);
                TimeSpan hora = fecha.TimeOfDay;
                solicitud.Hora = hora;

                solicitud.Estado = solicitudDto.Estado;
                solicitud.FechaCreacion = DateTime.Now;
                solicitud.FechaModificacion = DateTime.Now;
                solicitud.Activo = true;
                _dbContext.SolicitudCita.Add(solicitud);
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
                // Manejar el error de alguna manera, posiblemente devolver un mensaje de error en la respuesta
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }




    }
}

