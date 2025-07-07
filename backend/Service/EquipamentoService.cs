using AutoMapper;
using backend.DTO;
using backend.Exceptions;
using backend.Helpers;
using backend.Interfaces;
using backend.Model;
using FluentValidation;

namespace backend.Service
{
    public class EquipamentoService(IEquipamentoRepository equipamentoRepository, IMapper mapper, IValidator<EquipamentoPostDTO> validator) : IEquipamentoService
    {
        private readonly IEquipamentoRepository _equipamentoRepository = equipamentoRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<EquipamentoPostDTO> _validator = validator;


        public async Task<List<EquipamentoDTO>> GetAllEquipamentos()
        {
            var equipamentos = await _equipamentoRepository.GetAllEquipamentos();

            if (!equipamentos.Any())
            {
                throw new NotFoundException("Nenhum equipamento encontrado");
            }

            var equipamentosDTO = _mapper.Map<List<EquipamentoDTO>>(equipamentos);

            return equipamentosDTO;
        }

        public async Task<PagedList<EquipamentoDTO>> GetAllEquipamentosPaged(int currentPage)
        {
            var equipamentos = await _equipamentoRepository.GetAllEquipamentosPaged(currentPage);

            if (!equipamentos.Any())
            {
                throw new NotFoundException("Nenhum equipamento encontrado");
            }

            var equipamentosDTO = _mapper.Map<IEnumerable<EquipamentoDTO>>(equipamentos);

            var pagedList = new PagedList<EquipamentoDTO>(equipamentosDTO, currentPage, equipamentos.PageSize, equipamentos.TotalCount);

            return pagedList;
        }

        public async Task<EquipamentoDTO> GetEquipamentoById(long id)
        {
            var equipamento = await _equipamentoRepository.GetEquipamentoById(id) ?? throw new NotFoundException("Equipamento não encontrado", id);

            var equipamentoDTO = _mapper.Map<EquipamentoDTO>(equipamento);

            return equipamentoDTO;
        }

        public async Task ModifyStatus(long id)
        {
            var equipamento = await _equipamentoRepository.GetEquipamentoById(id) ?? throw new NotFoundException("Equipamento não encontrado", id);
            equipamento.Ativo = !equipamento.Ativo;
            await _equipamentoRepository.SalvarAlteracao(equipamento);
        }

        public async Task NewEquipamento(EquipamentoPostDTO post)
        {
            var validacao = _validator.Validate(post);

            if (!validacao.IsValid)
            {
                throw new ValidationException("Informações do equipamento inválidas", validacao.Errors);
            }

            var equipamento = _mapper.Map<Equipamento>(validacao);

            await _equipamentoRepository.NewEquipamento(equipamento);
        }
    }
}