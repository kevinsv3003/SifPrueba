using Aplicacion.Contrato;
using Aplicacion.EntidadesDto;
using AutoMapper;
using Dominio.Contratos.Servicio;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios.MenuServicios
{
    public class MenuRolAplicacion : IMenuRolAplicacion
    {
        private readonly IMenuRolNegocio _menuRolNegocio;

        public MenuRolAplicacion(IMenuRolNegocio _menuRolNegocio)
        {
            this._menuRolNegocio = _menuRolNegocio;
        }
        public async Task<List<RolesMenuDto>> BuscarAcceso(Guid MenuId, Guid RolId)
        {
            try
            {
                var acceso = await _menuRolNegocio.BuscarAcceso(MenuId, RolId);
                var accesoDto = Mapper.Map<List<RolesMenu>, List<RolesMenuDto>>(acceso);
                return accesoDto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<bool> EliminarMenuRol(Guid RolesMenuId)
        {
            try
            {
                var retorno  = await _menuRolNegocio.EliminarMenuRol(RolesMenuId);
                return retorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<bool> InsertarMenuRol(RolesMenuDto rolMenu)
        {
            try
            {
                var rolMenuDb = Mapper.Map<RolesMenuDto, RolesMenu>(rolMenu);
                var retorno = await _menuRolNegocio.InsertarMenuRol(rolMenuDb);
                return retorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
