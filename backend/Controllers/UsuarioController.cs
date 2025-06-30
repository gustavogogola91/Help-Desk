using backend.DTO;
using backend.Helpers;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<UsuarioDTO>>> GetAllUsuarios(int currentPage = 1)
        {
            var list = await _usuarioService.GetAllUsuarios(currentPage);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarioById(int id)
        {
            var usuario = await _usuarioService.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound($"Usuário ID ${id} não encontrado");
            }
            return Ok(usuario);
        }

        [HttpGet("/setor/{id}")]
        public async Task<ActionResult<PagedList<UsuarioDTO>>> GetUsuarioBySetor(long id, int currentPage = 1)
        {
            var list = await _usuarioService.GetUsuarioBySetor(id, currentPage);
            if (list == null || !list.Any())
            {
                return NotFound("Nenhum usuário encontrado");
            }
            return Ok(list);
        }

        [HttpPost("novo")]
        public async Task<IActionResult> NewUsuario([FromBody] UsuarioPostDTO post)
        {
            await _usuarioService.NewUsuario(post);

            return Created();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> AlterarStatus(long id)
        {
            await _usuarioService.AlterarStatus(id);

            return Ok();
        }

        // TODO: Endpoints para alteração das informações do usuário
    }
}