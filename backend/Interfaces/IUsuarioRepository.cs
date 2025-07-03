using backend.DTO;
using backend.Helpers;
using backend.Model;

namespace backend.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> ExisteAsync(long id);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UsernameExistsAsync(string username);
        Task<PagedList<Usuario>> GetAllUsuariosPaged(int currentPage);
        Task<List<Usuario>> GetAllUsuarios();
        Task<Usuario> GetUsuarioById(long id);
        Task<PagedList<UsuarioDTO>> GetUsuariosBySetor(long setorId, int currentPage);
        Task<List<EmailDTO>> GetEmailsBySetor(long setorId);
        Task NewUsuario(Usuario usuario);
        Task SalvarAlteracao(Usuario usuario);
    }
}