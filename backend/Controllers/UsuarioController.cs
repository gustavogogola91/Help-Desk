using backend.DTO;
using backend.Helpers;
using backend.Interfaces;
using FluentValidation;
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
            return Ok(usuario);
        }

        [HttpGet("setor/{id}")]
        public async Task<ActionResult<PagedList<UsuarioDTO>>> GetUsuarioBySetor(long id, int currentPage = 1)
        {
            var list = await _usuarioService.GetUsuarioBySetor(id, currentPage);
            return Ok(list);
        }

        [HttpPost("novo")]
        public async Task<IActionResult> NewUsuario([FromBody] UsuarioPostDTO post)
        {
            await _usuarioService.NewUsuario(post);
            return Created();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ModifyStatus(long id)
        {
            await _usuarioService.ModifyStatus(id);
            return Ok();
        }

        // TODO: Endpoints para alteração das informações do usuário

        [HttpPatch("{id}/password")]
        public async Task<IActionResult> UserChangePassword([FromBody] ChangePasswordDTO dto, long id)
        {
            await _usuarioService.UserChangePassword(id, dto);
            return Ok();
        }

        // TODO: Criar metodo para o admin resetar a senha (em controller especifico talvez)
        [HttpPatch("admin/{id}/password")]
        public async Task<IActionResult> ResetPassword(long id)
        {
            await _usuarioService.AdminResetUserPassword(id);
            return Ok();
        }

        [HttpPut("alterar/{id}")]
        public async Task<IActionResult> ModifyUsuario([FromBody] UsuarioPutDTO put, long id)
        {
            await _usuarioService.ModifyUsuario(put, id);
            return Ok();
        }
    }
}