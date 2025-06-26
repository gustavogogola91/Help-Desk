using backend.DTO;
using backend.Interfaces;
using FluentValidation;

namespace backend.Validators
{
    public class ChamadoValidator : AbstractValidator<ChamadoPostDTO>
    {
        private readonly IEquipamentoRepository _equipamentoRepository;
        private readonly ISetorRepository _setoreRepository;
        private readonly IEstabelecimentoRepository _estabelecimentoRepository;

        public ChamadoValidator(IEquipamentoRepository equipamentoRepository, ISetorRepository setorRepository, IEstabelecimentoRepository estabelecimentoRepository)
        {
            _equipamentoRepository = equipamentoRepository;
            _setoreRepository = setorRepository;
            _estabelecimentoRepository = estabelecimentoRepository;

            RuleFor(c => c.NomeSolicitante)
                .NotEmpty().WithMessage("{PropertyName}  é obrigatório")
                .MaximumLength(60).WithMessage("{PropertyName} deve conter no máximo {MaxLength} caracteres");

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("{PropertyName}  é obrigatório");

            RuleFor(c => c.EquipamentoId)
                .GreaterThan(0).WithMessage("O ID do equipamento deve ser maior que zero")
                .MustAsync(async (setorId, cancellation) => await _equipamentoRepository.ExisteAsync(setorId))
                .WithMessage("O equipamento com o ID especificado não foi encontrado.");

            RuleFor(c => c.SetorSolicitanteId)
                .GreaterThan(0).WithMessage("O ID do setor deve ser maior que zero")
                .MustAsync(async (setorId, cancellation) => await _setoreRepository.ExisteAsync(setorId))
                .WithMessage("O Setor com o ID especificado para {PropertyName} não foi encontrado.");

            RuleFor(c => c.SetorDestinoId)
                .GreaterThan(0).WithMessage("O ID do setor deve ser maior que zero")
                .MustAsync(async (setorId, cancellation) => await _setoreRepository.ExisteAsync(setorId))
                .WithMessage("O Setor com o ID especificado para {PropertyName} não foi encontrado.");

            RuleFor(c => c.EstabelecimentoId)
                .GreaterThan(0).WithMessage("O ID do equipamento deve ser maior que zero")
                .MustAsync(async (estabelecimentoId, cancellation) => await _estabelecimentoRepository.ExisteAsync(estabelecimentoId))
                .WithMessage("O Estabelecimento com o ID especificado não foi encontrado.");

            RuleFor(c => c.Ramal)
                .NotEmpty().WithMessage("{PropertyName}  é obrigatório")
                .MaximumLength(6).WithMessage("{PropertyName} deve conter no máximo {MaxLength} caracteres");

            RuleFor(c => c.Computador)
                .NotEmpty().WithMessage("{PropertyName}  é obrigatório");

            RuleFor(c => c.Ip)
                .NotEmpty().WithMessage("{PropertyName}  é obrigatório")
                .MaximumLength(15).WithMessage("{PropertyName} deve conter no máximo {MaxLength} caracteres");
        }
    }
}