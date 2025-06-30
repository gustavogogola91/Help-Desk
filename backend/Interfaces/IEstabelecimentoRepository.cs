using backend.Helpers;
using backend.Model;

namespace backend.Interfaces
{
    public interface IEstabelecimentoRepository
    {
        Task<bool> ExisteAsync(long id);
        Task<PagedList<Estabelecimento>> GetAllEstabelecimentos(int currentPage);
        Task<Estabelecimento> GetEstabelecimentoById(long id);
        Task NewEstabelecimento(Estabelecimento estabelecimento);
    }
}