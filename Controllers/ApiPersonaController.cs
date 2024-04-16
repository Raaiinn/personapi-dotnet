using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPersonaController(PersonaDbContext context) : ControllerBase
    {
        private readonly PersonaDbContext _context = context;

        public async Task<List<Persona>> Get() => await _context.Personas.ToListAsync();
    }
}
