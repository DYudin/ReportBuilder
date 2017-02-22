using System;

namespace ReportBuilder.Infrastructure
{
    public class ReportBuilderException : Exception
    {
        public ReportBuilderException(string message) : this(message, null)
        { }

        public ReportBuilderException(string message, Exception innerException) : base (message, innerException)
        {
        }
    }
}
