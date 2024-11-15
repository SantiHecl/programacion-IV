using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientesApi.Data;
using ClientesApi.Models;
using ClientesApi.Interfaces;
using ClientesApi.Models.DTO;

namespace ClientesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly IClienteService _clienteService;

        public ClientesController(DataBaseContext context, IClienteService clienteService)
        {
            _context = context;
            _clienteService = clienteService;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesModel>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{codCliente}")]
        public async Task<ActionResult<ClientesModel>> GetClienteByCodCliente(string codCliente)
        {
            var clientesModel = await _context.Clientes.FindAsync(codCliente);

            if (clientesModel == null)
            {
                return NotFound();
            }

            return clientesModel;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{codCliente}")]
        public async Task<IActionResult> ModificarCliente(string codCliente, ClientesModel clientesModel)
        {
            if (codCliente != clientesModel.CodCliente)
            {
                return BadRequest();
            }

            _context.Entry(clientesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExiste(codCliente))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientesModel>> AltaNuevoCliente(ClientesModel clientesModel)
        {
            _context.Clientes.Add(clientesModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClienteExiste(clientesModel.CodCliente))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("AltaNuevoCliente", new { codCliente = clientesModel.CodCliente }, clientesModel);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{codCliente}")]
        public async Task<IActionResult> EliminarCliente(string codCliente)
        {
            var clientesModel = await _context.Clientes.FindAsync(codCliente);
            if (clientesModel == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(clientesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExiste(string codCliente)
        {
            return _context.Clientes.Any(e => e.CodCliente == codCliente);
        }
    }
}
