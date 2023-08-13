﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidades
{
    [Table("Rentas", Schema = "SEG")]

    public class Renta
    {
        [Key]
        public int RentaId { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string NombreRenta { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Descripcion { get; set; }

        public bool EsBorrado { get; set; }
    }
}
