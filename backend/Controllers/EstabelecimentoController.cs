using backend.DTO;
using backend.Helpers;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/estabelecimento")]
    public class EstabelecimentoController(IEstabelecimentoService estabelecimentoService) : ControllerBase
    {
        private readonly IEstabelecimentoService _estabelecimentoService = estabelecimentoService;

        [HttpGet]
        public async Task<ActionResult<PagedList<EstabelecimentoDTO>>> GetAllEstabelecimentos(int currentPage = 1)
        {
            var list = await _estabelecimentoService.GetAllEstabelecimentos(currentPage);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstabelecimentoDTO>> GetEstabelecimentoById(long id)
        {
            var estabelecimento = await _estabelecimentoService.GetEstabelecimentoById(id);
            return Ok(estabelecimento);
        }

        [HttpPost("novo")]
        public async Task<IActionResult> NewEstabelecimento([FromBody] EstabelecimentoPostDTO post)
        {
            await _estabelecimentoService.NewEstabelecimento(post);
            return Created();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ModifyStatus(long id)
        {
            await _estabelecimentoService.ModifyStatus(id);
            return Ok();
        }
    }
}