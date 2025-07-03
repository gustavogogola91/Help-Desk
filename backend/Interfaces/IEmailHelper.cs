using backend.Model;

namespace backend.Interfaces
{
    public interface IEmailHelper
    {
        public Task SendEmailChamado(Chamado chamado);
        public Task SendEmailInternalError(Exception ex);
    }
}