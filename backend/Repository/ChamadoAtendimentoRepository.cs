using backend.Interfaces;
using Data;

namespace backend.Repository
{
    public class ChamadoAtendimentoRepository(AppDbContext database) : IChamadoAtendimentoRepository
    {
        private readonly AppDbContext _database = database;
    }
}