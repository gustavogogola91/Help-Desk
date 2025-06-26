using backend.DTO;
using backend.Interfaces;
using FluentValidation;

namespace backend.Validators
{
    public class EquipamentoValidator : AbstractValidator<EquipamentoPostDTO>
    {
        private readonly ISetorRepository _setorRepository;

        public EquipamentoValidator(ISetorRepository setorRepository)
        {
            _setorRepository = setorRepository;

            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("{PropertyName}  é obrigatório")
                .MaximumLength(60).WithMessage("{PropertyName} deve conter no máximo {MaxLength} caracteres");

            RuleFor(e => e.SetorId)
                .GreaterThan(0).WithMessage("O ID do setor deve ser maior que zero")
                .MustAsync(async (setorId, cancellation) => await _setorRepository.ExisteAsync(setorId))
                .WithMessage("O Setor com o ID especificado não foi encontrado.");
        }
    }
}