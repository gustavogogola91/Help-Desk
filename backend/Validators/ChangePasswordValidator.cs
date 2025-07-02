using backend.DTO;
using FluentValidation;

namespace backend.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordValidator()
        {
            RuleFor(c => c.NovaSenha)
                .NotEmpty().WithMessage("{PropertyName} é obrigatório")
                .Length(8, 32).WithMessage("{PropertyName} deve conter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.SenhaAtual)
                .NotEmpty().WithMessage("{PropertyName} é obrigatório");
        }
    }
}