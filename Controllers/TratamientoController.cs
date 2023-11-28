using DentalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamientosController : ControllerBase
    {
        private readonly DentalContext _context;

        public TratamientosController(DentalContext context)
        {
            _context = context;
        }

        [HttpGet("ListarTratamientos")]
        public async Task<ActionResult<List<Tratamiento>>> Get()
        {
            try
            {
                var tratamientos = await _context.Tratamientos.ToListAsync();


                return Ok(tratamientos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error interno del servidor: {ex.Message}");
                return Ok(new List<Tratamiento>());
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tratamiento>> Get(int id)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(id);

            if (tratamiento == null)
            {
                return NotFound();
            }

            return Ok(tratamiento);
        }

        [HttpPost("Ingresar")]
        public async Task<ActionResult<List<Tratamiento>>> IngresarTratamiento([FromBody] Tratamiento tratamiento)
                {
                    try
                    {
                        tratamiento.Imagen = Encoding.ASCII.GetBytes("datos_de_la_imagen");
                        tratamiento.FechaCreacion = DateTime.Now;
                        tratamiento.FechaModificacion = DateTime.Now;

                        _context.Tratamientos.Add(tratamiento);
                        await _context.SaveChangesAsync();

                        var tratamientos = await _context.Tratamientos.ToListAsync();

                        return Ok(tratamientos);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error interno del servidor al agregar tratamiento: {ex.Message}");
                        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                    }
                }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Tratamiento tratamiento)
        {
            if (id != tratamiento.Id)
            {
                return BadRequest();
            }

            tratamiento.FechaModificacion = DateTime.Now;

            _context.Entry(tratamiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tratamientos.Any(t => t.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<List<Tratamiento>>> Delete(int id)
        {
            try
            {
                var tratamiento = await _context.Tratamientos.FindAsync(id);

                if (tratamiento == null)
                {
                    return NotFound();
                }

                _context.Tratamientos.Remove(tratamiento);
                await _context.SaveChangesAsync();

                var tratamientosActualizados = await _context.Tratamientos.ToListAsync();

                return Ok(tratamientosActualizados);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error interno del servidor al eliminar tratamiento: {ex.Message}");

                var tratamientosNoActualizados = await _context.Tratamientos.ToListAsync();
                return StatusCode(500, tratamientosNoActualizados);
            }
        }
    }
}
