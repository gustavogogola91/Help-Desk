namespace backend.Interfaces
{
    public interface ISetorRepository
    {
        Task<bool> ExisteAsync(long id);
    }
}