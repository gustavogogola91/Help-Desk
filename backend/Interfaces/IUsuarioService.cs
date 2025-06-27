using backend.DTO;
using backend.Helpers;

namespace backend.Interfaces
{
    public interface IUsuarioService
    {
        Task<PagedList<UsuarioDTO>> GetAllUsuarios(int currentPage);
        Task<UsuarioDTO> GetUsuarioById(int id);
    }
}