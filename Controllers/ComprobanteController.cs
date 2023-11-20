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

    public class ComprobanteController : ControllerBase
    {

        private readonly DentalContext _context;

        private bool CuotaExists(int id)
        {
            return _context.Comprobantes.Any(s => s.Id.Equals(id));
        }

        public ComprobanteController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }


        [HttpGet]
        [Route("ListarComprobantes")]
        public async Task<ActionResult<IEnumerable<Comprobante>>> GetComprobantes()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var _C = await _context.Cuotas.ToListAsync();

            return Ok(_C);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Cuota>> GetCuota(int _id)
        {
            var _C = await _context.Cuotas.FirstOrDefaultAsync(m => m.Id == _id);

            if (_C == null)
            {
                return NotFound();
            }

            return _C;
        }


        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCuota(Cuota C)
        {
            _context.Cuotas.Add(C);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCuota), new { id = C.Id }, C);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCuota(int id, Cuota c)
        {
            if (id != c.Id)
            {
                return BadRequest();
            }

            _context.Entry(c).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuotaExists(id))
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
        public async Task<IActionResult> DeleteCuota(int id)
        {
            var C = await _context.Cuotas.FindAsync(id);
            if (C == null)
            {
                return NotFound();
            }

            _context.Cuotas.Remove(C);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
