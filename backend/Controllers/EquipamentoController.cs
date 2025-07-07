using backend.DTO;
using backend.Helpers;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/equipamento")]
    public class EquipamentoController(IEquipamentoService equipamentoService) : ControllerBase
    {
        private readonly IEquipamentoService _equipamentoService = equipamentoService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipamentoDTO>>> GetAllEquipamentos()
        {
            var list = await _equipamentoService.GetAllEquipamentos();
            return Ok(list);
        }

        [HttpGet("paginado")]
        public async Task<ActionResult<PagedList<EquipamentoDTO>>> GetAllEquipamentosPaged(int currentPage = 1)
        {
            var list = await _equipamentoService.GetAllEquipamentosPaged(currentPage);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipamentoDTO>> GetEquipamentoById(long id)
        {
            var equipamento = await _equipamentoService.GetEquipamentoById(id);
            return Ok(equipamento);
        }

        [HttpPost("novo")]
        public async Task<IActionResult> NewEquipamento([FromBody] EquipamentoPostDTO post)
        {
            await _equipamentoService.NewEquipamento(post);
            return Created();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ModifyStatus(long id)
        {
            await _equipamentoService.ModifyStatus(id);
            return Ok();
        }
        
    }
}