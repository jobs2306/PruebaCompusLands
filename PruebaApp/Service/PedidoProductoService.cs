using Microsoft.EntityFrameworkCore;
using PruebaApp.Data;
using PruebaApp.Models;
using PruebaApp.Service.Interface;


namespace PruebaApp.Service
{
    public class PedidoProductoService : IPedidoProductoService
    {
        private readonly ApplicationDbContext _context;

        public PedidoProductoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PedidoProductos>> GetAllAsync()
        {
            return await _context.pedidoProductos
                .Include(pp => pp.Pedido)
                .Include(pp => pp.Producto)
                .ToListAsync();
        }

        public async Task<PedidoProductos> GetByIdAsync(int id)
        {
            var pedidoProducto = await _context.pedidoProductos
                .Include(pp => pp.Pedido)
                .Include(pp => pp.Producto)
                .FirstOrDefaultAsync(pp => pp.id == id);

            if (pedidoProducto == null)
            {
                throw new KeyNotFoundException("PedidoProducto no encontrado.");
            }

            return pedidoProducto;
        }

        public async Task<PedidoProductos> AddAsync(PedidoProductos pedidoProducto)
        {
            // Validar existencia del Pedido
            var pedido = await _context.pedidos.FindAsync(pedidoProducto.PedidosId);
            if (pedido == null)
            {
                throw new KeyNotFoundException("El pedido no existe.");
            }

            // Validar existencia del Producto
            var producto = await _context.productos.FindAsync(pedidoProducto.ProductosId);
            if (producto == null)
            {
                throw new KeyNotFoundException("El producto no existe.");
            }

            _context.pedidoProductos.Add(pedidoProducto);
            await _context.SaveChangesAsync();
            return pedidoProducto;
        }

        public async Task<PedidoProductos> UpdateAsync(int id, PedidoProductos pedidoProducto)
        {
            var existingPedidoProducto = await _context.pedidoProductos.FindAsync(id);
            if (existingPedidoProducto == null)
            {
                throw new KeyNotFoundException("PedidoProducto no encontrado.");
            }

            // Actualizar los datos
            existingPedidoProducto.PedidosId = pedidoProducto.PedidosId;
            existingPedidoProducto.ProductosId = pedidoProducto.ProductosId;
            existingPedidoProducto.Cantidad = pedidoProducto.Cantidad;

            _context.pedidoProductos.Update(existingPedidoProducto);
            await _context.SaveChangesAsync();
            return existingPedidoProducto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pedidoProducto = await _context.pedidoProductos.FindAsync(id);
            if (pedidoProducto == null)
            {
                return false;
            }

            _context.pedidoProductos.Remove(pedidoProducto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}