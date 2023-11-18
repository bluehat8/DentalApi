using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class Especialidad
{
    public int Id { get; set; }

    public string Especialidad1 { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Dentistum>? Dentista { get; set; } = new List<Dentistum>();
}
