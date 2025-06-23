namespace backend.DTO
{
    public class SetorPostDTO
    {
        public required string Nome { get; set; }
        public required long EstabelecimentoId { get; set; }
        public required bool Suporte { get; set; }
        public required bool Ativo { get; set; }
    }
}