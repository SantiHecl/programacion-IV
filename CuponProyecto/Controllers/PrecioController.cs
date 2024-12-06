using CuponProyecto.Data;
using CuponProyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
        public async Task<ActionResult<IEnumerable<PrecioModel>>> GetPrecios()
        {
            try
            {
                Log.Information($"Se llamo a GetPrecios");
                return await _context.Precios.Include(a => a.Articulo).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"GetPrecios error, {ex.Message}");
                return BadRequest($"Hubo un problema en GetPrecios, error {ex.Message}");
            }
        }

        // POST api/<PrecioController>
        [HttpPost]
        public async Task<ActionResult<PrecioModel>> PostPrecio(PrecioModel precioModel)
        {
            try
            {
                _context.Precios.Add(precioModel);
                await _context.SaveChangesAsync();

                Log.Information($"Se le asigno el precio {precioModel.Precio} para el artículo con id {precioModel.Id_Articulo} exitosamente.");
                return Ok(new { Message = "Precio creado y asignado al artículo", PrecioA = precioModel.Precio });

            }
            catch (Exception ex)
            {
                Log.Error($"PostPrecio error, {ex.Message}");
                return BadRequest($"Hubo un problema en PostPrecio, error {ex.Message}");
            }
        }

        // PUT api/<PrecioController>/5
        [HttpPut("{idPrecio}")]
        public async Task<ActionResult> PutPrecio(int idPrecio, PrecioModel precioModel)
        {
            if (idPrecio != precioModel.Id_Articulo)
            {
                Log.Warning($"ID en la ruta ({idPrecio}) no coincide con el ID del artículo ({precioModel.Id_Articulo})");
                return BadRequest();
            }

            _context.Entry(precioModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information($"Se le actualizo el precio del artículo con ID {precioModel.Id_Articulo} a un precio de {precioModel.Precio} exitosamente. ");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrecioModelExists(idPrecio))
                {
                    Log.Error($"No existe el precio con esa id para actualizar, {idPrecio}");
                    return NotFound();
                }
                else
                {
                    Log.Error($"Error al actualizar el precio con ID {idPrecio}");
                    throw;
                }
            }

            return Ok(new { Message = "Precio modificado", Precio = precioModel.Precio });
        }

        // DELETE api/<PrecioController>/5
        [HttpDelete("{idPrecio}")]
        public async Task<IActionResult> DeletePrecio(int idPrecio)
        {
            try
            {
                var precio = await _context.Precios.FindAsync(idPrecio);

                if (precio == null)
                {
                    Log.Error($"El artículo con el ID {idPrecio} no existe para borrar el precio.");
                    return NotFound("Artículo no encontrado.");
                }

                var articulo = await _context.Articulos.FindAsync(precio.Id_Articulo);
                articulo.Precio.Precio = 0;
                await _context.SaveChangesAsync();

                Log.Information($"Se elimino exitosamente el precio del artículo con el ID {idPrecio}.El artículo ahora tiene precio 0.");
                return Ok(new { Message = "Precio eliminado. El artículo ahora tiene precio 0." });
            }
            catch (Exception ex)
            {
                Log.Error($"DeletePrecio error, {ex.Message}");
                return BadRequest($"Hubo un problema en DeletePrecio, error {ex.Message}");
            }
        }

        private bool PrecioModelExists(int id)
        {
            return _context.Precios.Any(e => e.Id_Precio == id);
        }
    }
}
