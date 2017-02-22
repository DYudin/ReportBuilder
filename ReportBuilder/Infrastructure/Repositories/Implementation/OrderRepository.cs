
using ReportBuilder.Infrastructure.Repositories.Abstract;
using ReportBuilder.Models;

namespace ReportBuilder.Infrastructure.Repositories.Implementation
{
    public class OrderRepository : EntityRepository<Order>, IOrderRepository
    {
        public OrderRepository(ReportBuilderContext context)
            : base(context)
        {
        }
    }
}
