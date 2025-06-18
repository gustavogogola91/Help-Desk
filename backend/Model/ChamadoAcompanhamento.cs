using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class ChamadoAcompanhamento
    {
        [Key]
        public long Id { get; set; }
        public long ChamadoId { get; set; }
        public Chamado? Chamado { get; set; }
        public long UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public required string Conteudo { get; set; }
        public required DateTime DataMensagem { get; set; }
    }
}