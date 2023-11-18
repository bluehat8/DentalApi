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

    public class TelefonoController : ControllerBase
    {
        private readonly DentalContext _context;

        private bool TelefonoExists(int id)
        {
            return _context.Telefonos.Any(s => s.Id.Equals(id));
        }

        public TelefonoController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }


        [HttpGet]
        [Route("ListarTelefonos")]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefonos()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var _Telefonos = await _context.Telefonos.ToListAsync();

            return Ok(_Telefonos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Telefono>> GetTelefono(int _id)
        {
            var _Telefono = await _context.Telefonos.FirstOrDefaultAsync(m => m.Id.Equals(_id));

            if (_Telefono == null)
            {
                return NotFound();
            }

            return _Telefono;
        }


        [HttpPost]
        public async Task<ActionResult<Telefono>> CreateTelefono(Telefono T)
        {
            _context.Telefonos.Add(T);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTelefono), new { id = T.Id }, T);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTelefono(int id, Telefono T)
        {
            if (id != T.Id)
            {
                return BadRequest();
            }

            _context.Entry(T).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonoExists(id))
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
        public async Task<IActionResult> DeleteTelefono(int id)
        {
            var T = await _context.Telefonos.FindAsync(id);
            if (T == null)
            {
                return NotFound();
            }

            _context.Telefonos.Remove(T);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
