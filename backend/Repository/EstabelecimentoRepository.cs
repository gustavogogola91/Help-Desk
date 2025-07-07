using backend.Helpers;
using backend.Interfaces;
using backend.Model;
using Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class EstabelecimentoRepository(AppDbContext database, IPaginationHelper pagination) : IEstabelecimentoRepository
    {
        private readonly AppDbContext _database = database;
        private readonly IPaginationHelper _pagination = pagination;

        public async Task<bool> ExisteAsync(long id)
        {
            return await _database.tb_estabelecimento.AnyAsync(e => e.Id == id);
        }

        public async Task<PagedList<Estabelecimento>> GetAllEstabelecimentos(int currentPage)
        {
            var query = _database.tb_estabelecimento.AsQueryable();
            var pagedList = await _pagination.CreateAsync(query, currentPage, 10);

            return pagedList;
        }

        public async Task<Estabelecimento> GetEstabelecimentoById(long id)
        {
            var estabelecimento = await _database.tb_estabelecimento.FirstOrDefaultAsync(e => e.Id == id);
            return estabelecimento!;
        }

        public async Task NewEstabelecimento(Estabelecimento estabelecimento)
        {
            _database.tb_estabelecimento.Add(estabelecimento);
            await _database.SaveChangesAsync();
        }

        public async Task SalvarAlteracao(Estabelecimento estabelecimento)
        {
            _database.Update(estabelecimento);
            await _database.SaveChangesAsync();
        }
    }
}