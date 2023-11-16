using System;
using System.Collections.Generic;

namespace DentalApi.Models;

public partial class Dentistum
{
    public int Id { get; set; }

    public int? Usuario { get; set; }

    public int? Especialidad { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual Especialidad? EspecialidadNavigation { get; set; }

    public virtual Usuario? UsuarioNavigation { get; set; }
}
