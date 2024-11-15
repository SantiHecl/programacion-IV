using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CuponProyecto.Data;
using CuponProyecto.Models;
using Serilog;
using CuponProyecto.Interfaces;

namespace CuponProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cupon_ClienteController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly ICuponesServices _cuponesServices;

        public Cupon_ClienteController(DataBaseContext context, ICuponesServices cuponesServices)
        {
            _context = context;
            _cuponesServices = cuponesServices;
        }

        // GET: api/Cupon_Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupon_ClienteModel>>> GetCupones_Clientes()
        {
            Log.Information($"Se llamo a GetCupones_Clientes");
            return await _context.Cupones_Clientes.ToListAsync();
        }

        // GET: api/Cupon_Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cupon_ClienteModel>> GetCupon_ClienteModel(string id)
        {
            var cupon_ClienteModel = await _context.Cupones_Clientes.FindAsync(id);

            if (cupon_ClienteModel == null)
            {
                Log.Error($"GetCupones_ClientesId No existe el articulo con esa id, {id}");
                return NotFound();
            }
            Log.Information($"Se llamo a GetCupones_ClientesId");
            return cupon_ClienteModel;
        }

        // PUT: api/Cupon_Cliente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupon_ClienteModel(string id, Cupon_ClienteModel cupon_ClienteModel)
        {
            if (id != cupon_ClienteModel.NroCupon)
            {
                Log.Warning($"ID en la ruta ({id}) no coincide con el ID del cupon ({cupon_ClienteModel.NroCupon})");
                return BadRequest();
            }

            _context.Entry(cupon_ClienteModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information($"Cupon_Cliente con ID {id} actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cupon_ClienteModelExists(id))
                {
                    Log.Error($"No existe el Cupon_Cliente con esa id para actualizar, {id}");
                    return NotFound();
                }
                else
                {
                    Log.Error($"Error al actualizar el Cupon_Cliente con ID {id}");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cupon_Cliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cupon_ClienteModel>> PostCupon_ClienteModel(Cupon_ClienteModel cupon_ClienteModel)
        {
            cupon_ClienteModel.NroCupon = await _cuponesServices.GenerarNroCupon();
            cupon_ClienteModel.FechaAsignado = DateTime.Now;

            _context.Cupones_Clientes.Add(cupon_ClienteModel);
            try
            {
                await _context.SaveChangesAsync();
                Log.Information($"cupon_Cliente creado exitosamente.");
            }
            catch (DbUpdateException)
            {
                if (Cupon_ClienteModelExists(cupon_ClienteModel.NroCupon))
                {
                    Log.Error($"Ya existe el Cupon_Cliente con {cupon_ClienteModel.NroCupon}");
                    return Conflict();
                }
                else
                {
                    Log.Error($"Error al crear el Cupon_Cliente");
                    throw;
                }
            }

            return Ok ("Se dio de alta exitosamente en Cupon_Cliente");
        }

        // DELETE: api/Cupon_Cliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupon_ClienteModel(string id)
        {
            var cupon_ClienteModel = await _context.Cupones_Clientes.FindAsync(id);
            if (cupon_ClienteModel == null)
            {
                Log.Error($"Cupon_Cliente con ID {id} no existe para borrar.");
                return NotFound();
            }

            _context.Cupones_Clientes.Remove(cupon_ClienteModel);
            await _context.SaveChangesAsync();

            Log.Information($"Cupon_Cliente con ID {id} borrado exitosamente.");
            return NoContent();
        }

        private bool Cupon_ClienteModelExists(string id)
        {
            return _context.Cupones_Clientes.Any(e => e.NroCupon == id);
        }
    }
}
