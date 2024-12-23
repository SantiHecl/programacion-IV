﻿using System;
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
    public class CuponesController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public CuponesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Cupones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuponModel>>> GetCupones()
        {
            try
            {
                Log.Information($"Se llamo a GetCupones");
                return await _context.Cupones.Include(c => c.Cupones_Categorias)
                    .ThenInclude(cc => cc.Categoria).Include(c => c.Tipo_Cupon).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"GetCupones error, {ex.Message}");
                return BadRequest($"Hubo un problema en GetCupones, error {ex.Message}");
            }
        }

        [HttpGet("codCliente")]
        public async Task<ActionResult<Cupon_ClienteModel>> GetCuponPorCodCliente(int codCliente)
        {
            try
            {
                var cuponModel = await _context.Cupones_Clientes.FindAsync(codCliente);

                if (cuponModel == null)
                {
                    Log.Error($"GetCuponPorCodCliente No existe el cupón con ese código {codCliente}");
                    return NotFound();
                }
                Log.Information($"Se llamo a GetCuponesPorCodCliente");
                return cuponModel;
            }
            catch (Exception ex)
            {
                Log.Error($"GetCuponesPorCodCliente error, {ex.Message}");
                return BadRequest($"Hubo un problema en GetCuponesPorCodCliente, error {ex.Message}");
            }
            
        }  

        // GET: api/Cupones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CuponModel>> GetCuponModel(int id)
        {
            try
            {
                var cuponModel = await _context.Cupones.FindAsync(id);

                if (cuponModel == null)
                {
                    Log.Error($"GetCuponId No existe el cupón con esa id, {id}");
                    return NotFound();
                }
                Log.Information($"Se llamo a GetCuponId");
                return cuponModel;
            }
            catch (Exception ex)
            {
                Log.Error($"GetCuponId error, {ex.Message}");
                return BadRequest($"Hubo un problema en GetCuponId, error {ex.Message}");
            }
        }

        // PUT: api/Cupones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuponModel(int id, CuponModel cuponModel)
        {
            if (id != cuponModel.id_Cupon)
            {
                Log.Warning($"ID en la ruta ({id}) no coincide con el ID del cupón ({cuponModel.id_Cupon})");
                return BadRequest();
            }

            _context.Entry(cuponModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information($"Cupón con ID {id} actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuponModelExists(id))
                {
                    Log.Error($"No existe el cupón con esa id para actualizar, {id}");
                    return NotFound();
                }
                else
                {
                    Log.Error($"Error al actualizar el cupón con ID {id}");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cupones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CuponModel>> PostCuponModel(CuponModel cuponModel)
        {
            try
            {
                _context.Cupones.Add(cuponModel);
                await _context.SaveChangesAsync();

                Log.Information($"Cupón creado exitosamente.");
                return CreatedAtAction("GetCuponModel", new { id = cuponModel.id_Cupon }, cuponModel);
            }
            catch (Exception ex)
            {
                Log.Error($"PostCupon error, {ex.Message}");
                return BadRequest($"Hubo un problema en PostCuponModel, error {ex.Message}");
            }
           
        }

        // DELETE: api/Cupones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuponModel(int id)
        {
            try
            {
                var cuponModel = await _context.Cupones.FindAsync(id);
                if (cuponModel == null)
                {
                    Log.Error($"Cupón con ID {id} no existe para borrar.");
                    return NotFound();
                }

                //_context.Cupones.Remove(cuponModel);
                cuponModel.Activo = false;
                await _context.SaveChangesAsync();

                Log.Information($"Cupón con ID {id} borrado exitosamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error($"DeleteCupon error, {ex.Message}");
                return BadRequest($"Hubo un problema en DeleteCupon, error {ex.Message}");
            }
        }

        private bool CuponModelExists(int id)
        {
            return _context.Cupones.Any(e => e.id_Cupon == id);
        }
    }
}
