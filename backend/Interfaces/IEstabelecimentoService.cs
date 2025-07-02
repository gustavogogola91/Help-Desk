
using backend.DTO;
using backend.Helpers;

namespace backend.Interfaces
{
    public interface IEstabelecimentoService
    {
        Task ModifyStatus(long id);
        Task<PagedList<EstabelecimentoDTO>> GetAllEstabelecimentos(int currentPage);
        Task<EstabelecimentoDTO> GetEstabelecimentoById(long id);
        Task NewEstabelecimento(EstabelecimentoPostDTO post);
    }
}