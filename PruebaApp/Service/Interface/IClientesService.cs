using PruebaApp.Models;

namespace PruebaApp.Service.Interface
{
    public interface IClientesService
    {
        //metodos del CRUD
        Task<IEnumerable<Clientes>> GetAllAsync(); //obtener todos
        Task<Clientes> GetByIdAsync(int id); //obtener por id
        Task<Clientes> AddAsync(Clientes clientes); //Agregar 
        Task<Clientes> UpdateAsync(int id, Clientes cliente); //Actualizar
        Task<bool> DeleteAsync(int id); //Eliminar 
    }
}
