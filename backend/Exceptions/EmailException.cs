namespace backend.Exceptions
{
    public class EmailException : Exception
    {
        public EmailException() { }

        public EmailException(string? message) : base(message) { }

        public EmailException(Exception innerException) : base("Ocorreu um erro ao processar o e-mail.", innerException) { }

        public EmailException(string message, Exception innerException) : base(message, innerException) { }

    }
}