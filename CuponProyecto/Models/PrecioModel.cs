using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuponProyecto.Models
{
    public class PrecioModel
    {
        [Key]
        public int Id_Precio { get; set; }
       
        public int Id_Articulo { get; set; }
        public decimal Precio { get; set; }

        [ForeignKey("Id_Articulo")]
        public virtual ArticuloModel? Articulo { get; set; }
    }
}
