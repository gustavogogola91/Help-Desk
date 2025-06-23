namespace backend.DTO
{
    public class ChamadoAcompanhamentoPostDTO
    {
        public long ChamadoId { get; set; }
        public long UsuarioId { get; set; }
        public required string Conteudo { get; set; }
    }
}