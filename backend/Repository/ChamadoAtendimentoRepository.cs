using backend.Interfaces;
using Data;

namespace backend.Repository
{
    public class ChamadoAtendimentoRepository : IChamadoAtendimentoRepository
    {
        private readonly AppDbContext _database;

        public ChamadoAtendimentoRepository(AppDbContext database)
        {
            _database = database;
        }
    }
}