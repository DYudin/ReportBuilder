using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportBuilder.Infrastructure.Services.Abstract
{
    public interface IEmailService
    {
        void Configure(string hostName, string hostPort, string password);
        void SendFile(string receiverMail, string filePath);
    }
}