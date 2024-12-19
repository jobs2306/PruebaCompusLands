using Microsoft.EntityFrameworkCore;
using PruebaApp.Data;
using PruebaApp.Models;
using PruebaApp.Service.Interface;


namespace PruebaApp.Service
{
    public class ProductoService : IProductoService
    {
        private readonly ApplicationDbContext _context;

        public ProductoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Productos>> GetAllAsync()
        {
            return await _context.productos.ToListAsync();
        }

        public async Task<Productos> GetByIdAsync(int id)
        {
            return await _context.productos.FindAsync(id);
        }

        public async Task<Productos> AddAsync(Productos producto)
        {
            _context.productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<Productos> UpdateAsync(int id, Productos producto)
        {
            var existingProducto = await _context.productos.FindAsync(id);
            if (existingProducto == null)
            {
                throw new KeyNotFoundException("Producto no encontrado.");
            }

            existingProducto.Nombre = producto.Nombre;
            existingProducto.Precio = producto.Precio;
            existingProducto.Stock = producto.Stock;
            existingProducto.FechaCreacion = producto.FechaCreacion;

            _context.productos.Update(existingProducto);
            await _context.SaveChangesAsync();
            return existingProducto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var producto = await _context.productos.FindAsync(id);
            if (producto == null)
            {
                return false;
            }

            _context.productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<Productos>> GetFilteredAsync(decimal? minPrice, decimal? maxPrice, int? minStock)
        {
            var query = _context.productos.AsQueryable();

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Precio >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Precio <= maxPrice.Value);
            }

            if (minStock.HasValue)
            {
                query = query.Where(p => p.Stock >= minStock.Value);
            }

            return await query.ToListAsync();
        }
    }
}
