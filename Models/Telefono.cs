using System;
using System.Collections.Generic;

namespace DentalApi.Models;

public partial class Telefono
{
    public int Id { get; set; }

    public string Numero { get; set; } = null!;

    public virtual ICollection<Acompañante> Acompañantes { get; set; } = new List<Acompañante>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
