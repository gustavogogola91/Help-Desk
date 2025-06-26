using backend.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class EquipamentoRepository : IEquipamentoRepository
    {
        private readonly AppDbContext _database;

        public EquipamentoRepository(AppDbContext database)
        {
            _database = database;
        }
        public async Task<bool> ExisteAsync(long id)
        {
            return await _database.tb_equipamento.AnyAsync(e => e.Id == id);
        }
    }
}