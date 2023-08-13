using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidades
{
    [Table("LogTransaccion", Schema = "AUDI")]
    public class LogTransaccion
    {
        [Key]
        public int LogTransaccionID { get; set; }

        [Required]
        public string Usuario { get; set; }

        [Required]
        public DateTime? FechaProceso { get; set; }

        [Required]
        public string Accion { get; set; }

        [Required]
        public string Variables { get; set; }
    }
}
