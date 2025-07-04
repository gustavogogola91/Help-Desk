using backend.DTO;
using backend.Helpers;

namespace backend.Interfaces
{
    public interface IUsuarioService
    {
        Task ModifyStatus(long id);
        Task<PagedList<UsuarioDTO>> GetAllUsuariosPaged(int currentPage);
        Task<UsuarioDTO> GetUsuarioById(long id);
        Task<PagedList<UsuarioDTO>> GetUsuarioBySetor(long id, int currentPage);
        Task NewUsuario(UsuarioPostDTO post);
        Task UserChangePassword(long id, ChangePasswordDTO dto);
        Task AdminResetUserPassword(long id);
        Task ModifyUsuario(UsuarioPutDTO put, long id);
        Task<IEnumerable<UsuarioDTO>> GetAllUsuarios();
    }
}