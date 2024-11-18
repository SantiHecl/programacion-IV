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
using Serilog;

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
            Log.Information($"Se llamo a GetClientes");
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{codCliente}")]
        public async Task<ActionResult<ClientesModel>> GetClienteByCodCliente(string codCliente)
        {
            var clientesModel = await _context.Clientes.FindAsync(codCliente);

            if (clientesModel == null)
            {
                Log.Error($"GetClienteByCodCliente No existe el cliente con el código {codCliente}");
                return NotFound();
            }
            Log.Information($"Se llamo a GetClienteByCodCliente");
            return clientesModel;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{codCliente}")]
        public async Task<IActionResult> ModificarCliente(string codCliente, ClientesModel clientesModel)
        {
            if (codCliente != clientesModel.CodCliente)
            {
                Log.Warning($"El código del cliente {codCliente} no coincide con el cliente que desea modificar {clientesModel.CodCliente}");
                return BadRequest();
            }

            _context.Entry(clientesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information($"Cliente con el código {codCliente} actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExiste(codCliente))
                {
                    Log.Error($"No existe el cliente con ese código para actualizar. {codCliente}");
                    return NotFound();
                }
                else
                {
                    Log.Error($"Error al actualizar al cliente con el código {codCliente}");
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
                Log.Information($"Se dio de alta exitosamente el cliente.");
            }
            catch (DbUpdateException)
            {
                if (ClienteExiste(clientesModel.CodCliente))
                {
                    Log.Error($"Ya existe un cliente con ese código {clientesModel.CodCliente}");
                    return Conflict();
                }
                else
                {
                    Log.Error($"Error al crear Cliente");
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
                Log.Error($"Cliente con el código {codCliente} no existe apra borrar.");
                return NotFound();
            }

            _context.Clientes.Remove(clientesModel);
            await _context.SaveChangesAsync();

            Log.Information($"Cliente con el código {codCliente} borrado exitosamente.");
            return NoContent();
        }

        private bool ClienteExiste(string codCliente)
        {
            return _context.Clientes.Any(e => e.CodCliente == codCliente);
        }
    }
}
