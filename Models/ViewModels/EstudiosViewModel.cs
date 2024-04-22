using System.ComponentModel.DataAnnotations;

namespace personapi_dotnet.Models.ViewModels
{
    public class EstudiosViewModel
    {
        [Required]
        public int IdProf { get; set; }
        [Required]
        public int CcPer { get; set; }
        [Required]
        public DateOnly? Fecha { get; set; }
        [Required]
        public string Univer { get; set; }
    }
}
