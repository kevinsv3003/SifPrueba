using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.EntidadesDto
{
    public class SubMenuDto : MenuDto
    {      
        public Guid PadreId { get; set; }
    }
}
