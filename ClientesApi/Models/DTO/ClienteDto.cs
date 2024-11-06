namespace ClientesApi.Models.DTO
{
    public class ClienteDto
    {
     
        public int IdCupon { get; set; }
        public string CodCliente { get; set; }
        public string Email { get; set; }

        public DateTime FechaAsignado { get; set; }
    }
}
