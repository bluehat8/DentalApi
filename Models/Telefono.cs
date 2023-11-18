using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class Telefono
{
    public int Id { get; set; }

    public string Numero { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Acompañante> Acompañantes { get; set; } = new List<Acompañante>();
    [JsonIgnore]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
