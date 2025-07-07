
using backend.DTO;
using backend.Helpers;

namespace backend.Interfaces
{
    public interface IEquipamentoService
    {
        Task<List<EquipamentoDTO>> GetAllEquipamentos();
        Task<PagedList<EquipamentoDTO>> GetAllEquipamentosPaged(int currentPage);
        Task<EquipamentoDTO> GetEquipamentoById(long id);
        Task ModifyStatus(long id);
        Task NewEquipamento(EquipamentoPostDTO post);
    }
}