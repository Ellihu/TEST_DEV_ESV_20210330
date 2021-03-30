using Datos.Repository.Actions;
using Entidades.EntityModels;

namespace Datos.Repository
{
    public interface IUsuariosRepository :
        IReadRepository<UsuariosEModel, int>
    {
        UsuariosEModel GetByEmail(string email);
    }
}
