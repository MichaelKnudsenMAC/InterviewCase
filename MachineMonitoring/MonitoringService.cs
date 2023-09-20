﻿using MachineMonitoring.Data;
using MachineMonitoring.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MachineMonitoring
{
    public class MonitoringService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private MachineMonitoringDbContext _context;
        
        public MonitoringService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;           
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SeedOrders();
            
            while (stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(60000);
            }
        }

        private void SeedOrders()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                _context = scope.ServiceProvider.GetRequiredService<MachineMonitoringDbContext>();

                int orderCounter = _context.Orders.Count();

                if (orderCounter == 0)
                {
                    foreach (var machine in _context.Machines)
                    {
                        SeedOrderBacklog(machine);

                    }

                    _context.SaveChanges();
                }
            }
            
           
        }

        private void SeedOrderBacklog(Machine machine)
        {
            var rand = new Random();

            int numberOfOrders = rand.Next(5, 10);

            for (int i = 0; i < numberOfOrders; i++)
            {
                int orderNumber = rand.Next(100000, 999999);
                
                Order newOrder = new Order() { OrderNumber = orderNumber.ToString() };
                _context.Entry(newOrder).State = EntityState.Added;                
                machine.OrderBacklog.Add(newOrder);
            }
        }
    }
}
