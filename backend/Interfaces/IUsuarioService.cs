using backend.DTO;
using backend.Helpers;

namespace backend.Interfaces
{
    public interface IUsuarioService
    {
        Task ModifyStatus(long id);
        Task<PagedList<UsuarioDTO>> GetAllUsuarios(int currentPage);
        Task<UsuarioDTO> GetUsuarioById(long id);
        Task<PagedList<UsuarioDTO>> GetUsuarioBySetor(long id, int currentPage);
        Task NewUsuario(UsuarioPostDTO post);
        Task UserChangePassword(long id, ChangePasswordDTO dto);
    }
}