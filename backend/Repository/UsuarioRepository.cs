using backend.DTO;
using backend.Helpers;
using backend.Interfaces;
using backend.Model;
using Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class UsuarioRepository(AppDbContext database, IPaginationHelper pagination) : IUsuarioRepository
    {
        private readonly AppDbContext _database = database;
        private readonly IPaginationHelper _pagination = pagination;

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
        
        public async Task<PagedList<Usuario>> GetAllUsuariosPaged(int currentPage)
        {
            var query = _database.tb_usuario.AsQueryable();
            var pagedList = await _pagination.CreateAsync(query, currentPage, 10);

            return pagedList;
        }

        public async Task<List<Usuario>> GetAllUsuarios()
        {
            var usuarios = await _database.tb_usuario.Where(u => u.Ativo).ToListAsync();
            return usuarios;
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
                .Where(su => su.Usuario!.Ativo)
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