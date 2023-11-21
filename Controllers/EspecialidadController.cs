using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using DentalApi.HelperModels;


namespace DentalApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class EspecialidadController : ControllerBase
    {

        private readonly DentalContext _context;
        private bool EspecialidadExists(int id)
        {
            return _context.Especialidads.Any(s => s.Id.Equals(id));
        }

        public EspecialidadController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }


        [HttpGet]
        [Route("ListarEspecialidades")]
        public async Task<ActionResult<IEnumerable<Especialidad>>> GetEspecialidad()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var _Especialidad = await _context.Especialidads.ToListAsync();

            return Ok(_Especialidad);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Especialidad>> GetEspecialidad(int _id)
        {
            var _Especialidad = await _context.Especialidads.FirstOrDefaultAsync(m => m.Id == _id);

            if (_Especialidad == null)
            {
                return NotFound();
            }

            return _Especialidad;
        }


        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateEspecialidad(Especialidad especialidad)
        {
            _context.Especialidads.Add(especialidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEspecialidad), new { id = especialidad.Id }, especialidad);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEspecialidad(int id,Especialidad E)
        {
            if (id != E.Id)
            {
                return BadRequest();
            }

            _context.Entry(E).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspecialidadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidad(int id)
        {
            var E = await _context.Especialidads.FindAsync(id);
            if (E == null)
            {
                return NotFound();
            }

            _context.Especialidads.Remove(E);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
