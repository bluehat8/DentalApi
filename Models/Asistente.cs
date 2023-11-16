using System;
using System.Collections.Generic;

namespace DentalApi.Models;

public partial class Asistente
{
    public int Id { get; set; }

    public int? Usuario { get; set; }

    public virtual Usuario? UsuarioNavigation { get; set; }
}
