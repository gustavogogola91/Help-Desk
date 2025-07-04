using AutoMapper;
using backend.DTO;
using backend.Exceptions;
using backend.Helpers;
using backend.Interfaces;
using FluentValidation;

namespace backend.Service
{
    public class SetorService(IConfiguration config, ISetorRepository setorRepository, IValidator<SetorPostDTO> setorValidator, IMapper mapper) : ISetorService
    {
        private readonly IConfiguration _config = config;
        private readonly ISetorRepository _setorRepository = setorRepository;
        private readonly IValidator<SetorPostDTO> _setorValidator = setorValidator;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<SetorDTO>> GetAllSetores()
        {
            var setores = await _setorRepository.GetAllSetores();

            if (setores.Count == 0)
            {
                throw new NotFoundException("Nenhum setor encontrado");
            }
            var setoresDTO = _mapper.Map<IEnumerable<SetorDTO>>(setores);

            return setoresDTO;
        }

        public async Task<PagedList<SetorDTO>> GetAllSetoresPaged(int currentPage)
        {
            var setores = await _setorRepository.GetAllSetoresPaged(currentPage);

            if (setores.Count == 0)
            {
                throw new NotFoundException("Nenhum setor encontrado");
            }

            var setoresDTO = _mapper.Map<IEnumerable<SetorDTO>>(setores);

            var pagedList = new PagedList<SetorDTO>(setoresDTO, currentPage, setores.TotalPages, setores.TotalCount);
            return pagedList;
        }

        public async Task<SetorDTO> GetSetorById(long id)
        {
            var setor = await _setorRepository.GetSetorById(id) ?? throw new NotFoundException("Setor não encontrado", id);
            var setorDTO = _mapper.Map<SetorDTO>(setor);

            return setorDTO;
        }

        public async Task ModifyStatus(long id)
        {
            var setor = await _setorRepository.GetSetorById(id) ?? throw new NotFoundException("Setor não encontrado", id);
            setor.Ativo = !setor.Ativo;

            await _setorRepository.SalvarAlteracao(setor);
        }

        public Task NewSetor(SetorPostDTO post)
        {
            throw new NotImplementedException();
        }
    }
}