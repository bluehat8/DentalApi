using DentalApi.DTOs;
using DentalApi.HelperModels;
using DentalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace DentalApi.Controllers
{
    [ApiController]
    [Route("api/HistorialClinico")]
    public class HistorialClinicoController : ControllerBase
    {
        private readonly DentalContext _dbContext;

        public HistorialClinicoController(DentalContext dbContext)
        {
            _dbContext = dbContext;
        }


        // GET api/historialclinico/{idUsuario}
        [HttpGet("obtenerHistorialClinico/{idUsuario}")]
        public async Task<IActionResult> GetHistorialClinico(int idUsuario)
        {
            try
            {
                var historialClinico = _dbContext.HistorialClinicos.FirstOrDefault(h => h.PacienteNavigation.Usuario == idUsuario);

                if (historialClinico == null)
                {
                    return Ok(new HistorialClinico()); // Devolver 404 si no se encuentra el historial clínico
                }

                return Ok(historialClinico); // Devolver el historial clínico encontrado
            }
            catch (Exception ex)
            {
                // Manejar la excepción y devolver una respuesta de error apropiada
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }
    }
}
