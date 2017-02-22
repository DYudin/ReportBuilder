
using ReportBuilder.Services.UnitOfWork.Abstract;

namespace ReportBuilder.Infrastructure.UnitOfWork.Implementation
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new EfUnitOfWork();
        }
    }
}