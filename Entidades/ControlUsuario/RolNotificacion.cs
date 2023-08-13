using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidades
{
    [Table("RolesNotificaciones", Schema = "SEG")]
    public class RolNotificacion : IdentityRoleClaim<Guid>
    {
        [Column("LogId")]
        public int LogId { get; set; }

        [Column("UsuarioLogId")]
        public int UsuarioLogId { get; set; }


        [Column("EsBorrado")]
        public bool EsBorrado { get; set; }
    }
}
