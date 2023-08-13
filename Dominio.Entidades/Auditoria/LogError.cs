using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidades
{
    [Table("LogError", Schema = "AUDI")]
    public class LogError
    {
        [Key]
        public int LogErrorID { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Usuario { get; set; }

        [Required]
        public DateTime? FechaProceso { get; set; }

        [Required]
        public string ProgramaProcedimiento { get; set; }

        [Required]
        public string DescripcionErrorBD { get; set; }

        [Required]
        public string Variables { get; set; }
    }
}
