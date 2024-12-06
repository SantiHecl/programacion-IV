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
    public class ArticulosController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public ArticulosController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Articulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticuloModel>>> GetArticulos()
        {
            try
            {
                Log.Information($"Se llamo a GetArticulo");
                return await _context.Articulos.Include(a => a.Precio).ToListAsync();
            }
            catch (Exception ex){
                Log.Error($"GetArticulo error, {ex.Message}");
                return BadRequest($"Hubo un problema en getArticulo, error {ex.Message}");
            }    
        }

        // GET: api/Articulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticuloModel>> GetArticuloModel(int id)
        {
            try
            {
                var articuloModel = await _context.Articulos.FindAsync(id);

                if (articuloModel == null)
                {
                    Log.Error($"GetArticuloId No existe el articulo con esa id, {id}");
                    return NotFound();
                }
                Log.Information($"Se llamo a GetArticuloId");
                return articuloModel;
            }
            catch (Exception ex)
            {
                Log.Error($"GetArticuloId error, {ex.Message}");
                return BadRequest($"Hubo un problema en getArticuloId, error {ex.Message}");
            }

        }

        // PUT: api/Articulos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticuloModel(int id, ArticuloModel articuloModel)
        {
            if (id != articuloModel.Id_Articulo)
            {
                Log.Warning($"ID en la ruta ({id}) no coincide con el ID del artículo ({articuloModel.Id_Articulo})");
                return BadRequest();
            }

            _context.Entry(articuloModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information($"Artículo con ID {id} actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticuloModelExists(id))
                {
                    Log.Error($"No existe el articulo con esa id para actualizar, {id}");
                    return NotFound();
                }
                else
                {
                    Log.Error($"Error al actualizar el artículo con ID {id}");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Articulos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticuloModel>> PostArticuloModel(ArticuloModel articuloModel)
        {
            try
            {
                _context.Articulos.Add(articuloModel);
                await _context.SaveChangesAsync();

                Log.Information($"Artículo creado exitosamente.");
                return CreatedAtAction("GetArticuloModel", new { id = articuloModel.Id_Articulo }, articuloModel);
            }
            catch (Exception ex)
            {
                Log.Error($"PostArticulo error, {ex.Message}");
                return BadRequest($"Hubo un problema en PostArticulo, error {ex.Message}");
            }
        }

        // DELETE: api/Articulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticuloModel(int id)
        {
            try {
                var articuloModel = await _context.Articulos.FindAsync(id);
                if (articuloModel == null)
                {
                    Log.Error($"Artículo con ID {id} no existe para dar de baja.");
                    return NotFound();
                }

                articuloModel.Activo = false;
                await _context.SaveChangesAsync();

                Log.Information($"Artículo con ID {id} se ha dado de baja exitosamente.");
                return NoContent();

            }
            catch (Exception ex)
            {
                Log.Error($"DeleteArticulo error, {ex.Message}");
                return BadRequest($"Hubo un problema en DeleteArticulo, error {ex.Message}");
            }
        }
        private bool ArticuloModelExists(int id)
        {
            return _context.Articulos.Any(e => e.Id_Articulo == id);
        }
    }
}
