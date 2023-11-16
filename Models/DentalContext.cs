using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DentalApi.Models;

public partial class DentalContext : DbContext
{
    public string? Conexion { get; set; }

    public DentalContext()
    {
    }

    public DentalContext(DbContextOptions<DentalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acompañante> Acompañantes { get; set; }

    public virtual DbSet<Asistente> Asistentes { get; set; }

    public virtual DbSet<Auditorium> Auditoria { get; set; }

    public virtual DbSet<Citum> Cita { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Comprobante> Comprobantes { get; set; }

    public virtual DbSet<Cuota> Cuotas { get; set; }

    public virtual DbSet<Dentistum> Dentista { get; set; }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<EstadoCuentum> EstadoCuenta { get; set; }

    public virtual DbSet<HistorialClinico> HistorialClinicos { get; set; }

    public virtual DbSet<Mensaje> Mensajes { get; set; }

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<PacienteTratamiento> PacienteTratamientos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<SeguimientoTratamiento> SeguimientoTratamientos { get; set; }

    public virtual DbSet<SolicitudCitum> SolicitudCita { get; set; }

    public virtual DbSet<Telefono> Telefonos { get; set; }

    public virtual DbSet<TipoCitum> TipoCita { get; set; }

    public virtual DbSet<TipoPago> TipoPagos { get; set; }

    public virtual DbSet<TipoTratamiento> TipoTratamientos { get; set; }

    public virtual DbSet<Tratamiento> Tratamientos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public void setConnectionString()
    {
        var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        string? connectionString = config.GetConnectionString("CadenaSQL");

        Conexion = connectionString;

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string? connectionString = config.GetConnectionString("conexion");

            Conexion = connectionString;

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Acompañante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Acompaña__3214EC277D2FAF93");

            entity.ToTable("Acompañante");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Cedula)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.ClienteAcompañado).HasColumnName("clienteAcompañado");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Parentesco)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("parentesco");
            entity.Property(e => e.Telefono).HasColumnName("telefono");

