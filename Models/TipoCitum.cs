using System;
using System.Collections.Generic;

namespace DentalApi.Models;

public partial class TipoCitum
{
    public int Id { get; set; }

    public string Tipocita { get; set; } = null!;

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual ICollection<SolicitudCitum> SolicitudCita { get; set; } = new List<SolicitudCitum>();
}
