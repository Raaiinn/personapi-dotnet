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
    public class TelefonosController : Controller
    {
        private readonly PersonaDbContext _context;
        private readonly ApiTelefonoController _controller;

        public TelefonosController(PersonaDbContext context, ApiTelefonoController controller)
        {
            _context = context;
            _controller = controller;
        }

        // GET: Telefonoes
        public async Task<IActionResult> Index()
        {
            return View(await _controller.GetTelefonos());
        }

        // GET: Telefonos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .FirstOrDefaultAsync(m => m.Num == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // GET: Telefonos/Create
        [HttpGet("Telefonos/Create")]
        public IActionResult Create()
        {
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc");
            return View();
        }

        // POST: Telefonos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Telefonos/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Num,Oper,Duenio")] TelefonoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var telefono = new Telefono()
                {
                    Num = model.Num,
                    Duenio = model.Duenio,
                    Oper = model.Oper
                };
                await _controller.PostTelefono(telefono);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc", model.Duenio);
            return View(model);
        }

        // GET: Telefonoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        // POST: Telefonoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Num,Oper,Duenio")] TelefonoViewModel model)
        {
            if (id != model.Num)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var telefono = new Telefono()
                    {
                        Num = model.Num,
                        Duenio = model.Duenio,
                        Oper = model.Oper
                    };
                    await _controller.PutTelefono(model.Num, telefono);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelefonoExists(model.Num))
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
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc", model.Duenio);
            return View(model);
        }

        // GET: Telefonoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .FirstOrDefaultAsync(m => m.Num == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // POST: Telefonoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            await _controller.DeleteTelefono(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TelefonoExists(string id)
        {
            return _controller.TelefonoExists(id);
        }
    }
}
