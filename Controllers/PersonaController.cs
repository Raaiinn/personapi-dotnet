using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.ViewModels;

namespace personapi_dotnet.Controllers
{
    public class PersonaController : Controller
    {
        private readonly PersonaDbContext _context;
        public PersonaController(PersonaDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Personas.ToListAsync());
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(PersonaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var persona = new Persona()
                {
                    Cc = model.Cc,
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Genero = model.Genero,
                    Edad = model.Edad
                };
                _context.Personas.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
