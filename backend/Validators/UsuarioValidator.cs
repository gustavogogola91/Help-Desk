using backend.DTO;
using FluentValidation;

namespace backend.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioPostDTO>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("{PropertyName} é obrigatório")
                .MaximumLength(60).WithMessage("{PropertyName} deve conter no máximo {MaxLength} caracteres");

            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("{PropertyName} é obrigatório")
                .MaximumLength(60).WithMessage("{PropertyName} deve conter no máximo {MaxLength} caracteres");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("{PropertyName} é obrigatório")
                .Length(8, 32).WithMessage("{PropertyName} deve conter entre {MinLength} e {MaxLength}");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("{PropertyName} é obrigatório")
                .EmailAddress().WithMessage("{PropertyName} deve ser um email válido")
                .MaximumLength(120).WithMessage("{PropertyName} deve conter no máximo {MaxLength} caracteres");

            RuleFor(u => u.Tipo)
                .NotEmpty().WithMessage("{PropertyName} é obrigatório");

            RuleFor(u => u.Grupos)
                .NotEmpty().WithMessage("{PropertyName} é obrigatório");
        }
    }
}