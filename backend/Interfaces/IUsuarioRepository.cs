using backend.Helpers;
using backend.Model;

namespace backend.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> ExisteAsync(long id);

        Task<PagedList<Usuario>> GetAllUsuarios(int currentPage);

        Task<Usuario> GetUsuarioById(int id);
    }
}