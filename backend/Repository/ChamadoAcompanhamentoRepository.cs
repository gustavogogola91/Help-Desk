using backend.Interfaces;
using Data;

namespace backend.Repository
{
    public class ChamadoAcompanhamentoRepository(AppDbContext database) : IChamadoAcompanhamentoRepository
    {
        private readonly AppDbContext _database = database;
    }
}