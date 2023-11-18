using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class TipoCitum
{
    public int Id { get; set; }

    public string Tipocita { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Citum>? Cita { get; set; } = new List<Citum>();
    [JsonIgnore]
    public virtual ICollection<SolicitudCitum>? SolicitudCita { get; set; } = new List<SolicitudCitum>();
}
