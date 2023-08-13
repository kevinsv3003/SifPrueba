using Aplicacion.EntidadesDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Contrato
{
    public interface IMenuRolAplicacion
    {
        Task<bool> InsertarMenuRol(RolesMenuDto rolMenu);
        Task<bool> EliminarMenuRol(Guid RolesMenuId);
        Task<List<RolesMenuDto>> BuscarAcceso(Guid MenuId, Guid RolId);
    }
}
