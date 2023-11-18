using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace DentalApi.Models;

public partial class Dentistum
{
    public int Id { get; set; }

    public int? Usuario { get; set; }

    public int? Especialidad { get; set; }

    [JsonIgnore]
    public virtual ICollection<Citum>? Cita { get; set; } = new List<Citum>();
    [JsonIgnore]
    public virtual Especialidad? EspecialidadNavigation { get; set; }
    [JsonIgnore]
    public virtual Usuario? UsuarioNavigation { get; set; }
}
