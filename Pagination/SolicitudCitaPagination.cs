using System.Runtime.Serialization;

namespace DentalApi.Pagination
{
    public class SolicitudCitaPagination: PaginacionModel
    {
        public int? TipoCita { get; set; }
        public string? MotivoCita { get; set; }
    }
}
