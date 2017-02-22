using System;
using System.Data.Entity;
using ReportBuilder.Infrastructure.Repositories.Abstract;
using ReportBuilder.Infrastructure.Repositories.Implementation;
using ReportBuilder.Services.UnitOfWork.Abstract;

namespace ReportBuilder.Infrastructure.UnitOfWork.Implementation
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private IOrderRepository _orderRepository;
        private readonly ReportBuilderContext _context;
        private bool disposed;

        public EfUnitOfWork()
        {
            _context = new ReportBuilderContext();
        }

        public IOrderRepository OrderRepository
        {
            get { return _orderRepository ?? (_orderRepository = new OrderRepository(_context)); }
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
