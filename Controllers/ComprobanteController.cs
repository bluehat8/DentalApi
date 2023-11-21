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

        private bool ComprobanteExists(int id)
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

            var _C = await _context.Comprobantes.ToListAsync();

            return Ok(_C);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Comprobante>> GetComprobante(int _id)
        {
            var _C = await _context.Comprobantes.FirstOrDefaultAsync(m => m.Id == _id);

            if (_C == null)
            {
                return NotFound();
            }

            return _C;
        }


        [HttpPost]
        public async Task<ActionResult<Comprobante>> CreateComprobante(Comprobante C)
        {
            _context.Comprobantes.Add(C);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComprobante), new { id = C.Id }, C);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComprobante(int id, Comprobante c)
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
                if (!ComprobanteExists(id))
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
        public async Task<IActionResult> DeleteComprobante(int id)
        {
            var C = await _context.Comprobantes.FindAsync(id);
            if (C == null)
            {
                return NotFound();
            }

            _context.Comprobantes.Remove(C);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
