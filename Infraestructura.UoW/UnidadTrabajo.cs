using Dominio.Contratos.Repositorios;
using Dominio.Contratos.UnidadTrabajo;
using Infraestructura.AccesoDatos;
using Infraestructura.Repositorios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.UoW
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly SIFContext sifContext;
        private readonly IConfiguration _configuration;
        public UnidadTrabajo(SIFContext sifContext, IConfiguration _configuration)
        {
            this.sifContext = sifContext;
            this._configuration = _configuration;
        }

        IMenuRepositorio IUnidadTrabajo.MenuRepositorio => new MenuRepositorio(sifContext);        
        IMenuRolRepositorio IUnidadTrabajo.MenuRolRepositorio => new MenuRolRepositorio(sifContext);        
        //IDocumento IUnidadTrabajo.DocumentoRepositorio => new DocumentoRepositorio(documentoContext, _configuration);


        public void Commit()
        {
            sifContext.SaveChanges();
            if (sifContext.Database.CurrentTransaction != null)
            {
                sifContext.Database.CurrentTransaction.Commit();
            }
        }

        public void Disposable()
        {
            GC.KeepAlive(this);
            sifContext.Dispose();
        }

        public void RollBack()
        {
            sifContext.Database.CurrentTransaction.Rollback();
        }
    }
}
