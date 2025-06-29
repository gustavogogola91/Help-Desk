using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class SetorUsuario
    {
        public SetorUsuario(long setorId)
        {
            SetorId = setorId;
        }

        [Key]
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public long SetorId { get; set; }
        public Setor? Setor { get; set; }
    }
}