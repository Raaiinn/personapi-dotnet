using System.ComponentModel.DataAnnotations;

namespace personapi_dotnet.Models.ViewModels
{
    public class PersonaViewModel
    {
        [Required]
        public int Cc { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Genero { get; set; }
        [Required]
        public int Edad { get; set;}
    }
}
