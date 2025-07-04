using backend.Helpers;
using backend.Model;

namespace backend.Interfaces
{
    public interface ISetorRepository
    {
        Task<bool> ExisteAsync(long id);
        Task<List<Setor>> GetAllSetores();
        Task<PagedList<Setor>> GetAllSetoresPaged(int currentPage);
        Task<Setor> GetSetorById(long id);
        Task SalvarAlteracao(Setor setor);
    }
}