using backend.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class EstabelecimentoRepository : IEstabelecimentoRepository
    {
        private readonly AppDbContext _database;

        public EstabelecimentoRepository(AppDbContext database)
        {
            _database = database;
        }

        public async Task<bool> ExisteAsync(long id)
        {
            return await _database.tb_estabelecimento.AnyAsync(e => e.Id == id);
        }
    }
}