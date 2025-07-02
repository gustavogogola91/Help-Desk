using backend.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class ChamadoRepository(AppDbContext database) : IChamadoRepository
    {
        private readonly AppDbContext _database = database;

        public async Task<bool> ExisteAsync(long id)
        {
                return await _database.tb_chamado.AnyAsync(c => c.Id == id);          
        }
    }
}