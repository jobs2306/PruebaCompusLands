using Microsoft.EntityFrameworkCore;
using PruebaApp.Data;
using PruebaApp.Models;
using PruebaApp.Service.Interface;


namespace PruebaApp.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly ApplicationDbContext _context;

        public PedidoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedidos>> GetAllAsync()
        {
            return await _context.pedidos
                .Include(p => p.Cliente)
                .Include(p => p.PedidoProductos)
                    .ThenInclude(pp => pp.Producto)
                .ToListAsync();
        }

        public async Task<Pedidos> GetByIdAsync(int id)
        {
            var pedido = await _context.pedidos
                .Include(p => p.Cliente)
                .Include(p => p.PedidoProductos)
                    .ThenInclude(pp => pp.Producto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                throw new KeyNotFoundException("Pedido no encontrado.");
            }

            return pedido;
        }

        public async Task<Pedidos> AddAsync(Pedidos pedido)
        {
            // Validar que exista el cliente
            var cliente = await _context.clientes.FindAsync(pedido.ClientesId);
            if (cliente == null)
            {
                throw new KeyNotFoundException("El cliente no existe.");
            }

            // Validar stock suficiente para los productos
            foreach (var pp in pedido.PedidoProductos)
            {
                var producto = await _context.productos.FindAsync(pp.ProductosId);
                if (producto == null)
                {
                    throw new KeyNotFoundException($"El producto con ID {pp.ProductosId} no existe.");
                }

                if (producto.Stock < pp.Cantidad)
                {
                    throw new InvalidOperationException($"Stock insuficiente para el producto {producto.Nombre}.");
                }
            }

            // Calcular el total y actualizar el stock
            decimal total = 0;
            foreach (var pp in pedido.PedidoProductos)
            {
                var producto = await _context.productos.FindAsync(pp.ProductosId);
                producto.Stock -= pp.Cantidad;
                total += producto.Precio * pp.Cantidad;

                _context.productos.Update(producto);
            }

            pedido.Total = total;
            pedido.FechaPedido = DateTime.UtcNow;

            // Agregar el pedido y guardar cambios
            _context.pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedidos> UpdateAsync(int id, Pedidos pedido)
        {
            var existingPedido = await _context.pedidos.FindAsync(id);
            if (existingPedido == null)
            {
                throw new KeyNotFoundException("Pedido no encontrado.");
            }

            // Actualizar los detalles del pedido si es necesario
            existingPedido.ClientesId = pedido.ClientesId;
            existingPedido.FechaPedido = pedido.FechaPedido;
            existingPedido.Total = pedido.Total;

            _context.pedidos.Update(existingPedido);
            await _context.SaveChangesAsync();
            return existingPedido;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pedido = await _context.pedidos.FindAsync(id);
            if (pedido == null)
            {
                return false;
            }

            _context.pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
