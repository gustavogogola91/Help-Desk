using backend.DTO;
using backend.Interfaces;
using FluentValidation;

namespace backend.Validators
{
    public class SetorValidator : AbstractValidator<SetorPostDTO>
    {
        private readonly IEstabelecimentoRepository _estabelecimentoRepository;

        public SetorValidator(IEstabelecimentoRepository estabelecimentoRepository)
        {
            _estabelecimentoRepository = estabelecimentoRepository;

            RuleFor(s => s.Nome)
                .NotEmpty().WithMessage("{PropertyName} é obrigatório")
                .MaximumLength(60).WithMessage("{PropertyName} deve conter no máximo {MaxLength} caracteres");

            RuleFor(s => s.EstabelecimentoId)
                .GreaterThan(0).WithMessage("O ID do estabelecimento deve ser maior que zero")
                .MustAsync(async (estabelecimentoId, cancellation) => await _estabelecimentoRepository.ExisteAsync(estabelecimentoId))
                .WithMessage("O Estabelecimento com o ID especificado não foi encontrado.");
        }
    }
}