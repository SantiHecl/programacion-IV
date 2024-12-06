using ClientesApi.Data;
using ClientesApi.Interfaces;
using ClientesApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
using Serilog;
>>>>>>> 316cb7f31fa00f51f0f31385a10f9796c0397bce

namespace ClientesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cliente_CuponController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly IClienteService _clienteService;

        public Cliente_CuponController(DataBaseContext context, IClienteService clienteService)
        {
            _context = context;
            _clienteService = clienteService;
        }

        [HttpPost("EnviarSolicitudACupones")]
        public async Task<IActionResult> ReclamarCupon([FromBody] ClienteDto clienteDto)
        {
            try
            {

                var respuesta = await _clienteService.SolicitarCupon(clienteDto);
                Log.Information($"Se solicito su cupón exitosamente.");
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un error con la solicitud de su cupón.");
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("UsarCupon")]
        public async Task<IActionResult> UsarCupon(string nroCupon)
        {
            try
            {
<<<<<<< HEAD
                var respuesta = await _clienteService.QuemadoCupon(nroCupon);
=======
                var respuesta = await _clienteService.QuemadoCupon(clienteDto);
                Log.Information($"Su cupon fue usado exitosamente.");
>>>>>>> 316cb7f31fa00f51f0f31385a10f9796c0397bce
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un error al usar su cupón.");
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCuponesActivos(string codCliente)
        {
            try
            {
<<<<<<< HEAD
                var respuesta = await _clienteService.SolicitarCupon
=======
                var respuesta = await _context.Clientes.FindAsync(codCliente);
                Log.Information($"Se llamo a ObtenerCuponesActivos");
>>>>>>> 316cb7f31fa00f51f0f31385a10f9796c0397bce
                return Ok(respuesta);

            }catch (Exception ex)
            {
                Log.Error($"Error al obtener los cupones activos para el cliente con el código: {codCliente}");
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
