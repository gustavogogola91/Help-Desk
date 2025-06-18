using System.ComponentModel.DataAnnotations;
using backend.Model.Enum;

namespace backend.Model
{
    public class Estabelecimento
    {
        [Key]
        public long Id { get; set; }
        public required string Nome { get; set; }
        public required StatusAtivo Status { get; set; }
    }
}