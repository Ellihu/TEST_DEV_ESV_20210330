using Entidades.EntityModels;
using Entidades.ViewModels;
using System.Collections.Generic;

namespace Servicios
{
    public interface IApiService
    {
        public void Autenticar();
        public bool IsAutenticate();
        public dynamic Listar();
    }
}
