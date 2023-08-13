using Dominio.Contratos;
using Dominio.Contratos.Servicio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SIF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SIF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<UsuarioApp> _userManager; 
        private IUsuarioNegocio usuarioNegocio;

        public HomeController(ILogger<HomeController> logger, UserManager<UsuarioApp> _userManager, IUsuarioNegocio usuarioNegocio)
        {
            _logger = logger;
            this._userManager = _userManager;
            this.usuarioNegocio = usuarioNegocio;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var usuario = User.Identity.IsAuthenticated ? await _userManager.GetUserAsync(User) : null;
            ViewBag.RolActual = (usuario != null) ? await usuarioNegocio.obtenerRolUsuario(usuario) : "";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
