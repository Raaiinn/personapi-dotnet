using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Controllers.Api;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.ViewModels;

namespace personapi_dotnet.Controllers
{
    public class EstudiosController : Controller
    {
        private readonly PersonaDbContext _context;
        private readonly ApiEstudioController _controller;

        public EstudiosController(PersonaDbContext context, ApiEstudioController controller)
        {
            _context = context;
            _controller = controller;
        }

        // GET: Estudios
        public async Task<IActionResult> Index()
        {
            return View(await _controller.GetEstudios());
        }

        // GET: Estudios/Details/5
        [HttpGet("{IdProf}/{CcPer}")]
        public async Task<IActionResult> Details(int? IdProf, int? CcPer)
        {
            if (IdProf == null || CcPer == null)
            {
                return NotFound();
            }

            var estudio = await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .FirstOrDefaultAsync(m => m.IdProf == IdProf && m.CcPer == CcPer);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // GET: Estudios/Create
        [HttpGet("Estudios/Create")]
        public IActionResult Create()
        {
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc");
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id");
            return View();
        }

        // POST: Estudios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Estudios/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProf,CcPer,Fecha,Univer")] EstudiosViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _controller.PostEstudio(model);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", model.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", model.IdProf);
            return View(model);
        }

        // GET: Estudios/Edit/5
        [HttpGet("edit/{CcPer}/{IdProf}")]
        public async Task<IActionResult> Edit(int? IdProf, int? CcPer)
        {
            if (IdProf == null || CcPer == null)
            {
                return NotFound();
            }

            var estudio = await _context.Estudios.FindAsync(IdProf, CcPer);
            if (estudio == null)
            {
                return NotFound();
            }
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // POST: Estudios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{IdProf}/{CcPer}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdProf, int CcPer, [Bind("IdProf,CcPer,Fecha,Univer")] EstudiosViewModel model)
        {
            if (IdProf != model.IdProf || CcPer != model.CcPer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _controller.PutEstudio(IdProf, CcPer, model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudioExists(model.IdProf, model.CcPer))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", model.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", model.IdProf);
            return View(model);
        }

        // GET: Estudios/Delete/5
        [HttpGet("delete/{CcPer}/{IdProf}")]
        public async Task<IActionResult> Delete(int? IdProf, int? CcPer)
        {
            if (IdProf == null || CcPer == null)
            {
                return NotFound();
            }

            var estudio = await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .FirstOrDefaultAsync(m => m.IdProf == IdProf && m.CcPer == CcPer);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // POST: Estudios/Delete/5
        [HttpPost("delete/{IdProf}/{CcPer}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int IdProf, int CcPer)
        {

            await _controller.DeleteEstudio(IdProf, CcPer);
            return RedirectToAction(nameof(Index));
        }

        private bool EstudioExists(int id, int cc)
        {
            return _controller.EstudioExists(id, cc);
        }
    }
}
