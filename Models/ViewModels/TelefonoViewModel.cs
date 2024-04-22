using System.ComponentModel.DataAnnotations;

namespace personapi_dotnet.Models.ViewModels
{
    public class TelefonoViewModel
    {
        [Required]
        public string Num { get; set; }
        [Required]
        public string Oper { get; set; }
        [Required]
        public int Duenio { get; set; }
    }
}
