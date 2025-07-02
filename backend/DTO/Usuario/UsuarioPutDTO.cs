using backend.Model.Enum;

namespace backend.DTO
{
    public class UsuarioPutDTO
    {
        public string? Nome { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public TipoUsuario? Tipo { get; set; }
    }
}