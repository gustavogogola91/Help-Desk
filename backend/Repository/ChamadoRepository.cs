using backend.Interfaces;
using backend.Model;
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

        public async Task NewChamado(Chamado chamado)
        {
            _database.tb_chamado.Add(chamado);
            await _database.SaveChangesAsync();
        }
    }
}