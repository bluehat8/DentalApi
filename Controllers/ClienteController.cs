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

    public class ClienteController : ControllerBase
    {

        private readonly DentalContext _context;

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(s => s.Id.Equals(id));
        }

        public ClienteController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }

      
        [HttpGet]
        [Route("ListarClientes")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var _Clientes = await _context.Clientes.ToListAsync();

            return Ok(_Clientes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCLiente(int _id)
        {
            var _Cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == _id);

            if (_Cliente == null)
            {
                return NotFound();
            }

            return _Cliente;
        }


        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente(Cliente C)
        {
            _context.Clientes.Add(C);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCLiente), new { id = C.Id }, C);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, Cliente c)
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
                if (!ClienteExists(id))
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
        public async Task<IActionResult> DeleteCLiente(int id)
        {
            var C = await _context.Clientes.FindAsync(id);
            if (C == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(C);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
