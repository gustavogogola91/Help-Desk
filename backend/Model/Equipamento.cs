using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class Equipamento
    {
        [Key]
        public long Id { get; set; }
        public required string Nome { get; set; }
        public required long SetorId { get; set; }
        public Setor? Setor { get; set; }
        public required bool Ativo { get; set; }
    }
}