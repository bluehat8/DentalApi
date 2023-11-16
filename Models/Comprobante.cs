using System;
using System.Collections.Generic;

namespace DentalApi.Models;

public partial class Comprobante
{
    public int Id { get; set; }

    public string Numero { get; set; } = null!;

    public DateTime FechaComprobante { get; set; }

    public DateTime FechaModificacion { get; set; }

    public int? Pago { get; set; }

    public virtual ICollection<Cuota> Cuota { get; set; } = new List<Cuota>();

    public virtual Pago? PagoNavigation { get; set; }
}
