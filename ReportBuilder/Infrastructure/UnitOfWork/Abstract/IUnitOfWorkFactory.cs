
namespace ReportBuilder.Services.UnitOfWork.Abstract
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}