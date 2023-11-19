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

    public class PagosController : ControllerBase
    {

        private readonly DentalContext _context;
        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(s => s.Id.Equals(id));
        }

        public PagosController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }

        [HttpGet]
        [Route("ListarPagos")]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var _Pagos = await _context.Clientes.ToListAsync();

            return Ok(_Pagos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int _id)
        {
            var _Pago = await _context.Pagos.FirstOrDefaultAsync(m => m.Id == _id);

            if (_Pago == null)
            {
                return NotFound();
            }

            return _Pago;
        }


        [HttpPost]
        public async Task<ActionResult<Pago>> CreatePago(Pago P)
        {
            _context.Pagos.Add(P);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetPago), new { id = P.Id }, P);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePago(int id, Pago p)
        {
            if (id != p.Id)
            {
                return BadRequest();
            }

            _context.Entry(p).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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
        public async Task<IActionResult> DeletePago(int id)
        {
            var _P = await _context.Pagos.FindAsync(id);
            if (_P == null)
            {
                return NotFound();
            }

            _context.Pagos.Remove(_P);
            await _context.SaveChangesAsync();

            return NoContent();
        }




    }
}
