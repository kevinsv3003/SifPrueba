using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidades
{
    [Table("RolesMenu", Schema = "MENU")]
    public class RolesMenu
    {
        [Key]
        [Column("RolesMenuId")]
        public Guid RolesMenuId { get; set; }
        [Column("RoleId")]
        public Guid RolId { get; set; }
        [Column("MenuId")]
        public Guid MenuId { get; set; }
        [Column("EstaAnulado")]
        public bool EstaAnulado { get; set; }
        public virtual RolApp Rol { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
