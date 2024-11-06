using CuponProyecto.Models.DTO;
using CuponProyecto.Data;
using CuponProyecto.Interfaces;
using CuponProyecto.Models;
using CuponProyecto.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace CuponProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudCuponesController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly ICuponesServices _cuponesServices;
        private readonly ISendEmailService _sendEmailService;

        public SolicitudCuponesController(DataBaseContext context, ICuponesServices cuponesServices, ISendEmailService sendEmailService)
        {
            _context = context;
            _cuponesServices = cuponesServices;
            _sendEmailService = sendEmailService;
        }

        // POST: api/Cupones
        [HttpPost("SolicitarCupon")]
        public async Task<IActionResult> SolicitarCupon(ClienteDto clienteDto)
        {
            try
            {
                if (clienteDto.Cod_Cliente.IsNullOrEmpty())
                    throw new Exception("El Dni del cliente no puede estar vacío");

                string nroCupon = await _cuponesServices.GenerarNroCupon();

                Cupon_ClienteModel cupon_Cliente = new Cupon_ClienteModel()
                {
                    id_Cupon = clienteDto.Id_Cupon,
                    CodCliente = clienteDto.Cod_Cliente,
                    FechaAsignado = DateTime.Now,
                    NroCupon = nroCupon
                };

                _context.Cupones_Clientes.Add(cupon_Cliente);
                await _context.SaveChangesAsync();
                await _sendEmailService.EnviarEmailCliente(clienteDto.Email, nroCupon);
                return Ok(new
                {
                    Mensaje = "Se dio de alta el registro",
                    NroCupon = nroCupon
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        [HttpPost("QuemadoCupon")]
        public async Task<IActionResult> QuemadoCupon(string nroCupon)
        {
            try
            {
                Cupon_ClienteModel cuponCliente = await _context.Cupones_Clientes
                .FirstOrDefaultAsync(c => c.NroCupon == nroCupon);

                if (cuponCliente == null)
                {
                    return NotFound("El cupón no existe o ya fue utilizado.");
                }

                Cupones_HistorialModel cupones_Historial = new Cupones_HistorialModel()
                {
                    CodCliente = cuponCliente.CodCliente,
                    id_Cupon = cuponCliente.id_Cupon,
                    NroCupon = cuponCliente.NroCupon,
                    FechaUso = DateTime.Now
                };

                _context.Cupones_Historial.Add(cupones_Historial);
                _context.Cupones_Clientes.Remove(cuponCliente);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Mensaje = "Se ha quemado el cupon",
                    NroCupon = nroCupon
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
