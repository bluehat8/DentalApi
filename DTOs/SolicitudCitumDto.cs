namespace DentalApi.DTOs
{
    public class SolicitudCitumDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public int TipoCita { get; set; }
        public string? MotivoCita { get; set; }
        public int Estado { get; set; }
    }
}
