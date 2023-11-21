using DentalApi.DTOs;
using DentalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                                            Hora = sc.Hora,
                                            TipoCita = sc.TipoCita,
                                            MotivoCita = sc.MotivoCita,
                                            Estado = sc.Estado,
                                        }).ToListAsync();

            return Ok(solicitudesDto);
        }
    }
}
