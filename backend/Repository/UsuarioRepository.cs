using backend.Helpers;
using backend.Interfaces;
using backend.Model;
using Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _database;
        private readonly IPaginationHelper _pagination;

        public UsuarioRepository(AppDbContext database, IPaginationHelper pagination)
        {
            _database = database;
            _pagination = pagination;
        }

        public async Task<bool> ExisteAsync(long id)
        {
            return await _database.tb_usuario.AnyAsync(u => u.Id == id);
        }

        public async Task<PagedList<Usuario>> GetAllUsuarios(int currentPage)
        {
            var query = _database.tb_usuario.AsQueryable();
            var pagedList = await _pagination.CreateAsync(query, currentPage, 10);

            return pagedList;
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            var usuario = await _database.tb_usuario.FirstOrDefaultAsync(u => u.Id == id);
            return usuario!;
        }
    }
}