            entity.HasOne(d => d.ClienteAcompañadoNavigation).WithMany(p => p.Acompañantes)
                .HasForeignKey(d => d.ClienteAcompañado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Acompañan__clien__4F7CD00D");

            entity.HasOne(d => d.TelefonoNavigation).WithMany(p => p.Acompañantes)
                .HasForeignKey(d => d.Telefono)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Acompañan__telef__4E88ABD4");
        });

        modelBuilder.Entity<Asistente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Asistent__3214EC2742DB15D6");

            entity.ToTable("Asistente");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Asistentes)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("FK__Asistente__Usuar__44FF419A");
        });

        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Auditori__3214EC27FE185843");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DireccionIp)
                .HasMaxLength(50)
                .HasColumnName("direccionIP");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.NombreMaquina)
                .HasMaxLength(50)
                .HasColumnName("nombreMaquina");
            entity.Property(e => e.TipoAccion)
                .HasMaxLength(50)
                .HasColumnName("tipoAccion");
            entity.Property(e => e.Usuario).HasColumnName("usuario");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Auditoria__usuar__6A30C649");
        });

        modelBuilder.Entity<Citum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cita__3214EC273AE628A4");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cliente).HasColumnName("cliente");
            entity.Property(e => e.Dentista).HasColumnName("dentista");
            entity.Property(e => e.Deuda)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("deuda");
            entity.Property(e => e.Duracion).HasColumnName("duracion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.Pagado).HasColumnName("pagado");
            entity.Property(e => e.TipoCita).HasColumnName("tipoCita");

            entity.HasOne(d => d.ClienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.Cliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__cliente__534D60F1");

            entity.HasOne(d => d.DentistaNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.Dentista)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__dentista__5441852A");

            entity.HasOne(d => d.TipoCitaNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.TipoCita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__tipoCita__52593CB8");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3214EC27E66DB9D3");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.Ocupacion)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("ocupacion");
            entity.Property(e => e.Usuario).HasColumnName("usuario");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cliente__usuario__4BAC3F29");
        });

        modelBuilder.Entity<Comprobante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comproba__3214EC27C8243320");

            entity.ToTable("Comprobante");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FechaComprobante)
                .HasColumnType("datetime")
                .HasColumnName("fechaComprobante");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.Numero)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.Pago).HasColumnName("pago");

            entity.HasOne(d => d.PagoNavigation).WithMany(p => p.Comprobantes)
                .HasForeignKey(d => d.Pago)
                .HasConstraintName("FK__Comprobant__pago__5DCAEF64");
        });

        modelBuilder.Entity<Cuota>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cuotas__3214EC2785E046CB");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FechaComprobante)
                .HasColumnType("datetime")
                .HasColumnName("fechaComprobante");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificada)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificada");
            entity.Property(e => e.IdComprobante).HasColumnName("idComprobante");

            entity.HasOne(d => d.IdComprobanteNavigation).WithMany(p => p.Cuota)
                .HasForeignKey(d => d.IdComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cuotas__idCompro__60A75C0F");
        });

        modelBuilder.Entity<Dentistum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dentista__3214EC27BCE71CF0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Especialidad).HasColumnName("especialidad");
            entity.Property(e => e.Usuario).HasColumnName("usuario");

            entity.HasOne(d => d.EspecialidadNavigation).WithMany(p => p.Dentista)
                .HasForeignKey(d => d.Especialidad)
                .HasConstraintName("FK__Dentista__especi__48CFD27E");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Dentista)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("FK__Dentista__usuari__47DBAE45");
        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Especial__3214EC2776449D36");

            entity.ToTable("Especialidad");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Especialidad1)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("especialidad");
        });

        modelBuilder.Entity<EstadoCuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EstadoCu__3214EC27616BD868");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cliente).HasColumnName("cliente");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 5)")
                .HasColumnName("monto");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 5)")
                .HasColumnName("total");

            entity.HasOne(d => d.ClienteNavigation).WithMany(p => p.EstadoCuenta)
                .HasForeignKey(d => d.Cliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EstadoCue__clien__6754599E");
        });

        modelBuilder.Entity<HistorialClinico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC279C2273F2");

            entity.ToTable("HistorialClinico");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Caries).HasColumnName("caries");
            entity.Property(e => e.Deporte).HasColumnName("deporte");
            entity.Property(e => e.DetalleOperacion).HasColumnName("detalleOperacion");
            entity.Property(e => e.Diabetico).HasColumnName("diabetico");
            entity.Property(e => e.DientesFracturados).HasColumnName("dientesFracturados");
            entity.Property(e => e.Embarazado).HasColumnName("embarazado");
            entity.Property(e => e.EnfermedadMadre)
                .HasMaxLength(300)
                .HasColumnName("enfermedadMadre");
            entity.Property(e => e.EnfermedadPadre)
                .HasMaxLength(300)
                .HasColumnName("enfermedadPadre");
            entity.Property(e => e.Epileptico).HasColumnName("epileptico");
            entity.Property(e => e.EstadoBucal).HasColumnName("estadoBucal");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.MalestarDeporte).HasColumnName("malestarDeporte");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.Operado).HasColumnName("operado");
            entity.Property(e => e.OtrasEnfermedades).HasColumnName("otrasEnfermedades");
            entity.Property(e => e.Paciente).HasColumnName("paciente");
            entity.Property(e => e.PresionAlta).HasColumnName("presionAlta");
            entity.Property(e => e.ProblemaCardiaco).HasColumnName("problemaCardiaco");
            entity.Property(e => e.ProblemaRenales).HasColumnName("problemaRenales");
            entity.Property(e => e.SangraEncimas).HasColumnName("sangraEncimas");

            entity.HasOne(d => d.PacienteNavigation).WithMany(p => p.HistorialClinicos)
                .HasForeignKey(d => d.Paciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__pacie__76969D2E");
        });

        modelBuilder.Entity<Mensaje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mensaje__3214EC273072DED7");

            entity.ToTable("Mensaje");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cuerpo).HasColumnName("cuerpo");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.Usuariordestinatario).HasColumnName("usuariordestinatario");
            entity.Property(e => e.Usuarioremitente).HasColumnName("usuarioremitente");

            entity.HasOne(d => d.UsuariordestinatarioNavigation).WithMany(p => p.MensajeUsuariordestinatarioNavigations)
                .HasForeignKey(d => d.Usuariordestinatario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Mensaje__usuario__6E01572D");

            entity.HasOne(d => d.UsuarioremitenteNavigation).WithMany(p => p.MensajeUsuarioremitenteNavigations)
                .HasForeignKey(d => d.Usuarioremitente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Mensaje__usuario__6D0D32F4");
        });

        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC279153365A");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Asunto)
                .HasMaxLength(300)
                .HasColumnName("asunto");
            entity.Property(e => e.Cuerpo)
                .HasMaxLength(300)
                .HasColumnName("cuerpo");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.Usuario).HasColumnName("usuario");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificac__usuar__70DDC3D8");
        });

        modelBuilder.Entity<PacienteTratamiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Paciente__3214EC271A334BFC");

            entity.ToTable("PacienteTratamiento");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fechaFin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fechaInicio");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.HistorialMedico).HasColumnName("historialMedico");
            entity.Property(e => e.Motivo).HasColumnName("motivo");
            entity.Property(e => e.Tratamiento).HasColumnName("tratamiento");

            entity.HasOne(d => d.HistorialMedicoNavigation).WithMany(p => p.PacienteTratamientos)
                .HasForeignKey(d => d.HistorialMedico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PacienteT__histo__7A672E12");

            entity.HasOne(d => d.TratamientoNavigation).WithMany(p => p.PacienteTratamientos)
                .HasForeignKey(d => d.Tratamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PacienteT__trata__797309D9");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pago__3214EC27733AFF73");

            entity.ToTable("Pago");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cita).HasColumnName("cita");
            entity.Property(e => e.Cliente).HasColumnName("cliente");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.FechaPago)
                .HasColumnType("datetime")
                .HasColumnName("fechaPago");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 5)")
                .HasColumnName("monto");
            entity.Property(e => e.SaldoPendiente)
                .HasColumnType("decimal(10, 5)")
                .HasColumnName("saldoPendiente");
            entity.Property(e => e.TipoPago).HasColumnName("tipoPago");

            entity.HasOne(d => d.CitaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.Cita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__cita__59FA5E80");

            entity.HasOne(d => d.ClienteNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.Cliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__cliente__5812160E");

            entity.HasOne(d => d.TipoPagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.TipoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__tipoPago__59063A47");
        });

        modelBuilder.Entity<SeguimientoTratamiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seguimie__3214EC275B8BD312");

            entity.ToTable("SeguimientoTratamiento");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(400)
                .HasColumnName("observaciones");
            entity.Property(e => e.PacienteTratamiento).HasColumnName("pacienteTratamiento");
            entity.Property(e => e.Progreso).HasColumnName("progreso");

            entity.HasOne(d => d.PacienteTratamientoNavigation).WithMany(p => p.SeguimientoTratamientos)
                .HasForeignKey(d => d.PacienteTratamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Seguimien__pacie__7D439ABD");
        });

        modelBuilder.Entity<SolicitudCitum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Solicitu__3214EC27618E0EAD");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.MotivoCita).HasColumnName("motivoCita");
            entity.Property(e => e.PacienteId).HasColumnName("pacienteID");
            entity.Property(e => e.TipoCita).HasColumnName("tipoCita");

            entity.HasOne(d => d.Paciente).WithMany(p => p.SolicitudCita)
                .HasForeignKey(d => d.PacienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Solicitud__pacie__6383C8BA");

            entity.HasOne(d => d.TipoCitaNavigation).WithMany(p => p.SolicitudCita)
                .HasForeignKey(d => d.TipoCita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Solicitud__tipoC__6477ECF3");
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Telefono__3214EC27A5DF3779");

            entity.ToTable("Telefono");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Numero)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("numero");
        });

        modelBuilder.Entity<TipoCitum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoCita__3214EC27D3A58532");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Tipocita)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("tipocita");
        });

        modelBuilder.Entity<TipoPago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoPago__3214EC27DCF8F35A");

            entity.ToTable("TipoPago");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoTratamiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoTrat__3214EC276260F20A");

            entity.ToTable("TipoTratamiento");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Tipotratamiento1)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("tipotratamiento");
        });

        modelBuilder.Entity<Tratamiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tratamie__3214EC27A1969161");

            entity.ToTable("Tratamiento");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Duracion).HasColumnName("duracion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.Imagen)
                .HasMaxLength(1)
                .HasColumnName("imagen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 5)")
                .HasColumnName("precio");
            entity.Property(e => e.Restricciones)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("restricciones");
            entity.Property(e => e.TipoTratamiento).HasColumnName("tipoTratamiento");

            entity.HasOne(d => d.TipoTratamientoNavigation).WithMany(p => p.Tratamientos)
                .HasForeignKey(d => d.TipoTratamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tratamien__tipoT__73BA3083");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC27094A11F0");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Correo, "UQ__Usuario__2A586E0BEFB4A4C0").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Usuario__F3DBC5726D676ED9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Cedula)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.Correo)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fechaNacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol).HasColumnName("rol");
            entity.Property(e => e.Telefono).HasColumnName("telefono");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.TelefonoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Telefono)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__telefon__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
