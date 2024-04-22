using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Docs.Samples;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Controllers.Api;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class PersonasController : Controller
    {
        private readonly PersonaDbContext _context;
        private readonly ApiPersonaController _controller;

        public PersonasController(PersonaDbContext context, ApiPersonaController controller)
        {
            _context = context;
            _controller = controller;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
            return View(await _controller.GetPersonas());
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Cc == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        [Route("Personas/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Personas/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cc,Nombre,Apellido,Genero,Edad")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                await _controller.PostPersona(persona);
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cc,Nombre,Apellido,Genero,Edad")] Persona persona)
        {
            if (id != persona.Cc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _controller.PutPersona(id, persona);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Cc))
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
            return View(persona);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Cc == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _controller.DeletePersona(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
            return _controller.PersonaExists(id);
        }
    }
}
