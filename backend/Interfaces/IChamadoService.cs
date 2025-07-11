using backend.DTO;

namespace backend.Interfaces
{
    public interface IChamadoService
    {
        Task<string> NewChamado(ChamadoPostDTO post);
    }
}