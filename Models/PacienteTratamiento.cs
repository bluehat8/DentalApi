using System;
using System.Collections.Generic;

namespace DentalApi.Models;

public partial class PacienteTratamiento
{
    public int Id { get; set; }

    public int Tratamiento { get; set; }

    public int HistorialMedico { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string Motivo { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public bool? Estado { get; set; }

    public virtual HistorialClinico HistorialMedicoNavigation { get; set; } = null!;

    public virtual ICollection<SeguimientoTratamiento> SeguimientoTratamientos { get; set; } = new List<SeguimientoTratamiento>();

    public virtual Tratamiento TratamientoNavigation { get; set; } = null!;
}
