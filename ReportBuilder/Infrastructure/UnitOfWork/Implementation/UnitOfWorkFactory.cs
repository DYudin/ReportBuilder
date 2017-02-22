using System;
using System.Data.Entity;
using ReportBuilder.Services.UnitOfWork.Abstract;

namespace ReportBuilder.Infrastructure.UnitOfWork.Implementation
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly DbContext _dbContext;

        public UnitOfWorkFactory(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            _dbContext = dbContext;
        }

        public IUnitOfWork Create()
        {
            var result = new UnitOfWork(_dbContext);
            return result;
        }
    }
}
