
using Dominio.Contratos.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Contratos.UnidadTrabajo { 
    public interface IUnidadTrabajo
    {
        #region Declaraciones de propiedades y metodos de la interfase
        
        IMenuRepositorio MenuRepositorio { get; }
        IMenuRolRepositorio MenuRolRepositorio { get; }
        void Commit();
        void RollBack();
        void Disposable();

        #endregion
    }
}
