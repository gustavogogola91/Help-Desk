using backend.Helpers;
using backend.Interfaces;
using backend.Model;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace backend.Repository
{
    public class EquipamentoRepository(AppDbContext database, IPaginationHelper pagination) : IEquipamentoRepository
    {
        private readonly AppDbContext _database = database;
        private readonly IPaginationHelper _pagination = pagination;

        public async Task<bool> ExisteAsync(long id)
        {
            return await _database.tb_equipamento.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Equipamento>> GetAllEquipamentos()
        {
            return await _database.tb_equipamento.Include(e => e.Setor).Where(e => e.Ativo).ToListAsync();
        }

        public async Task<PagedList<Equipamento>> GetAllEquipamentosPaged(int currentPage)
        {
            var query = _database.tb_equipamento.Include(e => e.Setor).AsQueryable();
            var equipamentos = await _pagination.CreateAsync(query, currentPage, 10);

            return equipamentos;
        }

        public async Task<Equipamento> GetEquipamentoById(long id)
        {
            var equipamento = await _database.tb_equipamento.Include(e => e.Setor).FirstOrDefaultAsync(e => e.Id == id);

            return equipamento!;
        }

        public async Task NewEquipamento(Equipamento equipamento)
        {
            _database.tb_equipamento.Add(equipamento);
            await _database.SaveChangesAsync();
        }

        public async Task SalvarAlteracao(Equipamento equipamento)
        {
            _database.tb_equipamento.Update(equipamento);
            await _database.SaveChangesAsync();
        }
    }
}