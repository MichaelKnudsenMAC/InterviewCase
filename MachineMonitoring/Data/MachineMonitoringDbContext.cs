using MachineMonitoring.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace MachineMonitoring.Data
{
    public class MachineMonitoringDbContext : DbContext
    {
        public MachineMonitoringDbContext(DbContextOptions<MachineMonitoringDbContext> options) : base(options)
        {

        }

        public DbSet<Machine> Machines { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Machine>()
                .HasMany(e => e.OrderBacklog);

            modelBuilder.Entity<Machine>()
                .HasMany(e => e.HistoricalDeviceStates);
                
                                        
        }

    }
}
