using Aplicacion.EntidadesDto;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Contratos.Servicio
{
    public interface IUsuarioNegocio
    {
        Task<bool> ActualizarUsuario(UsuarioApp usuariodto,string rol);
        Task<string> obtenerRolUsuario(UsuarioApp usuario);
        Task<bool> ResetPass(UsuarioApp user, string pass);
        Task<bool> GuardarUsuario(UsuarioApp usuarioDto,string pass, string rol);
        Task<bool> EliminarUsuario(string Id);
        List<UsuarioApp> ObtenerUsuarios();
        List<RolApp> ObtenerRoles();
        Task<RolApp> BuscarRolPorIdRol(string IdRol);
        Task<bool> EliminarRol(string Id);
        Task<bool> ActualizarRol(RolApp rolDto);
        Task<bool> GuardarRol(RolApp roldto);
   
    }
}
