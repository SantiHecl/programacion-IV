using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuponProyecto.Models
{
    public class Cupones_DetallesModel
    {
        [Key]
        public int id_Cupon {get; set;}
        [Key]
        public int id_Articulo { get; set;}
        public int Cantidad { get; set;}
    }
}
