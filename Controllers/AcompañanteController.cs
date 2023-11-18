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
    public class AcompañanteController : ControllerBase 
    {
        private readonly DentalContext _context;

        private bool AcompañanteExists(int id)
        {
            return _context.Acompañantes.Any(s => s.Id.Equals(id));
        }

        public AcompañanteController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }


        [HttpGet]
        [Route("ListarAcompañante")]
        public async Task<ActionResult<IEnumerable<Acompañante>>> GetAcompañantes()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var _Acompañante = await _context.Acompañantes.ToListAsync();

            return Ok(_Acompañante);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Acompañante>> GetAcompañante(int _id)
        {
            var _Acompañante = await _context.Acompañantes.FirstOrDefaultAsync(m => m.Id == _id);

            if (_Acompañante == null)
            {
                return NotFound();
            }

            return _Acompañante;
        }

        [HttpPost]
        public async Task<ActionResult<Acompañante>> CreateAcompañante(Acompañante acompañante)
        {
            _context.Acompañantes.Add(acompañante);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAcompañante), new { id = acompañante.Id }, acompañante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAcompañante(int id, Acompañante acompañante)
        {
            if (id != acompañante.Id)
            {
                return BadRequest();
            }

            _context.Entry(acompañante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcompañanteExists(id))
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
        public async Task<IActionResult> DeleteAcompañante(int id)
        {
            var A = await _context.Acompañantes.FindAsync(id);
            if (A == null)
            {
                return NotFound();
            }

            _context.Acompañantes.Remove(A);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
