using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class Setor
    {
        [Key]
        public long Id { get; set; }
        public required string Nome { get; set; }  
        public required long EstabelecimentoId { get; set; }
        public Estabelecimento? Estabelecimento { get; set; }
        public required bool Suporte { get; set; }
        public required bool Ativo { get; set; }
    }
}