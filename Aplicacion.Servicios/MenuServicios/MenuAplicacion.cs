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
    public class MenuAplicacion : IMenuAplicacion
    {
        private readonly IMenuNegocio _menuNegocio;
        private readonly IMenuRolNegocio _menuRolNegocio;

        public MenuAplicacion(IMenuNegocio _menuNegocio, IMenuRolNegocio _menuRolNegocio)
        {
            this._menuNegocio = _menuNegocio;
            this._menuRolNegocio = _menuRolNegocio;

        }
        public async Task<List<SubMenuDto>> BuscarSubMenusPorMenu(Guid MenuId, Guid RolId)
        {
            var resultado = new List<SubMenuDto>();
            try
            {
                var subMenus = await _menuNegocio.ObtenerMenusHijoPorMenu(MenuId);
                subMenus.ForEach(async m =>
                {
                    var rolTieneAccesoMenu = await _menuRolNegocio.RolTieneAccesoMenu(MenuId, RolId);
                    var it = Mapper.Map<Menu, SubMenuDto>(m);
                    it.SubMenus = await this.BuscarSubMenusPorMenu(it.MenuId, RolId);
                    resultado.Add(it);
                });
                return resultado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<MenuDto>> ObtenerAccesosPorRol(Guid RolId)
        {
            try
            {
                var acceso = await _menuNegocio.ObtenerAccesosPorRol(RolId);
                var accesoDto = Mapper.Map<List<Menu>, List<MenuDto>>(acceso);
                return accesoDto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<MenuDto>> ObtenerMenus()
        {
            try
            {
                var menus = await _menuNegocio.ObtenerMenus();
                var menusoDto = Mapper.Map<List<Menu>, List<MenuDto>>(menus);
                return menusoDto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<MenuDto>> ObtenerMenusPorRol(Guid RolId)
        {
            try
            {
                var menusDb = await _menuNegocio.ObtenerMenusPadrePorRol(RolId);
                var menusDto = Mapper.Map<List<Menu>, List<MenuDto>>(menusDb);
                menusDto.ForEach(async m =>
                {
                    m.SubMenus = await this.BuscarSubMenusPorMenu(m.MenuId, RolId);
                });

                return menusDto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
