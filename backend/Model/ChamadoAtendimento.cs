using backend.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class ChamadoAtendimento
    {
        [Key]
        public long Id { get; set; }
        public required long ChamadoId { get; set; }
        public Chamado? Chamado { get; set; } // TODO: implementar DTO
        public required long UsuarioAtendimentoId { get; set; }
        // TODO: busca inversa
        public required long SetorAtualId { get; set; }
        // TODO: busca inversa
        public long? SetorTransferenciaId { get; set; }
        // TODO: busca inversa
        public DateTime? InicioAtendimento { get; set; }
        public DateTime? DataTransferencia { get; set; }
        public DateTime? DataFinalizado { get; set; }
        public string? ObservacaoTransferencia { get; set; }
        public required StatusAtendimento Status { get; set; }
        
    }
}