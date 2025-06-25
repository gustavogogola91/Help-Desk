using backend.DTO;
using Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace backend.Validators
{
    public class ChamadoAcompanhamentoValidator : AbstractValidator<ChamadoAcompanhamentoPostDTO>
    {
        private readonly AppDbContext _database;
        public ChamadoAcompanhamentoValidator(AppDbContext db)
        {
            _database = db;

            RuleFor(c => c.ChamadoId)
                .GreaterThan(0).WithMessage("O ID do chamado deve ser maior que zero")
                .MustAsync(async (chamadoId, cancellation) =>
                {
                    return await _database.tb_chamado.AnyAsync(c => c.Id == chamadoId, cancellation);
                }).WithMessage("O chamado com o ID especificado não foi encontrado.");
        }
    }
}