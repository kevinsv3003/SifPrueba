using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidades
{
    [Table("Usuarios", Schema = "SEG")]
    public class UsuarioApp : IdentityUser<Guid>
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public int RentaId { get; set; }

    }
}
