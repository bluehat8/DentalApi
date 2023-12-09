using DentalApi.DTOs;
using DentalApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DentalApi.Pagination
{
    public class PaginacionService<T> where T : class
    {
        private readonly DentalContext _dbContext;

        public PaginacionService(DentalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginacionResultadoDto<TDto>> ObtenerPaginaAsync<TDto>(
            Expression<Func<T, bool>> filtro,
            Expression<Func<T, TDto>> mapeo,
            PaginacionModel paginacion)
        {
            var consulta = _dbContext.Set<T>().AsQueryable();

            if (filtro != null)
            {
                consulta = consulta.Where(filtro);
            }

            var totalElementos = await consulta.CountAsync();

            var elementos = await consulta
                .Skip((paginacion.Pagina - 1) * paginacion.TamanoPagina)
                .Take(paginacion.TamanoPagina)
                .Select(mapeo)
                .ToListAsync();

            return new PaginacionResultadoDto<TDto>
            {
                TotalElementos = totalElementos,
                PaginaActual = paginacion.Pagina,
                TamanoPagina = paginacion.TamanoPagina,
                Elementos = elementos
            };
        }
    }
}
