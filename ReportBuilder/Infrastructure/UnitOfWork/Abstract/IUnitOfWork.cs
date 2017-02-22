using System;
using ReportBuilder.Infrastructure.Repositories.Abstract;

namespace ReportBuilder.Services.UnitOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository OrderRepository { get; }

        /// <summary>
        /// Commits all changes made on the unit of work.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls back all changes made on the unit of work.
        /// </summary>
        void Rollback();
    }
}
