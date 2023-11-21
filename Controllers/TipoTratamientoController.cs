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

    public class TipoTratamientoController : ControllerBase
    {
        private readonly DentalContext _context;

        private bool TipoTratamientoExists(int id)
        {
            return _context.TipoTratamientos.Any(s => s.Id.Equals(id));
        }

        public TipoTratamientoController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }


        [HttpGet]
        [Route("ListarTiposTratamientos")]
        public async Task<ActionResult<IEnumerable<TipoTratamiento>>> GetTiposTratamiento()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var _tm = await _context.Clientes.ToListAsync();

            return Ok(_tm);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTratamiento>> GetTipoTratamiento(int _id)
        {
            var _tm = await _context.TipoTratamientos.FirstOrDefaultAsync(m => m.Id == _id);

            if (_tm == null)
            {
                return NotFound();
            }

            return _tm;
        }


        [HttpPost]
        public async Task<ActionResult<TipoTratamiento>> CreateTipoTratamiento(TipoTratamiento tm)
        {
            _context.TipoTratamientos.Add(tm);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTiposTratamiento), new { id = tm.Id }, tm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoTratamiento(int id, TipoTratamiento tc)
        {
            if (id != tc.Id)
            {
                return BadRequest();
            }

            _context.Entry(tc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoTratamientoExists(id))
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
        public async Task<IActionResult> DeleteTipoTratamiento(int id)
        {
            var tt = await _context.TipoTratamientos.FindAsync(id);
            if (tt == null)
            {
                return NotFound();
            }

            _context.TipoTratamientos.Remove(tt);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
