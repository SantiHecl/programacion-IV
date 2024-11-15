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
    public class Cupon_CategoriaController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public Cupon_CategoriaController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Cupon_Categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupon_CategoriaModel>>> GetCupon_CategoriaModel()
        {
            Log.Information($"Se llamo a GetCuponCategoria");
            return await _context.Cupones_Categorias.ToListAsync();
        }

        // GET: api/Cupon_Categoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cupon_CategoriaModel>> GetCupon_CategoriaModel(int id)
        {
            var cupon_CategoriaModel = await _context.Cupones_Categorias.FindAsync(id);

            if (cupon_CategoriaModel == null)
            {
                Log.Error($"GetCupon_CategoriaId No existe el articulo con esa id, {id}");
                return NotFound();
            }
            Log.Information($"Se llamo a GetCupon_CategoriaId");
            return cupon_CategoriaModel;
        }

        // PUT: api/Cupon_Categoria/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupon_CategoriaModel(int id, Cupon_CategoriaModel cupon_CategoriaModel)
        {
            if (id != cupon_CategoriaModel.Id_Cupones_Categorias)
            {
                Log.Warning($"ID en la ruta ({id}) no coincide con el ID del cupon ({cupon_CategoriaModel.Id_Cupones_Categorias})");
                return BadRequest();
            }

            _context.Entry(cupon_CategoriaModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information($"Cupones_Categorias con ID {id} actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cupon_CategoriaModelExists(id))
                {
                    Log.Error($"No existe el Cupon_Categoria con esa id para actualizar, {id}");
                    return NotFound();
                }
                else
                {
                    Log.Error($"Error al actualizar el Cupon_Categoria con ID {id}");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cupon_Categoria
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cupon_CategoriaModel>> PostCupon_CategoriaModel(Cupon_CategoriaModel cupon_CategoriaModel)
        {
            _context.Cupones_Categorias.Add(cupon_CategoriaModel);
            await _context.SaveChangesAsync();

            Log.Information($"Cupon_Categoria creado exitosamente.");
            return CreatedAtAction("GetCupon_CategoriaModel", new { id = cupon_CategoriaModel.Id_Cupones_Categorias }, cupon_CategoriaModel);
        }

        // DELETE: api/Cupon_Categoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupon_CategoriaModel(int id)
        {
            var cupon_CategoriaModel = await _context.Cupones_Categorias.FindAsync(id);
            if (cupon_CategoriaModel == null)
            {
                Log.Error($"Cupon_Categoria con ID {id} no existe para borrar.");
                return NotFound();
            }

            _context.Cupones_Categorias.Remove(cupon_CategoriaModel);
            await _context.SaveChangesAsync();

            Log.Information($"Cupon_Categoria con ID {id} borrado exitosamente.");
            return NoContent();
        }

        private bool Cupon_CategoriaModelExists(int id)
        {
            return _context.Cupones_Categorias.Any(e => e.Id_Cupones_Categorias == id);
        }
    }
}
