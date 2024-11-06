using ClientesApi.Interfaces;
using ClientesApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cliente_CuponController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public Cliente_CuponController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        } 

        [HttpPost("EnviarSolicitudACupones")]
        public async Task<IActionResult> EnviarSolicitudACupones([FromBody] ClienteDto clienteDto)
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
        public async Task<IActionResult> UsarCupon([FromBody] ClienteDto clienteDto)
        {
            try
            {
                var respuesta = await _clienteService.QuemadoCupon(clienteDto);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
