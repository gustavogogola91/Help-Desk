namespace backend.Interfaces
{
    public interface IEquipamentoRepository
    {
        Task<bool> ExisteAsync(long id);
    }
}