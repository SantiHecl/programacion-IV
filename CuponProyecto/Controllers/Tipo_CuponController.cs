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

namespace CuponProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tipo_CuponController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public Tipo_CuponController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Tipo_Cupon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_CuponModel>>> GetTipo_Cupon()
        {
            Log.Information($"Se llamo a GetTipo_Cupon");
            return await _context.Tipo_Cupon.ToListAsync();
        }

        // GET: api/Tipo_Cupon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_CuponModel>> GetTipo_CuponModel(int id)
        {
            var tipo_CuponModel = await _context.Tipo_Cupon.FindAsync(id);

            if (tipo_CuponModel == null)
            {
                Log.Error($"GetTipo_CuponId No existe el tipo de cupón con esa id, {id}");
                return NotFound();
            }
            Log.Information($"Se llamo a GetTipo_CuponesId");
            return tipo_CuponModel;
        }

        // PUT: api/Tipo_Cupon/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_CuponModel(int id, Tipo_CuponModel tipo_CuponModel)
        {
            if (id != tipo_CuponModel.Id_Tipo_Cupon)
            {
                Log.Warning($"ID en la ruta ({id}) no coincide con el ID del tipo de cupón ({tipo_CuponModel.Id_Tipo_Cupon})");
                return BadRequest();
            }

            _context.Entry(tipo_CuponModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information($"Tipo_CuponModel con ID {id} actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_CuponModelExists(id))
                {
                    Log.Error($"No existe el Tipo_Cupon con esa id para actualizar, {id}");
                    return NotFound();
                }
                else
                {
                    Log.Error($"Error al actualizar el Tipo_Cupon con ID {id}");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tipo_Cupon
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipo_CuponModel>> PostTipo_CuponModel(Tipo_CuponModel tipo_CuponModel)
        {
            _context.Tipo_Cupon.Add(tipo_CuponModel);
            await _context.SaveChangesAsync();

            Log.Information($"Tipo_Cupon creado exitosamente.");
            return CreatedAtAction("GetTipo_CuponModel", new { id = tipo_CuponModel.Id_Tipo_Cupon }, tipo_CuponModel);
        }

        // DELETE: api/Tipo_Cupon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_CuponModel(int id)
        {
            var tipo_CuponModel = await _context.Tipo_Cupon.FindAsync(id);
            if (tipo_CuponModel == null)
            {
                Log.Error($"Tipo_Cupon con ID {id} no existe para borrar.");
                return NotFound();
            }

            _context.Tipo_Cupon.Remove(tipo_CuponModel);
            await _context.SaveChangesAsync();

            Log.Information($"Tipo_Cupon con ID {id} borrado exitosamente.");
            return NoContent();
        }

        private bool Tipo_CuponModelExists(int id)
        {
            return _context.Tipo_Cupon.Any(e => e.Id_Tipo_Cupon == id);
        }
    }
}
