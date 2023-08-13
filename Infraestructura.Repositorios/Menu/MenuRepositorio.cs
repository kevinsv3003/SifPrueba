
using Dominio.Contratos.Repositorios;
using Dominio.Entidades;
using InfraestructuraRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Repositorios
{
   public class MenuRepositorio : BaseRepositorio<Menu>, IMenuRepositorio
    {
        public MenuRepositorio(DbContext context) : base(context)
        {

        }
    }
}
