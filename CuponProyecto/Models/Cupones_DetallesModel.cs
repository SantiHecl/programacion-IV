using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuponProyecto.Models
{
    public class Cupones_DetallesModel
    {
        [Key]
        public int Id_Cupon {get; set;}
        [Key]
        public int Id_Articulo { get; set;}
        public int Cantidad { get; set;}
    }
}
