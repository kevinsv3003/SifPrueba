using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.EntidadesDto
{
    public class SubMenuDto
    {
        public Guid MenuId { get; set; }
        public string Nombre { get; set; }
        public string Area { get; set; }
        public string Controlador { get; set; }
        public string Vista { get; set; }
        public string Accion { get; set; }
        public bool esPrincipal { get; set; }
        public bool EstaAnulado { get; set; }
        public string IconoCss { get; set; }
        public int Posicion { get; set; }
        public Guid PadreId { get; set; }
        public List<SubMenuDto> SubMenus { get; set; } 

    }
}
