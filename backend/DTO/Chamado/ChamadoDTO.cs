namespace backend.DTO
{
    public class ChamadoDTO
    {
        public long Id { get; set; }
        public string? Protocolo { get; set; }
        public string? NomeSolicitante { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataAbertura { get; set; }
        public DateTime? DataFinalizado { get; set; }
        public string? NomeEquipamento { get; set; }
        public string? NomeSetorSolicitante { get; set; }
        public string? NomeEstabelecimento { get; set; }
        public string? Ramal { get; set; }
        public string? Computador { get; set; }
        public string? Ip { get; set; }
        public List<string>? Anexo { get; set; }
        public ICollection<ChamadoAcompanhamentoDTO>? Acompanhamentos { get; set; }
    }
}