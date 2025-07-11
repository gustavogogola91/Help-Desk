using System.Globalization;
using AutoMapper;
using backend.DTO;
using backend.Interfaces;
using backend.Model;
using FluentValidation;

namespace backend.Service
{
    public class ChamadoService(IChamadoRepository chamadoRepository, IValidator<ChamadoPostDTO> validator, IMapper mapper) : IChamadoService
    {
        private readonly IChamadoRepository _chamadoRepository = chamadoRepository;
        private readonly IValidator<ChamadoPostDTO> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task<string> NewChamado(ChamadoPostDTO post)
        {
            var validacao = await _validator.ValidateAsync(post);

            if (!validacao.IsValid)
            {
                throw new ValidationException("Informações do chamado inválidas", validacao.Errors);
            }

            var chamado = _mapper.Map<Chamado>(post);

            var data = DateTime.Now.ToString("ddMMyyyyHHmmss", CultureInfo.CurrentCulture);

            Random rnd = new();

            var aleatorio = rnd.Next(100).ToString("D2");
            string protocolo = data + aleatorio;

            chamado.Protocolo = protocolo;

            await _chamadoRepository.NewChamado(chamado);

            return protocolo;
        }
    }
}