using backend.Model.Enum;

namespace backend.DTO
{
    public class ChamadoAtendimentoDTO
    {
        public long Id { get; set; }
        public string? NomeUsuarioAtendimento { get; set; }
        public string? NomeSetorAtual { get; set; }
        public string? NomeSetorTransferencia { get; set; }
        public DateTime? InicioAtendimento { get; set; }
        public DateTime? DataTransferencia { get; set; }
        public DateTime? DataFinalizado { get; set; }
        public string? ObservacaoTransferencia { get; set; }
        public required StatusAtendimento Status { get; set; }
    }
}