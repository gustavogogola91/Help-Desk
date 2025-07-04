using backend.DTO;
using backend.Helpers;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/setor")]
    public class SetorController(ISetorService setorService) : ControllerBase
    {
        private readonly ISetorService _setorService = setorService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SetorDTO>>> GetAllSetores()
        {
            var list = await _setorService.GetAllSetores();
            return Ok(list);
        }

        [HttpGet("paginado")]
        public async Task<ActionResult<PagedList<SetorDTO>>> GetAllSetoresPaged(int currentPage = 1)
        {
            var list = await _setorService.GetAllSetoresPaged(currentPage);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SetorDTO>> GetSetorById(long id)
        {
            var setor = await _setorService.GetSetorById(id);
            return Ok(setor);
        }

        [HttpPost("novo")]
        public async Task<IActionResult> NewSetor([FromBody] SetorPostDTO post)
        {
            await _setorService.NewSetor(post);
            return Created();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ModifyStatus(long id)
        {
            await _setorService.ModifyStatus(id);
            return Ok();
        } 
    }
}