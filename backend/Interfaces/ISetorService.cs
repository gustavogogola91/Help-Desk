
using backend.DTO;
using backend.Helpers;

namespace backend.Interfaces
{
    public interface ISetorService
    {
        Task<IEnumerable<SetorDTO>> GetAllSetores();
        Task<PagedList<SetorDTO>> GetAllSetoresPaged(int currentPage);
        Task<SetorDTO> GetSetorById(long id);
        Task ModifyStatus(long id);
        Task NewSetor(SetorPostDTO post);
    }
}