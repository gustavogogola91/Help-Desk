using backend.Model.Enum;

namespace backend.DTO
{
    public class UsuarioPostDTO
    {
        public required string Nome { get; set; }
        public required string Username { get; set; }
        public required string Senha { get; set; }
        public required string Email { get; set; }
        public required bool Ativo { get; set; }
        public required TipoUsuario Tipo { get; set; }
        public required List<long> IdSetoresSuporte { get; set; }
    }
}