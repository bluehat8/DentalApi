using System;
using System.Collections.Generic;

namespace DentalApi.Models;

public partial class Cuota
{
    public int Id { get; set; }

    public int IdComprobante { get; set; }

    public DateTime FechaComprobante { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificada { get; set; }

    public virtual Comprobante IdComprobanteNavigation { get; set; } = null!;
}
