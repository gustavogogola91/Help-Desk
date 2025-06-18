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
        public Equipamento? Equipamento { get; set; }
        public required long SetorSolicitanteId { get; set; }
        public Setor? SetorSolicitante { get; set; }
        public required long SetorDestinoId { get; set; }
        public Setor? SetorDestino { get; set; }
        public required long EstabelecimentoId { get; set; }
        public Estabelecimento? Estabelecimento{ get; set; }
        public required string Ramal { get; set; }
        public required string Computador { get; set; }
        public required string Ip { get; set; }
        public ICollection<string>? Anexo { get; set; }
    }
}