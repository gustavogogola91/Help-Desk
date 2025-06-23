namespace backend.DTO
{
    public class ChamadoAcompanhamentoDTO
    {
        public long Id { get; set; }
        public string? NomeUsuario { get; set; }
        public string? Conteudo { get; set; }
        public DateTime? DataMensagem { get; set; }
    }
}