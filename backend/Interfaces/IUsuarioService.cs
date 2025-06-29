using backend.DTO;
using backend.Helpers;

namespace backend.Interfaces
{
    public interface IUsuarioService
    {
        Task<PagedList<UsuarioDTO>> GetAllUsuarios(int currentPage);
        Task<UsuarioDTO> GetUsuarioById(int id);
        Task<PagedList<UsuarioDTO>> GetUsuarioBySetor(long id, int currentPage);
        Task<OperationResult> NewUsuario(UsuarioPostDTO post);
    }
}