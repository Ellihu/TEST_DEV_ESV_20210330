using Datos.Repository.Actions;
using Entidades.EntityModels;

namespace Datos.Repository
{
    public interface IPersonasFisicasRepository :
        ICreateRepository<PersonasFisicasEModel>,
        IUpdateRepository<PersonasFisicasEModel>,
        IReadRepository<PersonasFisicasEModel, int>,
        IRemoveRepository<PersonasFisicasEModel>
    {
        //Puede agregar otras acciones propias del repositorio
    }
}
