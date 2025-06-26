using backend.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _database;

        public UsuarioRepository(AppDbContext database)
        {
            _database = database;
        }

        public async Task<bool> ExisteAsync(long id)
        {
            return await _database.tb_usuario.AnyAsync(u => u.Id == id);
        }
    }
}