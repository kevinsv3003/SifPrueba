using Aplicacion.EntidadesDto;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Contratos.Servicio
{
    public interface IMenuRolNegocio
    {
        Task<bool> InsertarMenuRol(RolesMenu rolMenu);
        Task<bool> EliminarMenuRol(Guid RolesMenuId);
        Task<bool> RolTieneAccesoMenu(Guid MenuId, Guid RolId);
        Task<List<RolesMenu>> BuscarAcceso(Guid MenuId, Guid RolId);

    }
}
