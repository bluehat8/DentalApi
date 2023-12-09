namespace DentalApi.DTOs
{
    public class PaginacionResultadoDto<T>
    {
        public int TotalElementos { get; set; }
        public int PaginaActual { get; set; }
        public int TamanoPagina { get; set; }
        public List<T>? Elementos { get; set; }
    }
}
