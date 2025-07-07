using backend.Helpers;
using backend.Model;

namespace backend.Interfaces
{
    public interface IEquipamentoRepository
    {
        Task<bool> ExisteAsync(long id);
        Task<IEnumerable<Equipamento>> GetAllEquipamentos();
        Task<PagedList<Equipamento>> GetAllEquipamentosPaged(int currentPage);
        Task<Equipamento> GetEquipamentoById(long id);
        Task NewEquipamento(Equipamento equipamento);
        Task SalvarAlteracao(Equipamento equipamento);
    }
}