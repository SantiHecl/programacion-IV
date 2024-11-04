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
        public async Task<ActionResult<CuponModel>> SolicitudCupon([FromBody] SolicitudCuponesModel solicitudCuponesModel)
        {
            Cupon_ClienteModel cupon_Cliente = new Cupon_ClienteModel();

            cupon_Cliente.id_Cupon = solicitudCuponesModel.Id_Cupon;
            cupon_Cliente.CodCliente = solicitudCuponesModel.CodCliente;
            cupon_Cliente.NroCupon = GenerarNroCupon();
            cupon_Cliente.FechaAsignado = DateTime.Now;

            _context.Cupones_Clientes.Add(cupon_Cliente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return Ok($"Se le asignó el cupon {cupon_Cliente.NroCupon} al cliente {solicitudCuponesModel.CodCliente}");
        }

        //Funcion para crear un NroCupon random
        private string GenerarNroCupon()
        {
            Random random = new Random();
            return $"{random.Next(100, 1000)}-{random.Next(100, 1000)}-{random.Next(100, 1000)}";
        }

        [HttpPost("QuemadoCupon")]
        public async Task<ActionResult<Cupones_HistorialModel>> PostQuemadoCupon(string nroCupon)
        {
            Cupon_ClienteModel cuponCliente = await _context.Cupones_Clientes
            .FirstOrDefaultAsync(c => c.NroCupon == nroCupon);

            if (cuponCliente == null)
            {
                return NotFound("El cupón no existe o ya fue utilizado.");
            }

            Cupones_HistorialModel cupones_Historial = new Cupones_HistorialModel
            {
                CodCliente = cuponCliente.CodCliente,
                id_Cupon = cuponCliente.id_Cupon,
                NroCupon = cuponCliente.NroCupon,
                FechaUso = DateTime.Now
            };
            _context.Cupones_Historial.Add(cupones_Historial);

            _context.Cupones_Clientes.Remove(cuponCliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return Ok($"El cupon {nroCupon} fue utilizado correctamente");//CreatedAtAction("GetCupones_HistorialModel", new { id = Cupones_HistorialModel.NroCupon }, Cupones_HistorialModel);
        }
    }
}
