﻿using System;
using System.Collections.Generic;

namespace DentalApi.Models;

public partial class Acompañante
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Telefono { get; set; }

    public string Cedula { get; set; } = null!;

    public string Parentesco { get; set; } = null!;

    public int ClienteAcompañado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public virtual Cliente ClienteAcompañadoNavigation { get; set; } = null!;

    public virtual Telefono TelefonoNavigation { get; set; } = null!;
}
