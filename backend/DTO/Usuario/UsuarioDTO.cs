using backend.Model.Enum;

namespace backend.DTO
{
    public class UsuarioDTO
    {
        public long Id { get; set; }
        public string? Nome { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool Ativo { get; set; }
        public TipoUsuario Tipo { get; set; }
        public List<GrupoSuporte>? Grupos { get; set; }
    }
}