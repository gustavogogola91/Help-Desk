using backend.DTO;
using backend.Interfaces;
using FluentValidation;

namespace backend.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioPostDTO>
    {
        private readonly ISetorRepository _setorRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioValidator(ISetorRepository setorRepository, IUsuarioRepository usuarioRepository)
        {
            _setorRepository = setorRepository;
            _usuarioRepository = usuarioRepository;

            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("{PropertyName} é obrigatório")
                .MaximumLength(60).WithMessage("{PropertyName} deve conter no máximo {MaxLength} caracteres");

            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("{PropertyName} é obrigatório")
                .MaximumLength(60).WithMessage("{PropertyName} deve conter no máximo {MaxLength} caracteres")
                .MustAsync(BeUniqueUsername).WithMessage("Este username já está em uso.");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("{PropertyName} é obrigatório")
                .Length(8, 32).WithMessage("{PropertyName} deve conter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("{PropertyName} é obrigatório")
                .EmailAddress().WithMessage("{PropertyName} deve ser um email válido")
                .MaximumLength(120).WithMessage("{PropertyName} deve conter no máximo {MaxLength} caracteres");

            RuleFor(u => u.IdSetoresSuporte)
                .NotNull().WithMessage("A lista de setores de suporte não deve ser nula")
                .Must(ids => ids != null && ids.Count != 0).WithMessage("Usuario deve estar ligado a pelo menos um setor")
                .ForEach(id =>
                {
                    id.MustAsync(BeAnExistingSetor).WithMessage("Um dos IDs de setor de suporte não existe.");
                });
        }

        private async Task<bool> BeAnExistingSetor(long setorId, CancellationToken cancellationToken)
        {
            return await _setorRepository.ExisteAsync(setorId);
        }

        private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return !await _usuarioRepository.UsernameExistsAsync(username);
        }
    }
}