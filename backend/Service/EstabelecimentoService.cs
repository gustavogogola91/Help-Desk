using AutoMapper;
using backend.DTO;
using backend.Exceptions;
using backend.Helpers;
using backend.Interfaces;
using backend.Model;
using FluentValidation;

namespace backend.Service
{
    public class EstabelecimentoService : IEstabelecimentoService
    {
        private readonly IEstabelecimentoRepository _estabelecimentoRepository;
        private readonly IValidator<EstabelecimentoPostDTO> _estabelecimentoValidator;
        private readonly IMapper _mapper;

        public EstabelecimentoService(IEstabelecimentoRepository estabelecimentoRepository, IValidator<EstabelecimentoPostDTO> estabelecimentoValidator, IMapper mapper)
        {
            _estabelecimentoRepository = estabelecimentoRepository;
            _estabelecimentoValidator = estabelecimentoValidator;
            _mapper = mapper;
        }

        public async Task<PagedList<EstabelecimentoDTO>> GetAllEstabelecimentos(int currentPage)
        {
            var estabelecimentos = await _estabelecimentoRepository.GetAllEstabelecimentos(currentPage);

            if (!estabelecimentos.Any())
            {
                throw new NotFoundException("Nenhum Estabelecimento encontrado");
            }

            var estabelecimentosDTO = _mapper.Map<IEnumerable<EstabelecimentoDTO>>(estabelecimentos);

            var pagedList = new PagedList<EstabelecimentoDTO>(estabelecimentosDTO, currentPage, estabelecimentos.PageSize, estabelecimentos.TotalCount);

            return pagedList;
        }

        public async Task<EstabelecimentoDTO> GetEstabelecimentoById(long id)
        {
            var estabelecimento = await _estabelecimentoRepository.GetEstabelecimentoById(id) ?? throw new NotFoundException("Nenhum usuário encontrado", id);
            var estabelecimentoDTO = _mapper.Map<EstabelecimentoDTO>(estabelecimento);

            return estabelecimentoDTO;
        }

        public async Task NewEstabelecimento(EstabelecimentoPostDTO post)
        {
            var validacao = _estabelecimentoValidator.Validate(post);

            if (!validacao.IsValid)
            {
                throw new ValidationException("Informações do estabelecimento inválidas", validacao.Errors);
            }

            var estabelecimento = _mapper.Map<Estabelecimento>(post);

            await _estabelecimentoRepository.NewEstabelecimento(estabelecimento);
        }
    }
}