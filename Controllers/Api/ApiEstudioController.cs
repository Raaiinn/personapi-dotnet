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
    public class ApiEstudioController : ControllerBase
    {
        private readonly PersonaDbContext _context;

        public ApiEstudioController(PersonaDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiEstudio
        [HttpGet]
        public async Task<List<Estudio>> GetEstudios()
        {
            var personaDbContext = _context.Estudios.Include(e => e.CcPerNavigation).Include(e => e.IdProfNavigation);
            return (await personaDbContext.ToListAsync());
        }

        // GET: api/ApiEstudio/5
        [HttpGet("{IdProf}/{CcPer}")]
        public async Task<ActionResult<Estudio>> GetEstudio(int IdProf, int CcPer)
        {
            var estudio = await _context.Estudios.FindAsync(IdProf, CcPer);
   

            if (estudio == null)
            {
                return NotFound();
            }

            return estudio;
        }

        // PUT: api/ApiEstudio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{IdProf}/{CcPer}")]
        public async Task<IActionResult> PutEstudio(int IdProf, int CcPer, EstudiosViewModel model)
        {
            var estudio = new Estudio()
            {
                IdProf = model.IdProf,
                CcPer = model.CcPer,
                Fecha = model.Fecha,
                Univer = model.Univer
            };

            if (IdProf != estudio.IdProf || CcPer !=estudio.CcPer)
            {
                return BadRequest();
            }

            _context.Entry(estudio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudioExists(IdProf, CcPer))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(estudio);
        }

        // POST: api/ApiEstudio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estudio>> PostEstudio(EstudiosViewModel model)
        {
            var estudio = new Estudio()
            {
                IdProf = model.IdProf,
                CcPer = model.CcPer,
                Fecha = model.Fecha,
                Univer = model.Univer
            };

            _context.Estudios.Add(estudio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EstudioExists(estudio.IdProf, estudio.CcPer))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEstudio", new { id = estudio.IdProf }, estudio);
        }

        // DELETE: api/ApiEstudio/5
        [HttpDelete("{IdProf}/{CcPer}")]
        public async Task<IActionResult> DeleteEstudio(int IdProf, int CcPer)
        {
            var estudio = await _context.Estudios.FindAsync(IdProf, CcPer);
            if (estudio == null)
            {
                return NotFound();
            }

            _context.Estudios.Remove(estudio);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool EstudioExists(int id, int cc)
        {
            return _context.Estudios.Any(e => e.IdProf == id && e.CcPer == cc);
        }
    }
}
