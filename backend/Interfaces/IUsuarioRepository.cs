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
        Task<PagedList<Usuario>> GetAllUsuarios(int currentPage);
        Task<Usuario> GetUsuarioById(long id);
        Task<PagedList<UsuarioDTO>> GetUsuariosBySetor(long setorId, int currentPage);
        Task NewUsuario(Usuario usuario);
        Task SalvarAlteracao(Usuario usuario);
    }
}