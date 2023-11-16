using System;
using System.Collections.Generic;

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

    public virtual Citum CitaNavigation { get; set; } = null!;

    public virtual Cliente ClienteNavigation { get; set; } = null!;

    public virtual ICollection<Comprobante> Comprobantes { get; set; } = new List<Comprobante>();

    public virtual TipoPago TipoPagoNavigation { get; set; } = null!;
}
