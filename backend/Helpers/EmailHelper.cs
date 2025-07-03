using backend.Exceptions;
using backend.Interfaces;
using backend.Model;
using MailKit.Net.Smtp;
using MimeKit;

namespace backend.Helpers
{
    public class EmailHelper(IConfiguration config, IUsuarioRepository usuarioRepository) : IEmailHelper
    {
        private readonly IConfiguration _config = config;
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task SendEmailChamado(Chamado chamado)
        {
            try
            {
                var diaDaSemana = DateTime.Now.DayOfWeek;
                var remetentes = await _usuarioRepository.GetEmailsBySetor(chamado.SetorDestinoId);

                var email = new MimeMessage();

                email.From.Add(new MailboxAddress(_config["Email:NomeRemetente"], _config["Email:EmailRemetente"]));

                if ((int)diaDaSemana == 0 || (int)diaDaSemana == 6)
                {
                    InternetAddressList list = [];
                    foreach (var remetente in remetentes)
                    {
                        list.Add(new MailboxAddress(remetente.Nome, remetente.Email));
                    }
                }
                else
                {
                    email.To.Add(new MailboxAddress(_config["Email:SobreAvisoNome"], _config["Email:EmailSobreAviso"]));
                }

                email.Subject = $"Novo chamado - {chamado.Protocolo}";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = @$"
                    <span>Segue chamado aberto no Help-Desk:</span><br/><br/>
                    <span style='font-weight: bold;'>Protocolo:</span> {chamado.Protocolo}<br/><br/>
                    <span style='font-weight: bold;'>Nome:</span> {chamado.NomeSolicitante}<br/>
                    <span style='font-weight: bold;'>Ramal:</span> {chamado.Ramal}<br/>
                    <span style='font-weight: bold;'>Estabelecimento:</span> {chamado.Estabelecimento!.Nome}
                    <span style='font-weight: bold;'>Setor Solicitante:</span> {chamado.SetorSolicitante!.Nome}
                    <span style='font-weight: bold;'>Equipamento:</span> {chamado.Equipamento!.Nome}
                    <span style='font-weight: bold;'>Ip:</span> {chamado.Ip}
                    <span style='font-weight: bold;'>Computador:</span> {chamado.Computador}
                    <span style='font-weight: bold; white-space: pre-line;'>Descricao:</span><br/><br/>
                    {chamado.Descricao}<br/><br/>
                    <span style='font-weight: bold;'>EMAIL DE ENVIO AUTOMÁTICO!</span>
                "
                };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(_config["Email:Host"], 587, false);

                    smtp.Authenticate(_config["Email:Username"], _config["Email:Senha"]);

                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                }
            }
            catch (Exception emailEx)
            {
                throw new EmailException(emailEx);
            }

        }

        public async Task SendEmailInternalError(Exception ex)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress(_config["Email:NomeRemetente"], _config["Email:EmailRemetente"]));
                email.To.Add(new MailboxAddress("Erro", _config["Email:EmailErro"]));


                email.Subject = $"Internal Server Error não tratado";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = @$"
                        <p><span style='font-weight: bold;'>Mensagem:</span> {ex.Message}</p>
                        <p><span style='font-weight: bold;'>StackTrace:</span> {ex.StackTrace}</p>
                        <span style='font-weight: bold;'>EMAIL DE ENVIO AUTOMÁTICO!</span>
                    "
                };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(_config["Email:Host"], 587, false);

                    smtp.Authenticate(_config["Email:Username"], _config["Email:Senha"]);

                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                }
            }
            catch (Exception emailEx)
            {
                throw new EmailException(emailEx);
            }
        }
    }
}