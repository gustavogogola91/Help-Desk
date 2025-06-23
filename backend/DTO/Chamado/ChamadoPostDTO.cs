namespace backend.DTO
{
    public class ChamadoPostDTO
    {
        public required string NomeSolicitante { get; set; }
        public required string Descricao { get; set; }
        public required long EquipamentoId { get; set; }
        public required long SetorSolicitanteId { get; set; }
        public required long SetorDestinoId { get; set; }
        public required long EstabelecimentoId { get; set; }
        public required string Ramal { get; set; }
        public required string Computador { get; set; }
        public required string Ip { get; set; }
        public List<string>? Anexo { get; set; }
    }
}