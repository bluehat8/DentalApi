using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class Asistente
{

    public int Id { get; set; }

    public int? Usuario { get; set; }

    [JsonIgnore]
    public virtual Usuario? UsuarioNavigation { get; set; }
}
