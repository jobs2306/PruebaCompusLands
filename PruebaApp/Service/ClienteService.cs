using Microsoft.EntityFrameworkCore;
using PruebaApp.Data;
using PruebaApp.Models;
using PruebaApp.Service.Interface;

namespace PruebaApp.Service
{
    public class ClienteService : IClientesService
    {
        private readonly ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Clientes>> GetAllAsync()
        {
            return await _context.clientes.ToListAsync();
        }

        public async Task<Clientes> GetByIdAsync(int id)
        {
            return await _context.clientes.FindAsync(id);
        }

        public async Task<Clientes> AddAsync(Clientes cliente)
        {
            // Validar que el email sea único
            if (await _context.clientes.AnyAsync(c => c.Email == cliente.Email))
            {
                throw new InvalidOperationException("El email ya está registrado.");
            }

            cliente.FechaRegistro = DateTime.Now;
            _context.clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Clientes> UpdateAsync(int id, Clientes cliente)
        {
            var existingCliente = await _context.clientes.FindAsync(id);
            if (existingCliente == null)
            {
                throw new KeyNotFoundException("Cliente no encontrado.");
            }

            // Validar que el nuevo email no esté en uso por otro cliente
            if (existingCliente.Email != cliente.Email &&
                await _context.clientes.AnyAsync(c => c.Email == cliente.Email))
            {
                throw new InvalidOperationException("El nuevo email ya está registrado.");
            }

            existingCliente.Nombre = cliente.Nombre;
            existingCliente.Email = cliente.Email;

            _context.clientes.Update(existingCliente);
            await _context.SaveChangesAsync();
            return existingCliente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _context.clientes.FindAsync(id);
            if (cliente == null)
            {
                return false;
            }

            _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
