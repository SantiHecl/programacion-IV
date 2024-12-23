﻿using ClientesApi.Data;
using ClientesApi.Interfaces;
using ClientesApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
        public async Task<IActionResult> UsarCupon([FromBody] string nroCupon)
        {
            try
            {
                var respuesta = await _clienteService.QuemadoCupon(nroCupon);

                Log.Information($"Se llamo a post UsarCupon");
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un error al usar su cupón.");
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("ObtenerCuponesActivos")]
        public async Task<IActionResult> ObtenerCuponesActivos(string codCliente)
        {
            try
            {
                var respuesta = await _context.Cupones_Clientes
                    .Where(c => c.CodCliente == codCliente)
                    .ToListAsync();

                Log.Information($"Se llamo a ObtenerCuponesActivos");
                return Ok(respuesta);

            }catch (Exception ex)
            {
                Log.Error($"Error al obtener los cupones activos para el cliente con el código: {codCliente}");
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
