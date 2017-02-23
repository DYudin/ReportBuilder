using System;

namespace ReportBuilder.Infrastructure.Services.Abstract
{
    public interface IEmailService : IDisposable
    {
        void Configure(string hostName, string hostPort, string password);
        void SendFile(string receiverMail, string filePath);
    }
}