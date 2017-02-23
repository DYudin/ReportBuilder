
using System;
using System.Collections.Generic;
using System.IO;
using ReportBuilder.ViewModels;

namespace ReportBuilder.Infrastructure.Services.Abstract
{
    public interface IReportBuilder : IDisposable
    {
        string CreateReportFile(IEnumerable<OrderViewModel> orders);

        void DeleteLastReportFile();
    }
}