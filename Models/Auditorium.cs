using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace DentalApi.Models;

public partial class Auditorium
{
    public int Id { get; set; }

    public int Usuario { get; set; }

    public string TipoAccion { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string DireccionIp { get; set; } = null!;

    public string NombreMaquina { get; set; } = null!;

    public bool Estado { get; set; }

    public DateTime FechaModificacion { get; set; }

    [JsonIgnore]
    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
