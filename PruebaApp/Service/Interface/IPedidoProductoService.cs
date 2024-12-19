using PruebaApp.Models;

namespace PruebaApp.Service.Interface
{
    public interface IPedidoProductoService
    {
        //metodos del CRUD
        Task<IEnumerable<PedidoProductos>> GetAllAsync(); //obtener todos los pedidos de productos
        Task<PedidoProductos> GetByIdAsync(int id); //obtener pedido por id
        Task<PedidoProductos> AddAsync(PedidoProductos pp); //Agregar un pedido
        Task<PedidoProductos> UpdateAsync(int id, PedidoProductos pp); //Actualizar un pedido
        Task<bool> DeleteAsync(int id); //Eliminar un pedido
    }
}
