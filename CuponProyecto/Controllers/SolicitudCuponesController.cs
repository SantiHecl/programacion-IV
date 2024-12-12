using CuponProyecto.Models.DTO;
using CuponProyecto.Data;
using CuponProyecto.Interfaces;
using CuponProyecto.Models;
using CuponProyecto.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;


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
                if (string.IsNullOrEmpty(clienteDto.CodCliente))
                {
                    Log.Warning($"El código del cliente no puede estar vacío.");
                    throw new Exception("El código del cliente no puede estar vacío");
                }
                Log.Information($"Se solicito el cupón del cliente {clienteDto.CodCliente} exitosamente.");
                string nroCupon = await _cuponesServices.GenerarNroCupon();

                Cupon_ClienteModel cupon_Cliente = new Cupon_ClienteModel()
                {
                    id_Cupon = clienteDto.IdCupon,
                    CodCliente = clienteDto.CodCliente,
                    FechaAsignado = DateTime.Now,
                    NroCupon = nroCupon
                };

                
                _context.Cupones_Clientes.Add(cupon_Cliente);
                await _context.SaveChangesAsync();
                await _sendEmailService.EnviarEmailCliente(clienteDto.Email, nroCupon);
                Log.Information($"Cupón creado y correo enviado exitosamente para el cliente con el código {clienteDto.CodCliente}");
                return Ok(new
                {
                    Mensaje = "Se dio de alta el registro",
                    NroCupon = nroCupon
                });
            }
            catch (Exception ex)
            {
                Log.Error($"Error en la solicitud para el cliente con código {clienteDto.CodCliente}");
                return BadRequest(ex.Message);
            }           
        }

        [HttpPost("QuemadoCupon")]
        public async Task<IActionResult> QuemadoCupon([FromBody] string nroCupon)
        {
            try
            {
                Cupon_ClienteModel cuponCliente = await _context.Cupones_Clientes.FirstOrDefaultAsync(c => c.NroCupon == nroCupon);

                if (cuponCliente == null)
                {
                    Log.Warning($"El cupón con número {nroCupon} no existe o ya fue utilizado.");
                    return NotFound("El cupón no existe o ya fue utilizado.");
                }

                Log.Information($"Cupón con el numero {nroCupon} encontrado.");
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
                Log.Information($"El cupón con el número {nroCupon} fue quemado exitosamente.");
                return Ok(new
                {
                    Mensaje = "Se ha quemado el cupon",
                    NroCupon = nroCupon
                });
            }
            catch (Exception ex)
            {
                Log.Error($"Error al quemaro el cupón con el número {nroCupon}");
                return BadRequest(ex.Message);
            }
        }
    }
}
