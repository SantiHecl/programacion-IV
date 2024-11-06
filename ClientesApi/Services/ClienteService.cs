using ClientesApi.Interfaces;
using ClientesApi.Models;
using ClientesApi.Models.DTO;
using Newtonsoft.Json;
using System.Text;

namespace ClientesApi.Services
{
    public class ClienteService : IClienteService
    {
        public async Task <string> SolicitarCupon(ClienteDto clienteDto)
        {
            try
            {
                //ClienteDto clienteDto = new ClienteDto()
                //{
                  //  Id_Cupon = 1,
                    //NroCupon = "123-456-789",
                    //FechaAsignado = DateTime.Now,
                   // CodCliente = "13245679"
               // };
                var jsonCliente = JsonConvert.SerializeObject(clienteDto);
                var contenido = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
                var cliente = new HttpClient();
                var respuesta = await cliente.PostAsync("https://localhost:7040/api/SolicitudCupones/SolicitudCupon", contenido);

                if (respuesta.IsSuccessStatusCode)
                {
                    var msg = await respuesta.Content.ReadAsStringAsync();
                    return msg;
                }
                else 
                { 
                    var error = await respuesta.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<string> QuemadoCupon(ClienteDto clienteDto)
        {
            try
            {
                var jsonCliente = JsonConvert.SerializeObject(clienteDto);
                var contenido = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
                var cliente = new HttpClient();
                var respuesta = await cliente.PostAsync("https://localhost:7040/api/SolicitudCupones/QuemadoCupon", contenido);

                if (respuesta.IsSuccessStatusCode)
                {
                    var msg = await respuesta.Content.ReadAsStringAsync();
                    return msg;
                }
                else
                {
                    var error = await respuesta.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
