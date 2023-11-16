using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DentalApi.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public int Telefono { get; set; }

    public int Rol { get; set; }

    public string Correo { get; set; } = null!;

    public bool Activo { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Cedula { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    [JsonIgnore]
    public virtual ICollection<Asistente> Asistentes { get; set; } = new List<Asistente>();

    [JsonIgnore]
    public virtual ICollection<Auditorium> Auditoria { get; set; } = new List<Auditorium>();

    [JsonIgnore]
    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    [JsonIgnore]
    public virtual ICollection<Dentistum> Dentista { get; set; } = new List<Dentistum>();

    [JsonIgnore]
    public virtual ICollection<Mensaje> MensajeUsuariordestinatarioNavigations { get; set; } = new List<Mensaje>();

    [JsonIgnore]
    public virtual ICollection<Mensaje> MensajeUsuarioremitenteNavigations { get; set; } = new List<Mensaje>();

    [JsonIgnore]
    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    [JsonIgnore]
    public virtual Telefono TelefonoNavigation { get; set; } = null!;
}
