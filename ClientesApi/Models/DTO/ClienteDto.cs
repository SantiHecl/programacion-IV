using System.ComponentModel.DataAnnotations;

namespace ClientesApi.Models.DTO
{
    public class ClienteDto
    {
        public int Id_Cupon { get; set; }
        public string NroCupon { get; set; }
        public DateTime FechaAsignado { get; set; }
        public string CodCliente { get; set; }
    }
}
