using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class TipoTratamiento
{
    public int Id { get; set; }

    public string Tipotratamiento1 { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Tratamiento>? Tratamientos { get; set; } = new List<Tratamiento>();
}
