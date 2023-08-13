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
    public class MenuNegocio : IMenuNegocio
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public MenuNegocio(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }
                
        public async Task<List<Menu>> ObtenerAccesosPorRol(Guid RolId)
        {
            var resultado = new List<Menu>();
            try
            {
                var menusRol = unidadTrabajo.MenuRolRepositorio.Buscar(m => m.RolId.Equals(RolId) && !m.EstaAnulado).ToList();
                menusRol.ForEach(m =>
                {
                    var menu = unidadTrabajo.MenuRepositorio.BuscarPorId(m.MenuId);
                    resultado.Add(menu);
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al obtener cuenta.");

            }
            return resultado;
        }

        public async Task<List<Menu>> ObtenerMenus()
        {
            var resultado = new List<Menu>();
            try
            {
                var menudb = unidadTrabajo.MenuRepositorio.ListarTodos();
                resultado = menudb.ToList();               
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al obtener menús.");

            }
            return resultado;
        }

        public async Task<List<Menu>> ObtenerMenusHijoPorMenu(Guid MenuId)
        {
            var resultado = new List<Menu>();
            try
            {
                var menudb = unidadTrabajo.MenuRepositorio.Buscar(m => m.PadreId.Equals(MenuId)).ToList();
                resultado = menudb;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al obtener menús.");

            }
            return resultado;
        }

        public async Task<List<Menu>> ObtenerMenusPadrePorRol(Guid RolId)
        {
            var resultado = new List<Menu>();
            try
            {
                var menusRol = unidadTrabajo.MenuRolRepositorio.Buscar(m => m.RolId.Equals(RolId) && m.Menu.esPrincipal && m.Menu.PadreId.Equals(Guid.Empty) && !m.EstaAnulado);
                foreach (var item in menusRol)
                {
                    var menu = unidadTrabajo.MenuRepositorio.BuscarPorId(item.MenuId);                    
                    resultado.Add(menu);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al obtener cuenta.");

            }
            return resultado;
        }
    }
}
