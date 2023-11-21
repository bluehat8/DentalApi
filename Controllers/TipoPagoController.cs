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

    public class TipoPagoController : ControllerBase
    {
        private readonly DentalContext _context;

        private bool TipoPagoExists(int id)
        {
            return _context.TipoPagos.Any(s => s.Id.Equals(id));
        }

        public TipoPagoController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }


        [HttpGet]
        [Route("ListarTipoPago")]
        public async Task<ActionResult<IEnumerable<TipoPago>>> GetTipoPagos()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var _TipoPago = await _context.TipoPagos.ToListAsync();

            return Ok(_TipoPago);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<TipoPago>> GetTipoPago(int _id)
        {
            var _TipoPago = await _context.TipoPagos.FirstOrDefaultAsync(m => m.Id.Equals(_id));
            
            if (_TipoPago == null)
            {
                return NotFound();
            }

            return _TipoPago;
        }


        [HttpPost]
        public async Task<ActionResult<TipoPago>> CreateTipoPago(TipoPago tc)
        {
            _context.TipoPagos.Add(tc);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipoPago), new { id = tc.Id }, tc);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoPago(int id, TipoPago tc)
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
                if (!TipoPagoExists(id))
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
        public async Task<IActionResult> DeleteTipoPago(int id)
        {
            var _tc = await _context.TipoPagos.FindAsync(id);
            if (_tc == null)
            {
                return NotFound();
            }

            _context.TipoPagos.Remove(_tc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
