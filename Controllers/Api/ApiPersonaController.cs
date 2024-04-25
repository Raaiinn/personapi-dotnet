using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPersonaController(PersonaDbContext context) : ControllerBase
    {
        private readonly PersonaDbContext _context = context;
        [HttpGet]
        public async Task<List<Persona>> GetPersonas() {
            var personaDbContext = _context.Personas.Include(e => e.Estudios).Include(e => e.Telefonos);
            return (await personaDbContext.ToListAsync());
        }

        [HttpGet("{cedula}")]
        public async Task<ActionResult<Persona>> GetPersona(int cedula)
        {
            var persona = await _context.Personas.FindAsync(cedula);

            if (persona == null)
            {
                return NotFound();
            }
            return persona;
        }

        [HttpPost]
        public async Task<IActionResult> PostPersona(Persona persona)
        {
            if (persona != null) 
            {         
                _context.Personas.Add(persona);
                await _context.SaveChangesAsync();
                return Ok(persona);
            }
            return BadRequest();
        }

        [HttpPut("{cedula}")]
        public async Task<IActionResult> PutPersona(int cedula, Persona persona)
        {
            if (cedula != persona.Cc)
            {
                return BadRequest();
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(cedula))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(persona);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool PersonaExists(int cedula)
        {
            return _context.Personas.Any(e => e.Cc == cedula);
        }
    }
}
