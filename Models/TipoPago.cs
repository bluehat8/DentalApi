using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class TipoPago
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public bool Activo { get; set; }

    [JsonIgnore]
    public virtual ICollection<Pago>? Pagos { get; set; } = new List<Pago>();
}
