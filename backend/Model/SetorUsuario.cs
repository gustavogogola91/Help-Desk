using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class SetorUsuario(long setorId)
    {
        [Key]
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public long SetorId { get; set; } = setorId;
        public Setor? Setor { get; set; }
    }
}