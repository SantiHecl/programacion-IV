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
    public class Cupones_HistorialController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public Cupones_HistorialController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Cupones_Historial
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupones_HistorialModel>>> GetCupones_Historial()
        {
            try
            {
                Log.Information($"Se llamo a GetCuponesHistorial");
                return await _context.Cupones_Historial.ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"GetCuponesHistorial error, {ex.Message}");
                return BadRequest($"Hubo un problema en GetCuponesHistorial, error {ex.Message}");
            }
            
        }

        // GET: api/Cupones_Historial/5
        [HttpGet("{idCupon}/{nroCupon}")]
        public async Task<ActionResult<Cupones_HistorialModel>> GetCupones_HistorialModel(int idCupon, string nroCupon)
        {
            try
            {
                var cupones_HistorialModel = await _context.Cupones_Historial.FindAsync(idCupon, nroCupon);

                if (cupones_HistorialModel == null)
                {
                    Log.Error($"GetCuponesId No existe el artiuclo con esa id, {idCupon}, {nroCupon}");
                    return NotFound();
                }
                Log.Information($"Se llamo a GetCuponesId");
                return cupones_HistorialModel;
            }
            catch (Exception ex)
            {
                Log.Error($"GetCuponesHistorialId error, {ex.Message}");
                return BadRequest($"Hubo un problema en GetCuponesHistorialId, error {ex.Message}");
            }
        }

        // PUT: api/Cupones_Historial/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idCupon}/{nroCupon}")]
        public async Task<IActionResult> PutCupones_HistorialModel(int idCupon, string nroCupon, Cupones_HistorialModel cupones_HistorialModel)
        {
            if (idCupon != cupones_HistorialModel.id_Cupon && nroCupon!=cupones_HistorialModel.NroCupon)
            {
                Log.Warning($"ID en la ruta ({idCupon} no coincide con el ID del cupón ({cupones_HistorialModel.id_Cupon})");
                return BadRequest();
            }

            _context.Entry(cupones_HistorialModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information($"Cupon con ID {idCupon} actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cupones_HistorialModelExists(idCupon))
                {
                    Log.Error($"No existe el cupón con esa id para actualizar, {idCupon}");
                    return NotFound();
                }
                else
                {
                    Log.Error($"Error al actualizar el cupón con ID {idCupon}");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cupones_Historial
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cupones_HistorialModel>> PostCupones_HistorialModel(Cupones_HistorialModel cupones_HistorialModel)
        {
            
            _context.Cupones_Historial.Add(cupones_HistorialModel);
            
            try
            {
                await _context.SaveChangesAsync();
                Log.Information($"Cupón con ID {cupones_HistorialModel.id_Cupon} creado exitosamente.", cupones_HistorialModel.id_Cupon);
            }
            catch (DbUpdateException)
            {
                if (Cupones_HistorialModelExists(cupones_HistorialModel.id_Cupon))
                {
                    Log.Error($"El cupón con ID {cupones_HistorialModel.id_Cupon}. Ya existe.", cupones_HistorialModel.id_Cupon);
                    return Conflict();
                }
                else
                {
                    Log.Error($"Error al intentar crear el cupón con ID {cupones_HistorialModel.id_Cupon}.", cupones_HistorialModel.id_Cupon);
                    throw;
                }
            }
            return CreatedAtAction("GetCupones_HistorialModel",new { idCupon = cupones_HistorialModel.id_Cupon, nroCupon = cupones_HistorialModel.NroCupon },cupones_HistorialModel);
        }

        // DELETE: api/Cupones_Historial/5
        [HttpDelete("{idCupon}/{nroCupon}")]
        public async Task<IActionResult> DeleteCupones_HistorialModel(int idCupon, string nroCupon)
        {
            try
            {
                var cupones_HistorialModel = await _context.Cupones_Historial.FindAsync(idCupon, nroCupon);
                if (cupones_HistorialModel == null)
                {
                    Log.Error($"Cupón con ID {idCupon} no existe para borrar.");
                    return NotFound();
                }

                _context.Cupones_Historial.Remove(cupones_HistorialModel);
                await _context.SaveChangesAsync();

                Log.Error($"Cupón con ID {idCupon} borrado exitosamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error($"DeleteCupones_Historial error, {ex.Message}");
                return BadRequest($"Hubo un problema en DeleteCupones_Historial, error {ex.Message}");
            }
        }

        private bool Cupones_HistorialModelExists(int id)
        {
            return _context.Cupones_Historial.Any(e => e.id_Cupon == id);
        }
    }
}
