using Entidades.EntityModels;
using Entidades.ViewModels;

namespace Servicios
{
    public interface IUserService
    {
        public AutenticarRespuestaVModel Autenticar(AutenticarVModel model);
        public UsuariosEModel UserLogued();
    }
}
