using System.ComponentModel.DataAnnotations;
using backend.Model.Enum;

namespace backend.Model
{
    public class Usuario
    {
        [Key]
        public long Id { get; set; }
        public required string Nome { get; set; }
        public required string Username { get; set; }
        public required string Senha { get; set; }
        public required string Email { get; set; }
        public required bool Ativo { get; set; }
        public required TipoUsuario Tipo { get; set; }
        public ICollection<SetorUsuario> SetoresSuporte { get; set; } = [];
    }
}