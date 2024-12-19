using PruebaApp.Models;

namespace PruebaApp.Service.Interface
{
    public interface IPedidoService
    {
        //metodos del CRUD
        Task<IEnumerable<Pedidos>> GetAllAsync(); //obtener todos los pedidos
        Task<Pedidos> GetByIdAsync(int id); //obtener pedido por id
        Task<Pedidos> AddAsync(Pedidos pedido); //Agregar un pedido
        Task<Pedidos> UpdateAsync(int id, Pedidos pedido); //Actualizar un pedido
        Task<bool> DeleteAsync(int id); //Eliminar un pedido
    }
}
