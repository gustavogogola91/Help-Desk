using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class Chamado
    {
        [Key]
        public long Id { get; set; }
        public required string Protocolo { get; set; }
        public required string NomeSolicitante { get; set; }
        public required string Descricao { get; set; }
        public required DateTime DataAbertura { get; set; }
        public DateTime? DataFinalizado { get; set; }
        public required long EquipamentoId { get; set; }
        // TODO: Busca inversa
        public required long SetorSolicitanteId { get; set; }
        // TODO: Busca inversa
        public required long SetorDestinoId { get; set; }
        // TODO: Busca inversa
        public required long EstabelecimentoId { get; set; }
        // TODO: Busca inversa
        public required string Ramal { get; set; }
        public required string Computador { get; set; }
        public required string Ip { get; set; }
        public ICollection<string>? Anexo { get; set; }
    }
}