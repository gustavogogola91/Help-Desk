using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class Estabelecimento
    {
        [Key]
        public long Id { get; set; }
        public required string Nome { get; set; }
        public required bool Ativo { get; set; }
    }
}