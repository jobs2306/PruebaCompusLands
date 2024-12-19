using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaApp.Models;
using PruebaApp.Service.Interface;

namespace PruebaApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // GET: api/pedidos
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _pedidoService.GetAllAsync();
            return Ok(pedidos);
        }

        // GET: api/pedidos/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Cliente")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var pedido = await _pedidoService.GetByIdAsync(id);
                return Ok(pedido);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Pedido no encontrado.");
            }
        }

        // POST: api/pedidos
        [HttpPost]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Add([FromBody] Pedidos pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoPedido = await _pedidoService.AddAsync(pedido);
                return CreatedAtAction(nameof(GetById), new { id = nuevoPedido.Id }, nuevoPedido);
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

        // PUT: api/pedidos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Pedidos pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var pedidoActualizado = await _pedidoService.UpdateAsync(id, pedido);
                return Ok(pedidoActualizado);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Pedido no encontrado.");
            }
        }

        // DELETE: api/pedidos/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _pedidoService.DeleteAsync(id);
            if (!eliminado)
            {
                return NotFound("Pedido no encontrado.");
            }
            return NoContent();
        }
    }
}



