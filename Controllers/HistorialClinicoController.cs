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

        // GET api/historialclinico/obtenerHistorialClinicoPorCedula/{cedula}
        [HttpGet("obtenerHistorialClinicoPorCedula/{cedula}")]
        public IActionResult GetHistorialClinicoPorCedula(string cedula)
        {
            try
            {
                // Buscar el historial clínico por cédula
                // Buscar el historial clínico por cédula
                var historialClinicoDto = _dbContext.HistorialClinicos
                    .Where(h => h.PacienteNavigation.UsuarioNavigation.Cedula == cedula)
                    .Select(h => new HistorialClinicoDto
                    {
                        Id = h.Id,
                        nombrePaciente = h.PacienteNavigation.UsuarioNavigation.Nombre + h.PacienteNavigation.UsuarioNavigation.Apellidos,
                        Paciente = h.Paciente,
                        Observaciones = h.Observaciones,
                        EnfermedadPadre = h.EnfermedadPadre,
                        EnfermedadMadre = h.EnfermedadMadre,
                        Deporte = h.Deporte,
                        MalestarDeporte = h.MalestarDeporte,
                        Diabetico = h.Diabetico,
                        ProblemaCardiaco = h.ProblemaCardiaco,
                        ProblemaRenales = h.ProblemaRenales,
                        PresionAlta = h.PresionAlta,
                        Epileptico = h.Epileptico,
                        Operado = h.Operado,
                        DetalleOperacion = h.DetalleOperacion,
                        Caries = h.Caries,
                        EstadoBucal = h.EstadoBucal,
                        OtrasEnfermedades = h.OtrasEnfermedades,
                        SangraEncimas = h.SangraEncimas,
                        DientesFracturados = h.DientesFracturados,
                        Embarazado = h.Embarazado,
                        FechaCreacion = h.FechaCreacion,
                        FechaModificacion = h.FechaModificacion
                    })
                    .FirstOrDefault();

                if (historialClinicoDto == null)
                {
                    return Ok(new HistorialClinico()); // Devolver 404 si no se encuentra el historial clínico
                }

                return Ok(historialClinicoDto); // Devolver el historial clínico encontrado
            }
            catch (Exception ex)
            {
                // Manejar la excepción y devolver una respuesta de error apropiada
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        // POST api/historialclinico/crearModificarHistorialClinico
        [HttpPost("crearModificarHistorialClinico")]
        public async Task<IActionResult> CrearModificarHistorialClinico([FromBody] HistorialClinicoDto historialClinicoDto)
        {
            try
            {
                // Verificar si el historial clínico ya existe
                var historialClinicoExistente = _dbContext.HistorialClinicos
                    .FirstOrDefault(h => h.Paciente == historialClinicoDto.Paciente);

                if (historialClinicoExistente == null)
                {
                    // Crear nuevo historial clínico
                    var nuevoHistorialClinico = new HistorialClinico
                    {
                        Paciente = historialClinicoDto.Paciente,
                        Observaciones = historialClinicoDto.Observaciones,
                        EnfermedadPadre = historialClinicoDto.EnfermedadPadre,
                        EnfermedadMadre = historialClinicoDto.EnfermedadMadre,
                        Deporte = historialClinicoDto.Deporte,
                        MalestarDeporte = historialClinicoDto.MalestarDeporte,
                        Diabetico = historialClinicoDto.Diabetico,
                        ProblemaCardiaco = historialClinicoDto.ProblemaCardiaco,
                        ProblemaRenales = historialClinicoDto.ProblemaRenales,
                        PresionAlta = historialClinicoDto.PresionAlta,
                        Epileptico = historialClinicoDto.Epileptico,
                        Operado = historialClinicoDto.Operado,
                        DetalleOperacion = historialClinicoDto.DetalleOperacion,
                        Caries = historialClinicoDto.Caries,
                        EstadoBucal = historialClinicoDto.EstadoBucal,
                        OtrasEnfermedades = historialClinicoDto.OtrasEnfermedades,
                        SangraEncimas = historialClinicoDto.SangraEncimas,
                        DientesFracturados = historialClinicoDto.DientesFracturados,
                        Embarazado = historialClinicoDto.Embarazado,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    _dbContext.HistorialClinicos.Add(nuevoHistorialClinico);
                }
                else
                {
                    // Modificar historial clínico existente
                    historialClinicoExistente.Observaciones = historialClinicoDto.Observaciones;
                    historialClinicoExistente.EnfermedadPadre = historialClinicoDto.EnfermedadPadre;
                    historialClinicoExistente.EnfermedadMadre = historialClinicoDto.EnfermedadMadre;
                    historialClinicoExistente.Deporte = historialClinicoDto.Deporte;
                    historialClinicoExistente.MalestarDeporte = historialClinicoDto.MalestarDeporte;
                    historialClinicoExistente.Diabetico = historialClinicoDto.Diabetico;
                    historialClinicoExistente.ProblemaCardiaco = historialClinicoDto.ProblemaCardiaco;
                    historialClinicoExistente.ProblemaRenales = historialClinicoDto.ProblemaRenales;
                    historialClinicoExistente.PresionAlta = historialClinicoDto.PresionAlta;
                    historialClinicoExistente.Epileptico = historialClinicoDto.Epileptico;
                    historialClinicoExistente.Operado = historialClinicoDto.Operado;
                    historialClinicoExistente.DetalleOperacion = historialClinicoDto.DetalleOperacion;
                    historialClinicoExistente.Caries = historialClinicoDto.Caries;
                    historialClinicoExistente.EstadoBucal = historialClinicoDto.EstadoBucal;
                    historialClinicoExistente.OtrasEnfermedades = historialClinicoDto.OtrasEnfermedades;
                    historialClinicoExistente.SangraEncimas = historialClinicoDto.SangraEncimas;
                    historialClinicoExistente.DientesFracturados = historialClinicoDto.DientesFracturados;
                    historialClinicoExistente.Embarazado = historialClinicoDto.Embarazado;
                    historialClinicoExistente.FechaModificacion = DateTime.Now;
                }

                await _dbContext.SaveChangesAsync();

                return Ok(); // Devolver éxito
            }
            catch (Exception ex)
            {
                // Manejar la excepción y devolver una respuesta de error apropiada
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

}

