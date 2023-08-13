
using Dominio.Contratos.Repositorios;
using Dominio.Entidades;
using InfraestructuraRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infraestructura.Repositorios
{
   public class MenuRolRepositorio : BaseRepositorio<RolesMenu>, IMenuRolRepositorio
    {
        public MenuRolRepositorio(DbContext context) : base(context)
        {

        }
       
    }
}
