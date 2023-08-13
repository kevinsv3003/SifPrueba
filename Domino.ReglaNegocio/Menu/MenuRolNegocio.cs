using Aplicacion.EntidadesDto;
using AutoMapper;
using Dominio.Contratos.Servicio;
using Dominio.Contratos.UnidadTrabajo;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domino.ReglaNegocio
{
    public class MenuRolNegocio : IMenuRolNegocio
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public MenuRolNegocio(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }

        public async Task<List<RolesMenu>> BuscarAcceso(Guid MenuId, Guid RolId)
        {
            var resultado = new List<RolesMenu>();
            try
            {
                var accesoBD = unidadTrabajo.MenuRolRepositorio.Buscar(m => m.MenuId.Equals(MenuId) && m.RolId.Equals(RolId));
                resultado = new List<RolesMenu>(accesoBD);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al obtener Accesos.");

            }
            return resultado;
        }

        public async Task<bool> EliminarMenuRol(Guid RolesMenuId)
        {
            try
            {
                var retorno = await Task.Factory.StartNew(() =>
                {
                    unidadTrabajo.MenuRolRepositorio.Eliminar(RolesMenuId);
                    unidadTrabajo.Commit();
                    return true;
                });

                return retorno;

            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al eliminar el acceso.");

            }
        }

        public async Task<bool> InsertarMenuRol(RolesMenu rolMenu)
        {
            try
            {
                var retorno = await Task.Factory.StartNew(() =>
                {
                    unidadTrabajo.MenuRolRepositorio.Insertar(rolMenu);
                    unidadTrabajo.Commit();
                    return true;
                });

                return retorno;
            }
            catch (Exception)
            {
                unidadTrabajo.RollBack();
                throw new Exception("Ocurrió un problema al agregar un nuevo acceso.");

            }
        }

        public async Task<bool> RolTieneAccesoMenu(Guid MenuId, Guid RolId)
        {
            try
            {
                var retorno = unidadTrabajo.MenuRolRepositorio.Buscar(m => m.RolId.Equals(RolId) && m.MenuId.Equals(MenuId) && !m.EstaAnulado).Any();
                return retorno;

            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al Consultar Acceso.");
            }
        }
    }
}
