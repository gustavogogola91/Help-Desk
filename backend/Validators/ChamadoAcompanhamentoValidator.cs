using backend.DTO;
using backend.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace backend.Validators
{
    public class ChamadoAcompanhamentoValidator : AbstractValidator<ChamadoAcompanhamentoPostDTO>
    {
        private readonly IChamadoRepository _chamadoRepository;
        public ChamadoAcompanhamentoValidator(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;

            RuleFor(c => c.ChamadoId)
                .GreaterThan(0).WithMessage("O ID do chamado deve ser maior que zero")
                .MustAsync(async (chamadoId, cancellation) => await _chamadoRepository.ExisteAsync(chamadoId)).WithMessage("O chamado com o ID especificado não foi encontrado.");
        }
    }
}