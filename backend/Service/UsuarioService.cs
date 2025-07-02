using System.Net;
using System.Security.Authentication;
using AutoMapper;
using backend.DTO;
using backend.Exceptions;
using backend.Helpers;
using backend.Interfaces;
using backend.Model;
using FluentValidation;

namespace backend.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IConfiguration _config;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISetorRepository _setorRepository;
        private readonly IValidator<UsuarioPostDTO> _usuarioValidator;
        private readonly IValidator<ChangePasswordDTO> _changePasswordValidator;
        private readonly IMapper _mapper;
        private readonly IEncryptHelper _hasher;

        public UsuarioService(IConfiguration config, IUsuarioRepository usuarioRepository, ISetorRepository setorRepository, IValidator<UsuarioPostDTO> usuarioValidator, IValidator<ChangePasswordDTO> changePasswordValidator, IMapper mapper, IEncryptHelper hasher)
        {
            _config = config;
            _usuarioRepository = usuarioRepository;
            _setorRepository = setorRepository;
            _usuarioValidator = usuarioValidator;
            _changePasswordValidator = changePasswordValidator;
            _mapper = mapper;
            _hasher = hasher;
        }

        public async Task ModifyStatus(long id)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id) ?? throw new NotFoundException("Usuário não encontrado", id);
            usuario.Ativo = !usuario.Ativo;

            await _usuarioRepository.SalvarAlteracao(usuario);
        }

        public async Task<PagedList<UsuarioDTO>> GetAllUsuarios(int currentPage)
        {
            var usuarios = await _usuarioRepository.GetAllUsuarios(currentPage);

            if (!usuarios.Any())
            {
                throw new NotFoundException("Nenhum usuário encontrado");
            }

            var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);

            var pagedlist = new PagedList<UsuarioDTO>(usuariosDTO, currentPage, usuarios.TotalPages, usuarios.PageSize, usuarios.TotalCount);

            return pagedlist;
        }

        public async Task<UsuarioDTO> GetUsuarioById(long id)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id) ?? throw new NotFoundException("Usuário não encontrado", id);
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);

            return usuarioDTO;
        }

        public async Task<PagedList<UsuarioDTO>> GetUsuarioBySetor(long idSetor, int currentPage)
        {
            var setorExiste = await _setorRepository.ExisteAsync(idSetor);
            if (!setorExiste)
            {
                throw new NotFoundException("O Setor especificado não existe.", idSetor);
            }

            var usuarios = await _usuarioRepository.GetUsuariosBySetor(idSetor, currentPage);

            return usuarios;
        }

        public async Task NewUsuario(UsuarioPostDTO post)
        {
            var validacao = _usuarioValidator.ValidateAsync(post).Result;

            if (!validacao.IsValid)
            {
                throw new ValidationException("Informações do usuário inválidas", validacao.Errors);
            }

            Usuario usuario = _mapper.Map<Usuario>(post);

            usuario.Senha = _hasher.EncryptPassword(usuario.Senha);

            post.IdSetoresSuporte.ForEach((id) =>
            {
                usuario.SetoresSuporte.Add(new SetorUsuario(id));
            });

            await _usuarioRepository.NewUsuario(usuario);
        }

        public async Task UserChangePassword(long id, ChangePasswordDTO dto)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id) ?? throw new NotFoundException("Usuário não encontrado", id);

            var validacao = _changePasswordValidator.Validate(dto);

            if (!validacao.IsValid)
            {
                throw new ValidationException("Informações inválidas", validacao.Errors);
            }

            if (!_hasher.VerifyPassword(dto.SenhaAtual, usuario.Senha))
            {
                throw new InvalidCredentialException("Credenciais inválidas");
            }

            usuario.Senha = _hasher.EncryptPassword(dto.NovaSenha);

            await _usuarioRepository.SalvarAlteracao(usuario);
        }
        //TODO: bloquear para apenas admins apos implementar JWT
        public async Task AdminResetUserPassword(long id)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id) ?? throw new NotFoundException("Usuário não encontrado", id);

            var defaultPassword = _config["Default:Password"] ?? throw new Exception("Senha padrão não encontrada");

            usuario.Senha = _hasher.EncryptPassword(defaultPassword);

            await _usuarioRepository.SalvarAlteracao(usuario);
        }
        //TODO: bloquear para apenas admins após implementar JWT
        public async Task ModifyUsuario(UsuarioPutDTO put, long id)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id) ?? throw new NotFoundException("Usuário não encontrado", id);

            usuario.Nome = put.Nome ?? usuario.Nome;

            if (put.Username != null)
            {
                if (await _usuarioRepository.UsernameExistsAsync(put.Username))
                {
                    usuario.Username = put.Username;
                }
                else
                {
                    throw new ArgumentException($"Username {put.Username} já está em uso");
                }
            }
            usuario.Email = put.Email ?? usuario.Email;
            usuario.Tipo = put.Tipo ?? usuario.Tipo;

            await _usuarioRepository.SalvarAlteracao(usuario);
        }
    }
}