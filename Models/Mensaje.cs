using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class Mensaje
{
    public int Id { get; set; }

    public int Usuarioremitente { get; set; }

    public int Usuariordestinatario { get; set; }

    public string Cuerpo { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }


    [JsonIgnore]
    public virtual Usuario? UsuariordestinatarioNavigation { get; set; } = null!;

    [JsonIgnore] 
    public virtual Usuario? UsuarioremitenteNavigation { get; set; } = null!;

}
