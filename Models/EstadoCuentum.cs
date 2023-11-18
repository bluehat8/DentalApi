using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class EstadoCuentum
{
    public int Id { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public TimeSpan Fecha { get; set; }

    public int Cliente { get; set; }

    public decimal Monto { get; set; }

    public decimal Total { get; set; }
    [JsonIgnore]
    public virtual Cliente? ClienteNavigation { get; set; } = null!;
}
