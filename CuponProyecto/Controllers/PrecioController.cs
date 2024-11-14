using CuponProyecto.Data;
using CuponProyecto.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuponProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecioController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public PrecioController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/<PrecioController>
        [HttpGet]
        public async Task<IActionResult> GetPrecios()
        {
            var articulos = await _context.Articulos
                .Include(c => c.Precio)
                .Select(c => new
                {
                    c.Id_Articulo,
                    c.Nombre_Articulo,
                    c.Descripcion_Articulo,
                    c.Activo,
                    Precio = c.Precio != null ? c.Precio.Precio : 0
                })
                .ToListAsync();

            return Ok(articulos);
        }

        // POST api/<PrecioController>
        [HttpPost]
        public async Task<ActionResult> PostPrecio(int idArticulo, [FromBody] decimal precio)
        {
            var articulo = _context.Articulos.FindAsync(idArticulo);

            if (articulo == null)
                return NotFound("Artículo no encontrado.");

            PrecioModel precioModel = new PrecioModel { Precio = precio, Id_Articulo = idArticulo };
            _context.Precios.Add(precioModel);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Precio creado y asignado al artículo", Precio = precio });
        }

        // PUT api/<PrecioController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPrecio(int idArticulo, [FromBody] decimal nuevoPrecio)
        {
            var precio = await _context.Precios.FindAsync(idArticulo);
            if (precio == null)
                return NotFound("AArtículo no encontrado.");

            precio.Precio = nuevoPrecio;
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Precio modificado", Precio = nuevoPrecio });
        }

        // DELETE api/<PrecioController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePrecio(int idArticulo)
        {
            var precio = await _context.Precios.FindAsync(idArticulo);

            if (precio == null)
                return NotFound("Artículo no encontrado.");

            precio.Precio = 0;

            var articulo = await _context.Articulos.FindAsync(precio.Id_Articulo);
            articulo.Precio = precio;

            _context.Precios.Remove(precio);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Precio eliminado. El artículo ahora tiene precio 0." });
        }
    }
}
