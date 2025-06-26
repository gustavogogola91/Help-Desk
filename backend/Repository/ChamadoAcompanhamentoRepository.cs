using backend.Interfaces;
using Data;

namespace backend.Repository
{
    public class ChamadoAcompanhamentoRepository : IChamadoAcompanhamentoRepository
    {
        private readonly AppDbContext _database;

        public ChamadoAcompanhamentoRepository(AppDbContext database)
        {
            _database = database;
        }
        
    }
}