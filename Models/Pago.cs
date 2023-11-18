using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int Cliente { get; set; }

    public int TipoPago { get; set; }

    public DateTime FechaPago { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public decimal Monto { get; set; }

    public decimal SaldoPendiente { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Cita { get; set; }

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual Citum? CitaNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Cliente? ClienteNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Comprobante>? Comprobantes { get; set; } = new List<Comprobante>();
    [JsonIgnore]
    public virtual TipoPago? TipoPagoNavigation { get; set; } = null!;
}
