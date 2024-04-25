using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.ViewModels;

namespace personapi_dotnet.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiTelefonoController : ControllerBase
    {
        private readonly PersonaDbContext _context;

        public ApiTelefonoController(PersonaDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiTelefono
        [HttpGet]
        public async Task<List<Telefono>> GetTelefonos()
        {
            var personaDbContext = _context.Telefonos.Include(t => t.DuenioNavigation);
            return (await personaDbContext.ToListAsync());
        }  

        // GET: api/ApiTelefono/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Telefono>> GetTelefono(string id)
        {
            var telefono = await _context.Telefonos.FindAsync(id);

            if (telefono == null)
            {
                return NotFound();
            }

            return telefono;
        }

        // PUT: api/ApiTelefono/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelefono(string id, TelefonoViewModel model)
        {
            var telefono = new Telefono()
            {
                Num = model.Num,
                Duenio = model.Duenio,
                Oper = model.Oper
            };

            if (id != telefono.Num)
            {
                return BadRequest();
            }

            _context.Entry(telefono).State = EntityState.Modified;

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

            return Ok(telefono);
        }

        // POST: api/ApiTelefono
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Telefono>> PostTelefono(TelefonoViewModel model)
        {
            if (model != null)
            {
                var telefono = new Telefono()
                {
                    Num = model.Num,
                    Duenio = model.Duenio,
                    Oper = model.Oper
                };
                _context.Telefonos.Add(telefono);
                await _context.SaveChangesAsync();
                return Ok(telefono);
            }
            return BadRequest();
        }

        // DELETE: api/ApiTelefono/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelefono(string id)
        {
            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            _context.Telefonos.Remove(telefono);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool TelefonoExists(string id)
        {
            return _context.Telefonos.Any(e => e.Num == id);
        }
    }
}
