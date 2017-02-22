
using System.Data.Entity;
using ReportBuilder.Models;

namespace ReportBuilder.Infrastructure
{
    public class ReportBuilderContext : DbContext
    {
        public ReportBuilderContext()
            : base("OrderDB")
        {
            //Database.SetInitializer<TransactionSubsystemContext>(null);
        }

        public DbSet<Order> Orders { get; set; }
    }
}
