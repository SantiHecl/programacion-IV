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
    public class Cupones_DetallesController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public Cupones_DetallesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Cupones_Detalles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupones_DetallesModel>>> GetCupones_Detalles()
        {
            try
            {
                Log.Information($"Se llamo a GetCupones_Detalles");
                return await _context.Cupones_Detalle.ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"GetCupones_Detalles error, {ex.Message}");
                return BadRequest($"Hubo un problema en GetCupones_Detalles, error {ex.Message}");
            }
        }

        // GET: api/Cupones_Detalles/5
        [HttpGet("{idCupon}/{idArticulo}")]
        public async Task<ActionResult<IEnumerable<Cupones_DetallesModel>>> GetCupones_DetallesModel(int idCupon, int idArticulo)
        {
            try
            {
                var cupones_DetallesModel = await _context.Cupones_Detalle.Where(cd => cd.id_Cupon == idCupon && cd.id_Articulo == idArticulo).ToListAsync();

                if (cupones_DetallesModel == null)
                {
                    Log.Error($"GetCupones_DetallesId No existe el cupon con esa id, {idCupon} / {idArticulo}");
                    return NotFound();
                }
                Log.Information($"Se llamo a GetCupones_DetallesId");
                return cupones_DetallesModel;
            }
            catch (Exception ex)
            {
                Log.Error($"GetCupones_DetallesId error, {ex.Message}");
                return BadRequest($"Hubo un problema en GetCupones_DetallesId, error {ex.Message}");
            }

        }

        // PUT: api/Cupones_Detalles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idCupon}/{idArticulo}")]
        public async Task<IActionResult> PutCupones_DetallesModel(int idCupon, int idArticulo, Cupones_DetallesModel cupones_DetallesModel)
        {
            if (idCupon != cupones_DetallesModel.id_Cupon)
            {
                Log.Warning($"ID en la ruta ({idCupon} /{idArticulo}) no coincide con el ID del cupon ({cupones_DetallesModel.id_Cupon})");
                return BadRequest();
            }

            _context.Entry(cupones_DetallesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information($"Cupones_Detalles con ID {idCupon}/ {idArticulo} actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cupones_DetallesModelExists(idCupon))
                {
                    Log.Error($"No existe el Cupones_Detalles con esa id para actualizar, {idCupon}/{idArticulo}");
                    return NotFound();
                }
                else
                {
                    Log.Error($"Error al actualizar el Cupones_Detalles con ID {idCupon}/{idArticulo}");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cupones_Detalles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cupones_DetallesModel>> PostCupones_DetallesModel(Cupones_DetallesModel cupones_DetallesModel)
        {
            try
            {
                _context.Cupones_Detalle.Add(cupones_DetallesModel);
                await _context.SaveChangesAsync();

                Log.Information($"Cupones_Detalles creado exitosamente.");
                return CreatedAtAction("GetCupones_DetallesModel", new { id = cupones_DetallesModel.id_Cupon }, cupones_DetallesModel);
            }
            catch (Exception ex)
            {
                Log.Error($"PostCupones_Detalles error, {ex.Message}");
                return BadRequest($"Hubo un problema en PostCupones_Detalles, error {ex.Message}");
            }
        }

        // DELETE: api/Cupones_Detalles/5
        [HttpDelete("{idCupon}/{idArticulo}")]
        public async Task<IActionResult> DeleteCupones_DetallesModel(int idCupon, int idArticulo)
        {
            try
            {
                var cupones_DetallesModel = await _context.Cupones_Detalle.FindAsync(idCupon, idArticulo);
                if (cupones_DetallesModel == null)
                {
                    Log.Error($"Cupones_Detalles con ID {idCupon}, {idArticulo} no existe para borrar.");
                    return NotFound();
                }

                _context.Cupones_Detalle.Remove(cupones_DetallesModel);
                await _context.SaveChangesAsync();

                Log.Information($"Cupones_Detalles con ID {idCupon}, {idArticulo} borrado exitosamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error($"DeleteCupones_Detalles error, {ex.Message}");
                return BadRequest($"Hubo un problema en DeleteCupones_Detalles, error {ex.Message}");
            }
        }

        private bool Cupones_DetallesModelExists(int id)
        {
            return _context.Cupones_Detalle.Any(e => e.id_Cupon == id);
        }
    }
}
