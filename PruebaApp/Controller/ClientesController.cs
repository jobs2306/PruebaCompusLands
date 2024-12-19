using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaApp.Models;
using PruebaApp.Service.Interface;

namespace PruebaApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesService _clientesService;

        public ClientesController(IClientesService clientesService)
        {
            _clientesService = clientesService;
        }

        // GET: api/clientes
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _clientesService.GetAllAsync();
            return Ok(clientes);
        }

        // GET: api/clientes/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _clientesService.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado.");
            }
            return Ok(cliente);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Clientes cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoCliente = await _clientesService.AddAsync(cliente);
                return CreatedAtAction(nameof(GetById), new { id = nuevoCliente.Id }, nuevoCliente);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/clientes/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Clientes cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var clienteActualizado = await _clientesService.UpdateAsync(id, cliente);
                return Ok(clienteActualizado);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Cliente no encontrado.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/clientes/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _clientesService.DeleteAsync(id);
            if (!eliminado)
            {
                return NotFound("Cliente no encontrado.");
            }
            return NoContent();
        }
    }
}



