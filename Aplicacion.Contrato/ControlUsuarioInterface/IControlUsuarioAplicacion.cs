using Aplicacion.EntidadesDto;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Contrato.ControlUsuarioInterface
{
    public interface IControlUsuarioAplicacion
    {
        Task<bool> ActualizarUsuario(UsuarioDto usuariodto);
        Task<string> obtenerRolUsuario(UsuarioApp usuario);
        Task<bool> ResetPass(UsuarioApp user, string pass);
        Task<bool> GuardarUsuario(UsuarioDto usuarioDto);
        Task<bool> EliminarUsuario(string Id);
        List<UsuarioDto> ObtenerUsuarios();
        List<RolDto> ObtenerRoles();
        Task<RolDto> BuscarRolPorIdRol(string IdRol);
        Task<bool> EliminarRol(string Id);
        Task<bool> ActualizarRol(RolDto rolDto);
        Task<bool> GuardarRol(RolDto roldto);
    }
}
