using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace personapi_dotnet.Models.Entities;

public partial class Estudio
{
    public int IdProf { get; set; }

    public int CcPer { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Univer { get; set; }

    [ForeignKey("CcPer")] public virtual Persona CcPerNavigation { get; set; } = null!;

    [ForeignKey("IdProf")] public virtual Profesion IdProfNavigation { get; set; } = null!;
}
