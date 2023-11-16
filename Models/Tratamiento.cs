using System;
using System.Collections.Generic;

namespace DentalApi.Models;

public partial class Tratamiento
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public TimeSpan Duracion { get; set; }

    public decimal Precio { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Restricciones { get; set; } = null!;

    public byte[] Imagen { get; set; } = null!;

    public int TipoTratamiento { get; set; }

    public byte Estado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public virtual ICollection<PacienteTratamiento> PacienteTratamientos { get; set; } = new List<PacienteTratamiento>();

    public virtual TipoTratamiento TipoTratamientoNavigation { get; set; } = null!;
}
