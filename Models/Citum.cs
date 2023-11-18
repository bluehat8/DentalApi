using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class Citum
{
    public int Id { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public TimeSpan Duracion { get; set; }

    public int TipoCita { get; set; }

    public int Cliente { get; set; }

    public int Dentista { get; set; }

    public decimal Monto { get; set; }

    public bool Estado { get; set; }

    public decimal? Deuda { get; set; }

    public bool? Pagado { get; set; }

    [JsonIgnore]
    public virtual Cliente? ClienteNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Dentistum? DentistaNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Pago>? Pagos { get; set; } = new List<Pago>();

    [JsonIgnore]
    public virtual TipoCitum? TipoCitaNavigation { get; set; } = null!;
}
