using backend.Helpers;
using backend.Interfaces;
using backend.Model;
using Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class SetorRepository(AppDbContext database, IPaginationHelper pagination) : ISetorRepository
    {
        private readonly AppDbContext _database = database;
        private readonly IPaginationHelper _pagination = pagination;

        public async Task<bool> ExisteAsync(long id)
        {
            return await _database.tb_setor.AnyAsync(s => s.Id == id);
        }

        public async Task<List<Setor>> GetAllSetores()
        {
            var setores = await _database.tb_setor.Where(s => s.Ativo).ToListAsync();
            return setores;
        }

        public async Task<PagedList<Setor>> GetAllSetoresPaged(int currentPage)
        {
            var query = _database.tb_setor.AsQueryable();
            var pagedList = await _pagination.CreateAsync(query, currentPage, 10);

            return pagedList;
        }

        public async Task<Setor> GetSetorById(long id)
        {
            var usuario = await _database.tb_setor.FirstOrDefaultAsync(s => s.Id == id);
            return usuario!;
        }

        public async Task SalvarAlteracao(Setor setor)
        {
            _database.tb_setor.Update(setor);
            await _database.SaveChangesAsync();
        }
    }
}