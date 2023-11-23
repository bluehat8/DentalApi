namespace DentalApi.DTOs
{
    public class NotificacionesDTO
    {
        public int Id { get; set; }
        public int Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? Asunto { get; set; }
        public string? Cuerpo { get; set; }
        public byte Estado { get; set; }
    }
}
