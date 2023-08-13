using Aplicacion.Contrato.ControlUsuarioInterface;
using Aplicacion.EntidadesDto;
using AutoMapper;
using Dominio.Contratos.Servicio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios.ControlUsuario
{
    public class ControlUsuarioAplicacion : IControlUsuarioAplicacion
    {
        private IUsuarioNegocio _usuarioNegocio;
        public ControlUsuarioAplicacion(IUsuarioNegocio _usuarioNegocio)
        {
            this._usuarioNegocio = _usuarioNegocio;
        }
        public async Task<bool> ActualizarRol(RolDto rolDto)
        {
            bool respuesta = false;
            try
            {
                var Rol = new RolApp() { Name = rolDto.Nombre, Descripcion = rolDto.Descripcion };
                var Actualizado = await _usuarioNegocio.ActualizarRol(Rol);
                respuesta = Actualizado;
            }
            catch (Exception ex)
            {
                throw(ex);
            }
            return respuesta;
        }

        public async Task<bool> ActualizarUsuario(UsuarioDto usuariodto)
        {
            bool respuesta = false;
            try
            {
                var usuario = new UsuarioApp();

                usuario.Nombres = usuariodto.Nombres;
                usuario.Apellidos = usuariodto.Apellidos;
                usuario.UserName = usuariodto.UserName;
                usuario.Sexo = usuariodto.Sexo;
                usuario.PhoneNumber = usuariodto.PhoneNumber;
                usuario.Email = usuariodto.Email;

                var rolUsuario = await _usuarioNegocio.obtenerRolUsuario(usuario);               
                var Actualizado = await _usuarioNegocio.ActualizarUsuario(usuario, rolUsuario);
                respuesta = Actualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return respuesta;
        }

        public async Task<RolDto> BuscarRolPorIdRol(string IdRol)
        {
            var resultado = new RolDto();
            try
            {
                var roles = await _usuarioNegocio.BuscarRolPorIdRol(IdRol);

                resultado = new RolDto()
                {
                    Nombre = roles.Name,
                    Descripcion = roles.Descripcion,
                    IdRol = roles.Id.ToString()
                };

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return resultado;
        }

        public async Task<bool> EliminarRol(string Id)
        {
            bool respuesta = false;
            try
            {
                var EliminoRol = await _usuarioNegocio.EliminarRol(Id);
                respuesta = EliminoRol;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return respuesta;
        }

        public async Task<bool> EliminarUsuario(string Id)
        {
            bool respuesta = false;
            try
            {
                var EliminoUsuario = await _usuarioNegocio.EliminarUsuario(Id);
                respuesta = EliminoUsuario;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return respuesta;
        }

        public async Task<bool> GuardarRol(RolDto roldto)
        {
            bool respuesta = false;
            try
            {
                var rol = new RolApp() { Name = roldto.Nombre, Descripcion = roldto.Descripcion };
                var guardoRol = await _usuarioNegocio.GuardarRol(rol);
                respuesta = guardoRol;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return respuesta;
        }

        public async Task<bool> GuardarUsuario(UsuarioDto usuarioDto)
        {
            bool respuesta = false;
            try
            {
                var usuario = Mapper.Map<UsuarioDto, UsuarioApp>(usuarioDto);
                respuesta = await _usuarioNegocio.GuardarUsuario(usuario, usuarioDto.contra, usuarioDto.Rol);               
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return respuesta;
        }

        public List<RolDto> ObtenerRoles()
        {
            var resultado = new List<RolDto>();
            try
            {
                var roles = _usuarioNegocio.ObtenerRoles();
                foreach (var item in roles)
                {
                    var rol = new RolDto()
                    {
                        Nombre = item.Name,
                        Descripcion = item.Descripcion,
                        IdRol = item.Id.ToString()
                    };
                    resultado.Add(rol);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return resultado;
        }

        public async Task<string> obtenerRolUsuario(UsuarioApp usuario)
        {
            var rolList = await _usuarioNegocio.obtenerRolUsuario(usuario);
            return rolList;
        }

        public List<UsuarioDto> ObtenerUsuarios()
        {
            var resultado = new List<UsuarioDto>();
            try
            {
                var users = _usuarioNegocio.ObtenerUsuarios();
                foreach (var item in users)
                {
                    var user = Mapper.Map<UsuarioApp, UsuarioDto>(item);
                    user.Rol = obtenerRolUsuario(item).Result;
                    resultado.Add(user);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return resultado;
        }

        public async Task<bool> ResetPass(UsuarioApp user, string pass)
        {
            bool respuesta = false;
            try
            {
                respuesta = await _usuarioNegocio.ResetPass(user, pass);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return respuesta;
            
        }
    }
}
