using backend.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class ChamadoRepository : IChamadoRepository
    {
        private readonly AppDbContext _database;

        public ChamadoRepository(AppDbContext database)
        {
            _database = database;
        }

        public async Task<bool> ExisteAsync(long id)
        {
                return await _database.tb_chamado.AnyAsync(c => c.Id == id);          
        }
    }
}