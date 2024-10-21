﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CuponProyecto.Data;
using CuponProyecto.Models;

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
            return await _context.Articulos.Include(a => a.Precio).ToListAsync();
        }

        // GET: api/Articulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticuloModel>> GetArticuloModel(int id)
        {
            var articuloModel = await _context.Articulos.FindAsync(id);

            if (articuloModel == null)
            {
                return NotFound();
            }

            return articuloModel;
        }

        // PUT: api/Articulos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticuloModel(int id, ArticuloModel articuloModel)
        {
            if (id != articuloModel.Id_Articulo)
            {
                return BadRequest();
            }

            _context.Entry(articuloModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticuloModelExists(id))
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

        // POST: api/Articulos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticuloModel>> PostArticuloModel(ArticuloModel articuloModel)
        {
            _context.Articulos.Add(articuloModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticuloModel", new { id = articuloModel.Id_Articulo }, articuloModel);
        }

        // DELETE: api/Articulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticuloModel(int id)
        {
            var articuloModel = await _context.Articulos.FindAsync(id);
            if (articuloModel == null)
            {
                return NotFound();
            }

            _context.Articulos.Remove(articuloModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticuloModelExists(int id)
        {
            return _context.Articulos.Any(e => e.Id_Articulo == id);
        }
    }
}
