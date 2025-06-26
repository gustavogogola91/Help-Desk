namespace backend.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> ExisteAsync(long id);
    }
}