using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public int Usuario { get; set; }

    public string Ocupacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }


    [JsonIgnore]
    public virtual ICollection<Acompañante>? Acompañantes { get; set; } = new List<Acompañante>();
    [JsonIgnore]
    public virtual ICollection<Citum>? Cita { get; set; } = new List<Citum>();
    [JsonIgnore]
    public virtual ICollection<EstadoCuentum>? EstadoCuenta { get; set; } = new List<EstadoCuentum>();
    [JsonIgnore]
    public virtual ICollection<HistorialClinico>? HistorialClinicos { get; set; } = new List<HistorialClinico>();
    [JsonIgnore]
    public virtual ICollection<Pago>? Pagos { get; set; } = new List<Pago>();
    [JsonIgnore]
    public virtual ICollection<SolicitudCitum>? SolicitudCita { get; set; } = new List<SolicitudCitum>();
    [JsonIgnore]
    public virtual Usuario? UsuarioNavigation { get; set; } = null!;
}
