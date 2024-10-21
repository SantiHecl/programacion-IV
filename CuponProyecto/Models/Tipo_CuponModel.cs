﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuponProyecto.Models
{
    [Table ("Tipo_Cupon")]
    public class Tipo_CuponModel
    {
        [Key]
        public int Id_Tipo_Cupon { get; set; }
        public string Nombre { get; set;}
    }
}
