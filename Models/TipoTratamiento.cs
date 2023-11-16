using System;
using System.Collections.Generic;

namespace DentalApi.Models;

public partial class TipoTratamiento
{
    public int Id { get; set; }

    public string Tipotratamiento1 { get; set; } = null!;

    public virtual ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
}
