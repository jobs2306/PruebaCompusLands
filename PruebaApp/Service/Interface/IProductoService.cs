using PruebaApp.Models;

namespace PruebaApp.Service.Interface
{
    public interface IProductoService
    {
        //Metodos del servicio
        Task<IEnumerable<Productos>> GetAllAsync();
        Task<Productos> GetByIdAsync(int id);
        Task<Productos> AddAsync(Productos producto);
        Task<Productos> UpdateAsync(int id, Productos producto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Productos>> GetFilteredAsync(decimal? minPrice, decimal? maxPrice, int? minStock);
    }
}
