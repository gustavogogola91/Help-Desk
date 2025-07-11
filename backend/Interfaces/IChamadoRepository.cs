using backend.Model;

namespace backend.Interfaces
{
    public interface IChamadoRepository
    {
        Task<bool> ExisteAsync(long id);
        Task NewChamado(Chamado chamado);
    }
}