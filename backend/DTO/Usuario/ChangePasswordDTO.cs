namespace backend.DTO
{
    public class ChangePasswordDTO
    {
        public required string SenhaAtual { get; set; }
        public required string NovaSenha { get; set; }
    }
}