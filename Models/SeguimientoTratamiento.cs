using System;
using System.Collections.Generic;

namespace DentalApi.Models;

public partial class SeguimientoTratamiento
{
    public int Id { get; set; }

    public int PacienteTratamiento { get; set; }

    public string Observaciones { get; set; } = null!;

    public string Progreso { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public virtual PacienteTratamiento PacienteTratamientoNavigation { get; set; } = null!;
}
