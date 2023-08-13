
using Aplicacion.Contrato;
using Aplicacion.Contrato.ControlUsuarioInterface;
using Aplicacion.Servicios.ControlUsuario;
using Aplicacion.Servicios.MenuServicios;
using Dominio.Contratos.Repositorios;
using Dominio.Contratos.Servicio;
using Dominio.Contratos.UnidadTrabajo;
using Dominio.ReglaNegocio;
using Domino.ReglaNegocio;
using Infraestructura.Repositorios;
using Infraestructura.Transversal;
using Infraestructura.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructura.IoC
{
    public class ServicioIoC
    {
        public static IServiceCollection Service(IServiceCollection services)
        {
            //REGISTRAMOS UNIDAD DE TRABAJO Y REPOSITORIOS           
            services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();
            services.AddScoped<IUtilitario, Utilitario>();
            services.AddScoped<IMenuRepositorio, MenuRepositorio>();
            services.AddScoped<IMenuRolRepositorio, MenuRolRepositorio>();


            //REGISTRAMOS LOS MODELOS DE NEGOCIO           
            services.AddScoped<IUsuarioNegocio, UsuarioNegocio>();
            services.AddScoped<IMenuNegocio, MenuNegocio>();
            services.AddScoped<IMenuRolNegocio, MenuRolNegocio>();

            //REGISTRAMOS LOS MODELOS DE APLICACION           
            services.AddScoped<IControlUsuarioAplicacion, ControlUsuarioAplicacion>();
            services.AddScoped<IMenuRolAplicacion, MenuRolAplicacion>();
            services.AddScoped<IMenuAplicacion, MenuAplicacion>();

            return services;
        }
    }
}
