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
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISetorRepository _setorRepository;
        private readonly IValidator<UsuarioPostDTO> _usuarioValidator;
        private readonly IMapper _mapper;
        private readonly IEncryptHelper _hasher;

        public UsuarioService(IUsuarioRepository usuarioRepository, ISetorRepository setorRepository, IValidator<UsuarioPostDTO> usuarioValidator, IMapper mapper,
            IEncryptHelper hasher)
        {
            _usuarioRepository = usuarioRepository;
            _setorRepository = setorRepository;
            _usuarioValidator = usuarioValidator;
            _mapper = mapper;
            _hasher = hasher;
        }

        public async Task ModifyStatus(long id)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id) ?? throw new NotFoundException($"Usuário não encontrado", id);
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
            var usuario = await _usuarioRepository.GetUsuarioById(id) ?? throw new NotFoundException("Nenhum usuário encontrado", id);
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
    }
}