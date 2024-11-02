using CuponProyecto.Data;
using CuponProyecto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuponProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudCuponesController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public SolicitudCuponesController(DataBaseContext context)
        {
            _context = context;
        }

        // POST: api/Cupones
        [HttpPost]
      // public async Task<ActionResult<CuponModel>>


        [HttpPost]
        public async Task<ActionResult<Cupones_HistorialModel>> PostQuemadoCupon(Cupones_HistorialModel Cupones_HistorialModel, int id_Cupon, String NroCupon, String CodCliente)
        {
            Cupones_HistorialModel.id_Cupon = id_Cupon;
            Cupones_HistorialModel.NroCupon = NroCupon;
            Cupones_HistorialModel.CodCliente = CodCliente;
            Cupones_HistorialModel.FechaUso = DateTime.Now;

            _context.Cupones_Historial.Add(Cupones_HistorialModel);
            //falta agregar lo de Eliminar registro en Cupones_Clientes.
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return Ok(Cupones_HistorialModel);//CreatedAtAction("GetCupones_HistorialModel", new { id = Cupones_HistorialModel.NroCupon }, Cupones_HistorialModel);
        }

    }
}
