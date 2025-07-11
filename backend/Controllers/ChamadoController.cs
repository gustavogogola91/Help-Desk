using backend.DTO;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/chamado")]
    public class ChamadoController(IChamadoService chamadoService) : ControllerBase
    {
        private readonly IChamadoService _chamadoService = chamadoService;

        [HttpPost("novo")]
        public async Task<IActionResult> NewChamado(ChamadoPostDTO post)
        {
            var protocolo = await _chamadoService.NewChamado(post);
            return Created(protocolo, protocolo);
        }
    }
}