using backend.DTO;
using FluentValidation;

namespace backend.Validators
{
    public class EstabelecimentoValidator : AbstractValidator<EstabelecimentoPostDTO>
    {
        public EstabelecimentoValidator()
        {
            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("{PropertyName}  é obrigatório")
                .MaximumLength(60).WithMessage("{PropertyName} deve conter no máximo {MaxLength} caracteres");
        }
    }
}