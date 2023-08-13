using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidades
{
    [Table("Menu", Schema = "MENU")]
    public class Menu
    {

        [Key]
        [Column("MenuId")]
        public Guid MenuId { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Descripcion")]
        public string Descripcion { get; set; }
        [Column("Area")]
        public string Area { get; set; }
        [Column("Controlador")]
        public string Controlador { get; set; }
        [Column("NombreVista")]
        public string Vista { get; set; }
        [Column("Accion")]
        public string Accion { get; set; }
        [Column("esPrincipal")]
        public bool esPrincipal { get; set; }
        [Column("EstaAnulado")]
        public bool EstaAnulado { get; set; }
        [Column("IconoCss")]
        public string IconoCss { get; set; }
        public int Posicion { get; set; }

        public Guid PadreId { get; set; }



    }
}
