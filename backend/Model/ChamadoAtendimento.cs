using backend.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class ChamadoAtendimento
    {
        [Key]
        public long Id { get; set; }
        public required long ChamadoId { get; set; }
        public Chamado? Chamado { get; set; }
        public long? UsuarioAtendimentoId { get; set; }
        public Usuario? UsuarioAtendimento { get; set; }
        public required long SetorAtualId { get; set; }
        public Setor? SetorAtual { get; set; }
        public long? SetorTransferenciaId { get; set; }
        public Setor? SetorTransferencia { get; set; }
        public DateTime? InicioAtendimento { get; set; }
        public DateTime? DataTransferencia { get; set; }
        public DateTime? DataFinalizado { get; set; }
        public string? ObservacaoTransferencia { get; set; }
        public required StatusAtendimento Status { get; set; }
    }
}