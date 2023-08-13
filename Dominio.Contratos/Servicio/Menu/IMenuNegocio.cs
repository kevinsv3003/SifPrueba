
using Aplicacion.EntidadesDto;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Contratos.Servicio
{
    public interface IMenuNegocio
    {
        Task<List<Menu>> ObtenerMenus();
        Task<List<Menu>> ObtenerMenusPadrePorRol(Guid RolId);
        Task<List<Menu>> ObtenerAccesosPorRol(Guid RolId);
        Task<List<Menu>> ObtenerMenusHijoPorMenu(Guid MenuId);
   
    }
}
