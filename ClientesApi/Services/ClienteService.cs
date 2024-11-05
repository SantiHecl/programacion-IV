using ClientesApi.Interfaces;
using ClientesApi.Models;
using ClientesApi.Models.DTO;
using Newtonsoft.Json;
using System.Text;

namespace ClientesApi.Services
{
    public class ClienteService : IClienteService
    {
        public async Task SolicitarCupon()
        {
            try
            {
                ClienteDto clienteDto = new ClienteDto()
                {
                    Id_Cupon = 1,
                    NroCupon = "123-456-789",
                    FechaAsignado = DateTime.Now,
                    CodCliente = "13245679"
                };
                var jsonCliente = JsonConvert.SerializeObject(clienteDto);
                var contenido = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
                var cliente = new HttpClient();
                var respuesta = await cliente.PostAsync("https://localhost:7040/api/Cupon_Cliente", contenido);

                if(respuesta.IsSuccessStatusCode)
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
