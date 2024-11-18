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
    public class CategoriasController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public CategoriasController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> GetCategorias()
        {
            try
            {
                Log.Information($"Se llamo a GetCategoria");
                return await _context.Categorias.Include(a => a.Cupones_Categorias).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"GetCategorias error, {ex.Message}");
                return BadRequest($"Hubo un problema en GetCategorias, error {ex.Message}");
            }
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaModel>> GetCategoriaModel(int id)
        {
            try
            {
                var categoriaModel = await _context.Categorias.FindAsync(id);

                if (categoriaModel == null)
                {
                    Log.Error($"no existe id {id} en GetCategoriaId");
                    return NotFound();
                }
                Log.Information($"Se llamo a GetCategoriaId");
                return categoriaModel;
            }
            catch (Exception ex)
            {
                Log.Error($"GetCategoriaId error, {ex.Message}");
                return BadRequest($"Hubo un problema en GetCategoriaId, error {ex.Message}");
            }
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaModel(int id, CategoriaModel categoriaModel)
        {
            if (id != categoriaModel.Id_Categoria)
            {
                Log.Warning($"ID en la ruta ({id}) no coincide con el ID de la categoria ({categoriaModel.Id_Categoria})");
                return BadRequest();
            }

            _context.Entry(categoriaModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information($"Se creo Categoria");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaModelExists(id))
                {
                    Log.Error($"No existe la categoria con esa id para actualizar, {id}");
                    return NotFound();
                }
                else
                {
                    Log.Error($"Error al actualizar la categoria con ID {id}");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriaModel>> PostCategoriaModel(CategoriaModel categoriaModel)
        {
            try
            {
                _context.Categorias.Add(categoriaModel);
                await _context.SaveChangesAsync();

                Log.Information($"Categoria creada exitosamente.");
                return CreatedAtAction("GetCategoriaModel", new { id = categoriaModel.Id_Categoria }, categoriaModel);
            }
            catch (Exception ex)
            {
                Log.Error($"PostCategoria error, {ex.Message}");
                return BadRequest($"Hubo un problema en PostCategoria, error {ex.Message}");
            }
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaModel(int id)
        {
            try
            {
                var categoriaModel = await _context.Categorias.FindAsync(id);
                if (categoriaModel == null)
                {
                    Log.Error($"Artículo con ID {id} no existe para borrar.");
                    return NotFound();
                }

                _context.Categorias.Remove(categoriaModel);
                await _context.SaveChangesAsync();

                Log.Information($"Se elimino Categoria");
                return NoContent();
            }
            catch (Exception ex) {
                Log.Error($"DeleteCategoria error, {ex.Message}");
                return BadRequest($"Hubo un problema en DeleteCategoria, error {ex.Message}");
            }
        }

        private bool CategoriaModelExists(int id)
        {
            return _context.Categorias.Any(e => e.Id_Categoria == id);
        }
    }
}
