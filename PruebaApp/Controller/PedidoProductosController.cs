using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaApp.Models;
using PruebaApp.Service.Interface;

namespace PruebaApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoProductosController : ControllerBase
    {
        private readonly IPedidoProductoService _pedidoProductoService;

        public PedidoProductosController(IPedidoProductoService pedidoProductoService)
        {
            _pedidoProductoService = pedidoProductoService;
        }

        // GET: api/pedidoproductos
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var pedidoProductos = await _pedidoProductoService.GetAllAsync();
            return Ok(pedidoProductos);
        }

        // GET: api/pedidoproductos/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var pedidoProducto = await _pedidoProductoService.GetByIdAsync(id);
                return Ok(pedidoProducto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("PedidoProducto no encontrado.");
            }
        }

        // POST: api/pedidoproductos
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] PedidoProductos pedidoProducto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoPedidoProducto = await _pedidoProductoService.AddAsync(pedidoProducto);
                return CreatedAtAction(nameof(GetById), new { id = nuevoPedidoProducto.id }, nuevoPedidoProducto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/pedidoproductos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] PedidoProductos pedidoProducto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var pedidoProductoActualizado = await _pedidoProductoService.UpdateAsync(id, pedidoProducto);
                return Ok(pedidoProductoActualizado);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("PedidoProducto no encontrado.");
            }
        }

        // DELETE: api/pedidoproductos/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _pedidoProductoService.DeleteAsync(id);
            if (!eliminado)
            {
                return NotFound("PedidoProducto no encontrado.");
            }
            return NoContent();
        }
    }

}


