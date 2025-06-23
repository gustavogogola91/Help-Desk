namespace backend.DTO
{
    public class SetorDTO
    {
        public long Id { get; set; }
        public string? Nome { get; set; }
        public string? NomeEstabelecimento { get; set; }
        public bool Suporte { get; set; }
        public bool Ativo { get; set; }
    }
}