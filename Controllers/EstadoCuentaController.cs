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

    public class EstadoCuentaController : ControllerBase
    {

        private readonly DentalContext _context;

        private bool EstadoExists(int id)
        {
            return _context.EstadoCuenta.Any(s => s.Id.Equals(id));
        }

        public EstadoCuentaController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }

        [HttpGet]
        [Route("ListarEstadoCuenta")]
        public async Task<ActionResult<IEnumerable<EstadoCuentum>>> GetEstados()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var _EstadosCuenta = await _context.EstadoCuenta.ToListAsync();

            return Ok(_EstadosCuenta);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoCuentum>> GetEstado(int _id)
        {
            var _Estado = await _context.EstadoCuenta.FirstOrDefaultAsync(m => m.Id == _id);

            if (_Estado == null)
            {
                return NotFound();
            }

            return _Estado;
        }


        [HttpPost]
        public async Task<ActionResult<EstadoCuentum>> CreateEstadoCuenta(EstadoCuentum E)
        {
            _context.EstadoCuenta.Add(E);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstado), new { id = E.Id }, E);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstadoCuenta(int id, EstadoCuentum E)
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
                if (!EstadoExists(id))
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
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var E = await _context.EstadoCuenta.FindAsync(id);
            if (E == null)
            {
                return NotFound();
            }

            _context.EstadoCuenta.Remove(E);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
