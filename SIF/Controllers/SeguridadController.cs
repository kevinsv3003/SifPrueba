using Aplicacion.Contrato;
using Aplicacion.Contrato.ControlUsuarioInterface;
using Aplicacion.EntidadesDto;
using AutoMapper;
using Dominio.Contratos.Servicio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using static Infraestructura.Transversal.Utilitario;

namespace SIF.Controllers
{
    public class SeguridadController : Controller
    {
        private SignInManager<UsuarioApp> _signManager;
        private UserManager<UsuarioApp> _userManager;
        private RoleManager<RolApp> _roleManager;
        private IUsuarioNegocio _usuarioNegocio;
        private IControlUsuarioAplicacion _usuarioAplicacion;
        private IMenuAplicacion _menuAplicacion;
        private IMenuRolAplicacion _menuRolAplicacion;
        private IUtilitario _utilitario;

        public SeguridadController(SignInManager<UsuarioApp> _signManager, UserManager<UsuarioApp> _userManager, IMenuRolAplicacion _menuRolAplicacion, IControlUsuarioAplicacion _usuarioAplicacion,
            RoleManager<RolApp> _roleManager, IUsuarioNegocio _usuarioNegocio, IMenuAplicacion _menuAplicacion, IUtilitario _utilitario)
        {
            this._signManager = _signManager;
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._usuarioNegocio = _usuarioNegocio;
            this._usuarioAplicacion = _usuarioAplicacion;
            this._menuAplicacion = _menuAplicacion;
            this._menuRolAplicacion = _menuRolAplicacion;
            this._utilitario = _utilitario;
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult AdministrarCuentas()
        {
            return View();
        }

        public IActionResult _TablaUsuarios()
        {
            try
            {
                var usuarios = _usuarioAplicacion.ObtenerUsuarios();// _usuarioNegocio.ObtenerUsuarios();
                return PartialView(usuarios);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }
        }
        public IActionResult _TablaRoles()
        {
            try
            {
                var usuarios = _usuarioAplicacion.ObtenerRoles(); //_usuarioNegocio.ObtenerRoles();
                return PartialView(usuarios);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }
        }

        [Authorize]
        public async Task<IActionResult> _FormularioUsuario(int Origen, string IdUser)
        {
            try
            {
                ViewBag.Roles = await obtenerSelcetRoles(Origen, IdUser);
                ViewBag.Origen = Origen;
                var usuario = (Origen == (int)ORIGEN_FORMULARIO.PROPIO) ? await _userManager.GetUserAsync(User) :
                              (Origen == (int)ORIGEN_FORMULARIO.NUEVO) ? null : await _userManager.FindByIdAsync(IdUser);
                var UsuarioDto = (usuario != null) ? Mapper.Map<UsuarioApp, UsuarioDto>(usuario) : new UsuarioDto();
                ViewBag.Rol = (usuario != null) ? await _usuarioNegocio.obtenerRolUsuario(usuario) : "";

                return PartialView(UsuarioDto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
        }

        [Authorize]
        public async Task<IActionResult> _FormularioRol(int Origen, string IdRol)
        {
            try
            {
                ViewBag.Origen = Origen;
                var rolDto = (Origen != (int)ORIGEN_FORMULARIO.NUEVO)  ? await _usuarioAplicacion.BuscarRolPorIdRol(IdRol) : new RolDto();

                return PartialView(rolDto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
        }


        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> _GestionAccesoRol(string IdRol)
        {
            try
            {
                var listaMenu = await _menuAplicacion.ObtenerMenus();
                var menusRol = await _menuAplicacion.ObtenerAccesosPorRol(Guid.Parse(IdRol));

                listaMenu.ForEach(x =>
                {
                    x.Activo = menusRol.Any(m => m.MenuId.Equals(x.MenuId));
                });
                ViewBag.IdRol = IdRol;
                return PartialView(listaMenu);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
        }
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GuardarUsuario(UsuarioDto usuario)
        {
            Thread.Sleep(1500);
            var mensaje = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    if (usuario.OrigenForm != (int)ORIGEN_FORMULARIO.NUEVO)
                    {
                        var actualizado = await _usuarioAplicacion.ActualizarUsuario(usuario);// _usuarioNegocio.ActualizarUsuario(usuario);
                        if (actualizado)
                        {
                            Response.StatusCode = (int)HttpStatusCode.OK;
                            mensaje = "Se ha actualizado su información correctamente!!";
                        }
                        else
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            mensaje = "Se produjo un error al actualizar su información!!";
                        }
                    }
                    else
                    {
                        if (usuario.contra != usuario.contraConfirm)
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            mensaje = "Las contraseñas no coinciden, intente nuevamente!!";
                        }
                        else
                        {
                            usuario.contra = _utilitario.GenerarCodigo();
                            var guardado = await _usuarioAplicacion.GuardarUsuario(usuario);// _usuarioNegocio.GuardarUsuario(usuario);
                            if (guardado)
                            {
                                Response.StatusCode = (int)HttpStatusCode.OK;
                                mensaje = "Se ha guardado el usuario correctamente!!";
                            }
                            else
                            {
                                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                mensaje = "Se produjo un error al guardar el usuario!!";
                            }
                        }
                    }
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    mensaje = "Favor verifique todos los campos!!";
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
            return Json(mensaje);
        }
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GuardarRol(RolDto rol)
        {
            Thread.Sleep(1500);
            var mensaje = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    if (rol.IdRol != null)
                    {
                        var actualizado = await _usuarioAplicacion.ActualizarRol(rol);// _usuarioNegocio.ActualizarRol(rol);
                        if (actualizado)
                        {
                            Response.StatusCode = (int)HttpStatusCode.OK;
                            mensaje = "Se ha actualizado su información correctamente!!";
                        }
                        else
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            mensaje = "Se produjo un error al actualizar su información!!";
                        }
                    }
                    else
                    {

                        var guardado = await _usuarioAplicacion.GuardarRol(rol);// _usuarioNegocio.GuardarRol(rol);
                        if (guardado)
                        {
                            Response.StatusCode = (int)HttpStatusCode.OK;
                            mensaje = "Se ha guardado el rol correctamente!!";
                        }
                        else
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            mensaje = "Se produjo un error al guardar el rol!!";
                        }

                    }
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    mensaje = "Favor verifique todos los campos!!";
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
            return Json(mensaje);
        }
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EliminarUsuario([FromBody] string Id)
        {
            Thread.Sleep(1500);
            var mensaje = string.Empty;
            try
            {
                var retorno = await _usuarioAplicacion.EliminarUsuario(Id);// _usuarioNegocio.EliminarUsuario(Id);
                if (retorno)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    mensaje = "Se eliminó el usuario correctamente!!";
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    mensaje = "Se generó un error al eliminar el usuario!!";
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
            return Json(mensaje);
        }
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> EliminarRol([FromBody] string Id)
        {
            Thread.Sleep(1500);
            var mensaje = string.Empty;
            try
            {
                var retorno = await _usuarioAplicacion.EliminarRol(Id);// _usuarioNegocio.EliminarRol(Id);
                if (retorno)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    mensaje = "Se eliminó el rol correctamente!!";
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    mensaje = "Se generó un error al eliminar el rol!!";
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
            return Json(mensaje);
        }

        [AllowAnonymous]
        public async Task<IActionResult> RestaurarContra([FromBody] LoginDto model)
        {
            return await RecordarContrasena(model);
        }
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> _changeAcceso(string MenuId, bool Ischecked, string IdRol)
        {
            var mensaje = string.Empty;
            try
            {
                var acceso = (Ischecked) ? new RolesMenuDto() { MenuId = Guid.Parse(MenuId), RolId = Guid.Parse(IdRol) } : _menuRolAplicacion.BuscarAcceso(Guid.Parse(MenuId), Guid.Parse(IdRol)).Result.FirstOrDefault();
                var retorno = (Ischecked) ? await _menuRolAplicacion.InsertarMenuRol(acceso) : await _menuRolAplicacion.EliminarMenuRol(acceso.RolesMenuId);
                if (retorno)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
            return Json(mensaje);
        }

        [AllowAnonymous]
        public async Task<IActionResult> RecordarContrasena(LoginDto model)
        {
            Thread.Sleep(1500);
            var mensaje = string.Empty;
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var esCorreoUsuarioValido = user != null && (user.UserName == model.userName);

                if (esCorreoUsuarioValido)
                {
                    var contraseña = _utilitario.GenerarCodigo();// General.GenerarCodigo();

                    var ResetContraseña = await _usuarioAplicacion.ResetPass(user, contraseña);// _usuarioNegocio.ResetPass(user, contraseña);
                    if (ResetContraseña)
                    {
                        var valoresCuerpo = new ValoresCorreoContra()
                        {
                            NombreCompleto = user.Nombres + " " + user.Apellidos,
                            NombreUsuario = user.UserName,
                            Contraseña = contraseña
                        };
                        string cuerpo = _utilitario.ObtenerHtml("CorreoContra.cshtml", valoresCuerpo);
                        var valoresCorreo = new MailDto()
                        {
                            NombreCompleto = valoresCuerpo.NombreCompleto,
                            Correo = model.Email,
                            Asunto = "Restauración Contraseña”",
                            Mensaje = cuerpo
                        };
                        await _utilitario.EnviarEmailAsync(valoresCorreo);
                        Response.StatusCode = (int)HttpStatusCode.OK;
                        mensaje = "Se ha restaurado la contraseña con éxito, se recomiendo cambiarla al iniciar sesión.";
                    }
                    else
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        mensaje = "Se produjo un error al restaurar su contraseña!!";
                    }

                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    mensaje = "Usuario o Correo no válidos!!";
                }

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                mensaje = "Se produjo un error al restaurar su contraseña!!";
            }
            return Json(mensaje);
        }
        private async Task<List<SelectListItem>> obtenerSelcetRoles(int Origen, string IdUser)
        {
            var usuario = (Origen == (int)ORIGEN_FORMULARIO.PROPIO) && (User.Identity.IsAuthenticated) ? await _userManager.GetUserAsync(User) :
                          (Origen == (int)ORIGEN_FORMULARIO.NUEVO) ? null : await _userManager.FindByIdAsync(IdUser);

            var rolUser = (usuario != null) ? await _usuarioAplicacion.obtenerRolUsuario(usuario) : null;

            var rolList = _roleManager.Roles.ToList();
            List<SelectListItem> lst = new List<SelectListItem>();
            SelectListItem select = new SelectListItem() { Text = "..:: Seleccione una Opción ::..", Value = "0" };
            lst.Add(select);

            foreach (var item in rolList)
            {
                lst.Add(new SelectListItem() { Text = item.Name, Value = item.Name.ToString(), Selected = (rolUser != null) ? (item.Name.Equals(rolUser)) : false });
            }

            return lst;
        }

        #region Inicio de Sesion

        [AllowAnonymous]
        public IActionResult Index(string returnUrl = "")
        {
            var model = new LoginDto { ReturnUrl = returnUrl };
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> IniciarSesion(LoginDto model)
        {
            Thread.Sleep(1500);
            var mensaje = string.Empty;
            if (ModelState.IsValid)
            {
                await _signManager.SignOutAsync();
                var result = await _signManager.PasswordSignInAsync(model.userName, model.Password, model.RememberMe, lockoutOnFailure: true);

                Response.StatusCode = (result.Succeeded) ? (int)HttpStatusCode.OK : (result.IsLockedOut) ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.BadRequest;
                mensaje = (result.Succeeded) ? "Listo" : (result.IsLockedOut) ? "UBT" : "Usuario o Contraseña no validos";
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                mensaje = "Favor introducir correctamente los datos!";
            }
            return Json(mensaje);
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }

        #endregion
    }
}
