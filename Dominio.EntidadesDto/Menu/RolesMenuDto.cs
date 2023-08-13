using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.EntidadesDto
{
    public class RolesMenuDto
    {
        public Guid RolesMenuId { get; set; }

        public Guid RolId { get; set; }

        public Guid MenuId { get; set; }

        public bool EstaAnulado { get; set; }


    }
}
