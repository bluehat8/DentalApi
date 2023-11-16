using System;
using System.Collections.Generic;

namespace DentalApi.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public int Usuario { get; set; }

    public string Ocupacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public virtual ICollection<Acompañante> Acompañantes { get; set; } = new List<Acompañante>();

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual ICollection<EstadoCuentum> EstadoCuenta { get; set; } = new List<EstadoCuentum>();

    public virtual ICollection<HistorialClinico> HistorialClinicos { get; set; } = new List<HistorialClinico>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<SolicitudCitum> SolicitudCita { get; set; } = new List<SolicitudCitum>();

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
