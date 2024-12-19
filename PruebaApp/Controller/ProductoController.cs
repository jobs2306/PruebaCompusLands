using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaApp.Models;
using PruebaApp.Service.Interface;

namespace PruebaApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] int? minStock)
        {
            var productos = await _productoService.GetFilteredAsync(minPrice, maxPrice, minStock);
            return Ok(productos);
        }

        // GET: api/productos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }
            return Ok(producto);
        }

        // POST: api/productos
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] Productos producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevoProducto = await _productoService.AddAsync(producto);
            return CreatedAtAction(nameof(GetById), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        // PUT: api/productos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Productos producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var productoActualizado = await _productoService.UpdateAsync(id, producto);
                return Ok(productoActualizado);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Producto no encontrado.");
            }
        }

        // DELETE: api/productos/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _productoService.DeleteAsync(id);
            if (!eliminado)
            {
                return NotFound("Producto no encontrado.");
            }
            return NoContent();
        }
    }
}

