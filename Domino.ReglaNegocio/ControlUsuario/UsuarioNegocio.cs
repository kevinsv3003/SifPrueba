using Aplicacion.EntidadesDto;
using AutoMapper;
using Dominio.Contratos.Servicio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.ReglaNegocio
{
    public class UsuarioNegocio : IUsuarioNegocio
    {
        private SignInManager<UsuarioApp> _signManager;
        private UserManager<UsuarioApp> _userManager;
        private RoleManager<RolApp> _roleManager;

        public UsuarioNegocio(SignInManager<UsuarioApp> _signManager, UserManager<UsuarioApp> _userManager, RoleManager<RolApp> _roleManager)
        {
            this._signManager = _signManager;
            this._userManager = _userManager;
            this._roleManager = _roleManager;
        }

        public List<UsuarioApp> ObtenerUsuarios()
        {
            var resultado = new List<UsuarioApp>();
            try
            {
                var users = _userManager.Users.ToList();
                //foreach (var item in users)
                //{
                //    var user = Mapper.Map<UsuarioApp, UsuarioDto>(item);
                //    user.Rol = obtenerRolUsuario(item).Result;
                //    resultado.Add(user);
                //}
                resultado = users;
                    
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return resultado;
        }

        public List<RolApp> ObtenerRoles()
        {
            var resultado = new List<RolApp>();
            try
            {
                var roles = _roleManager.Roles.ToList();
                //foreach (var item in roles)
                //{
                //    var rol = new RolDto()
                //    {
                //        Nombre = item.Name,
                //        Descripcion = item.Descripcion,
                //        IdRol = item.Id.ToString()
                //    };
                //    resultado.Add(rol);
                //}
                resultado = roles;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return resultado;
        }

        public async Task<RolApp> BuscarRolPorIdRol(string IdRol)
        {
            var resultado = new RolApp();
            try
            {
                var roles = await _roleManager.FindByIdAsync(IdRol);

                //resultado = new RolDto()
                //{
                //    Nombre = roles.Name,
                //    Descripcion = roles.Descripcion,
                //    IdRol = roles.Id.ToString()
                //};
                resultado = roles;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return resultado;
        }

        public async Task<bool> ActualizarUsuario(UsuarioApp usuariodto, string rol)
        {
            bool respuesta = false;
            try
            {
                var usuario = usuariodto;// await _userManager.FindByIdAsync(usuariodto.Id);

                //usuario.Nombres = usuariodto.Nombres;
                //usuario.Apellidos = usuariodto.Apellidos;
                //usuario.UserName = usuariodto.UserName;
                //usuario.Sexo = usuariodto.Sexo;
                //usuario.PhoneNumber = usuariodto.PhoneNumber;
                //usuario.Email = usuariodto.Email;

                var rolUsuario = await obtenerRolUsuario(usuario);
                var borrarRol = (rolUsuario != string.Empty) ? await _userManager.RemoveFromRoleAsync(usuario, rolUsuario) : new IdentityResult();
                var AsignarRol = (borrarRol.Succeeded || rolUsuario == string.Empty) ? await _userManager.AddToRoleAsync(usuario, rol) : new IdentityResult();
                var Actualizado = await _userManager.UpdateAsync(usuario);

                respuesta = Actualizado.Succeeded && AsignarRol.Succeeded;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return respuesta;
        }

        public async Task<bool> ActualizarRol(RolApp rolDto)
        {
            bool respuesta = false;
            try
            {
                var Rol = rolDto;// await _roleManager.FindByIdAsync(rolDto.IdRol);
                //Rol.Name = rolDto.Nombre;
                //Rol.Descripcion = rolDto.Descripcion;
                var Actualizado = await _roleManager.UpdateAsync(Rol);
                respuesta = Actualizado.Succeeded;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return respuesta;
        }

        public async Task<bool> GuardarRol(RolApp roldto)
        {
            bool respuesta = false;
            try
            {
                var rol = roldto;// new RolApp() { Name = roldto.Nombre, Descripcion = roldto.Descripcion };
                var guardoRol = await _roleManager.CreateAsync(rol);
                respuesta = guardoRol.Succeeded;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return respuesta;
        }

        public async Task<bool> GuardarUsuario(UsuarioApp usuarioDto, string pass, string rol)
        {
            bool respuesta = false;
            try
            {
                var usuario = usuarioDto;//Mapper.Map<UsuarioDto, UsuarioApp>(usuarioDto);
                
                var guardoUsuario = await _userManager.CreateAsync(usuario,pass);
                if (guardoUsuario.Succeeded)
                {
                    var rolAsignado = await _userManager.AddToRoleAsync(usuario, rol);
                    respuesta = guardoUsuario.Succeeded && rolAsignado.Succeeded;
                }
                else if (guardoUsuario.Errors.FirstOrDefault().Code.Equals("PasswordTooShort"))
                {
                    var mensaje = ("Se produjo un error al restaurar su contraseña!! \nDebe de ser mayor de 5 caracteres.");
                    throw new ArgumentException(mensaje);
                }
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
                var usuario = await _userManager.FindByIdAsync(Id);
                var EliminoUsuario = await _userManager.DeleteAsync(usuario);

                respuesta = EliminoUsuario.Succeeded;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return respuesta;
        }

        public async Task<bool> EliminarRol(string Id)
        {
            bool respuesta = false;
            try
            {
                var rol = await _roleManager.FindByIdAsync(Id);
                var EliminoRol = await _roleManager.DeleteAsync(rol);

                respuesta = EliminoRol.Succeeded;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return respuesta;
        }

        public async Task<string> obtenerRolUsuario(UsuarioApp usuario)
        {
            var rolList = await _userManager.GetRolesAsync(usuario);
            var rol = rolList.Count > 0 ? new List<string>(rolList).ToArray()[0] : "";
            return rol;
        }

        public async Task<bool> ResetPass(UsuarioApp user, string pass)
        {
            //var removePass = await _userManager.RemovePasswordAsync(user);
            //var resetPass = await _userManager.AddPasswordAsync(user, contraseña);
            var tokenResetPass = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetPass = await _userManager.ResetPasswordAsync(user, tokenResetPass, pass);

            return resetPass.Succeeded;
        }

    }
}
