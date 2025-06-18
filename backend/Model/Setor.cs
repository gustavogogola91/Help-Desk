using System.ComponentModel.DataAnnotations;
using backend.Model.Enum;

namespace backend.Model
{
    public class Setor
    {
        [Key]
        public long Id { get; set; }
        public required string Nome { get; set; }  
        public required long EstabelecimentoId { get; set; }
        public Estabelecimento? Estabelecimento { get; set; }
        public required bool IsSuporte { get; set; }
        public required StatusAtivo Status { get; set; }
    }
}