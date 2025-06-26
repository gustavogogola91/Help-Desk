namespace backend.Interfaces
{
    public interface IChamadoRepository
    {
        Task<bool> ExisteAsync(long id);
    }
}