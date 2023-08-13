using Aplicacion.EntidadesDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Contrato
{
   public interface IMenuAplicacion
    {
        Task<List<MenuDto>> ObtenerMenus();
        Task<List<MenuDto>> ObtenerMenusPorRol(Guid RolId);
        Task<List<MenuDto>> ObtenerAccesosPorRol(Guid RolId);
        Task<List<SubMenuDto>> BuscarSubMenusPorMenu(Guid MenuId, Guid RolId);
    }
}
