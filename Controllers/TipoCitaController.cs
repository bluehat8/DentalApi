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

    public class TipoCitaController : ControllerBase
    {
        private readonly DentalContext _context;

        private bool TipoCitaExists(int id)
        {
            return _context.TipoCita.Any(s => s.Id.Equals(id));
        }

        public TipoCitaController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }


        [HttpGet]
        [Route("ListarTipocita")]
        public async Task<ActionResult<IEnumerable<TipoCitum>>> GetTipoCita()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var _TipoCita = await _context.TipoCita.ToListAsync();

            return Ok(_TipoCita);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TipoCitum>> GetTipoCita(int _id)
        {
            var _tc = await _context.TipoCita.FirstOrDefaultAsync(m => m.Id == _id);

            if (_tc == null)
            {
                return NotFound();
            }

            return _tc;
        }


        [HttpPost]
        public async Task<ActionResult<TipoCitum>> CreateTipoCita(TipoCitum tc)
        {
            _context.TipoCita.Add(tc);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipoCita), new { id = tc.Id }, tc);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoCita(int id,TipoCitum tc)
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
                if (!TipoCitaExists(id))
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
        public async Task<IActionResult> DeleteTipoCita(int id)
        {
            var tc = await _context.TipoCita.FindAsync(id);
            if (tc == null)
            {
                return NotFound();
            }

            _context.TipoCita.Remove(tc);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
