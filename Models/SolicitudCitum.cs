using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class SolicitudCitum
{
    public int Id { get; set; }

    public int? PacienteId { get; set; }

    public DateTime Fecha { get; set; }

    public TimeSpan Hora { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public int TipoCita { get; set; }

    public string MotivoCita { get; set; } = null!;

    public byte Estado { get; set; }

    public bool Activo { get; set; }

    [JsonIgnore]
    public virtual Cliente? Paciente { get; set; } = null!;
    [JsonIgnore]
    public virtual TipoCitum? TipoCitaNavigation { get; set; } = null!;
}
