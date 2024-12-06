using ClientesApi.Data;
using ClientesApi.Interfaces;
using ClientesApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("UsarCupon")]
        public async Task<IActionResult> UsarCupon(string nroCupon)
        {
            try
            {
                var respuesta = await _clienteService.QuemadoCupon(nroCupon);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCuponesActivos(string codCliente)
        {
            try
            {
                var respuesta = await _clienteService.SolicitarCupon
                return Ok(respuesta);

            }catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
