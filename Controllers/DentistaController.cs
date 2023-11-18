using System.Threading.Tasks;
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

    public class DentistaController : ControllerBase
    {
        private readonly DentalContext _context;

        private bool DentistaExists(int id)
        {
            return _context.Dentista.Any(e => e.Id.Equals(id));
        }

        public DentistaController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }


        [HttpGet]
        [Route("ListarOdontologos")]
        public async Task<ActionResult<IEnumerable<Dentistum>>> GetDentistas()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var _Dentista = await _context.Usuarios.ToListAsync();

            return Ok(_Dentista);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Dentistum>> GetDentista(int id)
        {
            var _Dentista = await _context.Dentista.FirstOrDefaultAsync(m => m.Id.Equals(id));

            if (_Dentista == null)
            {
                return NotFound();
            }

            return _Dentista;
        }


        [HttpPost]
        public async Task<ActionResult<Dentistum>> CreateDentista(Dentistum _D)
        {
            _context.Dentista.Add(_D);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDentista), new { id = _D.Id }, _D);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDentista(int id, Dentistum D)
        {
            if (id != D.Id)
            {
                return BadRequest();
            }

            _context.Entry(D).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DentistaExists(id))
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
        public async Task<IActionResult> DeleteDentista(int id)
        {
            var D = await _context.Dentista.FindAsync(id);
            if (D == null)
            {
                return NotFound();
            }

            _context.Dentista.Remove(D);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
