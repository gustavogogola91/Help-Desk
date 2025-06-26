namespace backend.Interfaces
{
    public interface IEstabelecimentoRepository
    {
        Task<bool> ExisteAsync(long id);
    }
}