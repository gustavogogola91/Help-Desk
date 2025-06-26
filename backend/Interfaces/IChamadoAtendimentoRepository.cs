namespace backend.Interfaces
{
    public interface IChamadoAtendimentoRepository
    {
        Task<bool> ExisteAsync(long id);
    }
}