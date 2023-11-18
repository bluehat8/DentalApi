using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class Notificacione
{
    public int Id { get; set; }

    public int Usuario { get; set; }

    public DateTime Fecha { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public string Asunto { get; set; } = null!;

    public string Cuerpo { get; set; } = null!;

    public byte Estado { get; set; }

    [JsonIgnore]
    public virtual Usuario? UsuarioNavigation { get; set; } = null!;
}
