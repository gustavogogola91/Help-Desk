using AutoMapper;
using backend.DTO;
using backend.Helpers;
using backend.Interfaces;
using FluentValidation;

namespace backend.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IValidator<UsuarioPostDTO> _usuarioValidator;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IValidator<UsuarioPostDTO> usuarioValidator, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioValidator = usuarioValidator;
            _mapper = mapper;
        }

        public async Task<PagedList<UsuarioDTO>> GetAllUsuarios(int currentPage)
        {
            var usuarios = await _usuarioRepository.GetAllUsuarios(currentPage);

            var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);

            var pagedlist = new PagedList<UsuarioDTO>(usuariosDTO, currentPage, usuarios.TotalPages, usuarios.PageSize, usuarios.TotalCount);

            return pagedlist;
        }

        public async Task<UsuarioDTO> GetUsuarioById(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id);

            if (usuario == null)
            {
                return null!;
            }

            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);

            return usuarioDTO;
        }
    }
}