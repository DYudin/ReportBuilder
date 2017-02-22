using System;
using System.Data.Entity;
using ReportBuilder.Infrastructure.Repositories.Abstract;
using ReportBuilder.Services.UnitOfWork.Abstract;

namespace ReportBuilder.Infrastructure.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IOrderRepository _orderRepository;
        private readonly DbContext _context;
        private bool disposed;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            // todo
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
