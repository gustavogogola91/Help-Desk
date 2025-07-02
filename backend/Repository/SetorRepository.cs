using backend.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class SetorRepository(AppDbContext database) : ISetorRepository
    {
        private readonly AppDbContext _database = database;

        public async Task<bool> ExisteAsync(long id)
        {
            return await _database.tb_setor.AnyAsync(s => s.Id == id);
        }
    }
}