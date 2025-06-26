namespace backend.Interfaces
{
    public interface IChamadoAcompanhamentoRepository
    {
        Task<bool> ExisteAsync(long id);
    }
}