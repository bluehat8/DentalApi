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
    public class AsistenteController : ControllerBase
    {
        private readonly DentalContext _context;
        private bool AsistenteExists(int id)
        {
            return _context.Asistentes.Any(e => e.Id == id);
        }
        public AsistenteController(DentalContext context)
        {
            _context = context;
            _context.setConnectionString();
        }


        [HttpGet]
        [Route("ListarAsistentes")]
        public async Task<ActionResult<IEnumerable<Asistente>>> GetAsistentes()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var asistentes = await _context.Asistentes.ToListAsync();

            return Ok(asistentes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Asistente>> GetAsistente(int _id)
        {
            var _Asistente = await _context.Asistentes.FirstOrDefaultAsync(m => m.Id == _id);

            if (_Asistente == null)
            {
                return NotFound();
            }

            return _Asistente;
        }


    }
}
