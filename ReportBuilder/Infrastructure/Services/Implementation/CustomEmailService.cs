
using System.Net;
using ReportBuilder.Infrastructure.Services.Abstract;
using System.Net.Mail;

namespace ReportBuilder.Infrastructure.Services.Implementation
{
    public class CustomEmailService : IEmailService
    {
        private MailAddress _senderAddress;
        private SmtpClient _smtp;

        public CustomEmailService(string emailHost, int hostPort)
        {
            _smtp = new SmtpClient(emailHost, hostPort);
            // логин и пароль

        }

        public void Configure(string senderMail, string senderName, string password)
        {
            _senderAddress = new MailAddress(senderMail, senderName);
            _smtp.Credentials = new NetworkCredential(senderMail, password);
            _smtp.EnableSsl = true;
        }

        public void SendFile(string receiverMail, string filePath)
        {
            var receiverAddress = new MailAddress(receiverMail);
            var mail = new MailMessage(_senderAddress, receiverAddress) {Subject = "Report"};
            mail.Attachments.Add(new Attachment(filePath));

            _smtp.Send(mail);
        }

        public void Dispose()
        {
            if (_smtp != null)
            {
                _smtp.Dispose();
            }
        }
    }
}