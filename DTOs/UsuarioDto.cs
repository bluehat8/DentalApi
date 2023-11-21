﻿using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DentalApi.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        public string? Username { get; set; } = null!;

        public string? Contraseña { get; set; } = null!;

        public string? Telefono { get; set; }

        public int Rol { get; set; }

        public string? Correo { get; set; } = null!;

        public bool Activo { get; set; }

        public string? Nombre { get; set; } = null!;

        public string? Apellidos { get; set; } = null!;

        public string? FechaNacimiento { get; set; }

        public string? Cedula { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
