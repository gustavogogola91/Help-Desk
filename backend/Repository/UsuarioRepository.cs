using backend.DTO;
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

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _database.tb_usuario.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _database.tb_usuario.AnyAsync(u => u.Username == username);
        }
        
        public async Task<PagedList<Usuario>> GetAllUsuarios(int currentPage)
        {
            var query = _database.tb_usuario.AsQueryable();
            var pagedList = await _pagination.CreateAsync(query, currentPage, 10);

            return pagedList;
        }

        public async Task<Usuario> GetUsuarioById(long id)
        {
            var usuario = await _database.tb_usuario.FirstOrDefaultAsync(u => u.Id == id);
            return usuario!;
        }

        public async Task<PagedList<UsuarioDTO>> GetUsuariosBySetor(long setorId, int currentPage)
        {
            var query = _database.tb_setor_usuario.Where(su => su.SetorId == setorId)
                .Include(su => su.Usuario)
                .Select(su => new UsuarioDTO
                {
                    Id = su.Usuario!.Id,
                    Nome = su.Usuario.Nome,
                    Username = su.Usuario.Username,
                    Email = su.Usuario.Email,
                    Ativo = su.Usuario.Ativo,
                    Tipo = su.Usuario.Tipo
                }).AsQueryable();

            var pagedList = await _pagination.CreateAsync(query, currentPage, 10);

            return pagedList;
        }

        public async Task<List<string>> GetEmailsBySetor(long setorId)
        {
            var emails = await _database.tb_setor_usuario.Where(su => su.SetorId == setorId)
                .Include(su => su.Usuario)
                .Select(su => su.Usuario!.Email).ToListAsync();

            return emails;
        }

        public async Task NewUsuario(Usuario usuario)
        {
            _database.tb_usuario.Add(usuario);
            await _database.SaveChangesAsync();
        }

        public async Task SalvarAlteracao(Usuario usuario)
        {
            _database.tb_usuario.Update(usuario);
            await _database.SaveChangesAsync();
        }
    }
}