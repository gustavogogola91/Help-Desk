namespace backend.DTO
{
    public class EquipamentoPostDTO
    {
        public required string Nome { get; set; }
        public required long SetorId { get; set; }
        public required bool Ativo { get; set; }
    }
}