using System.ComponentModel.DataAnnotations;

namespace ClientesApi.Models
{
    public class Cupon_ClienteModel
    {  
        public int id_Cupon { get; set; }
        public string NroCupon { get; set; }
        public DateTime FechaAsignado { get; set; }
        public string CodCliente { get; set; }
    }
}
