using EventContracts.Models;
using MachineMonitoring.Data;
using MachineMonitoring.Data.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace MachineMonitoring
{
    public class DeviceStateConsumer : IConsumer<StateChangeMessage>
    {
        private readonly MachineMonitoringDbContext _context;
        private readonly ILogger<DeviceStateConsumer> _logger;

        public DeviceStateConsumer(MachineMonitoringDbContext context, ILogger<DeviceStateConsumer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<StateChangeMessage> context)
        {
            try
            {
                _logger.LogDebug("Consuming new device state from workcenter {workcenter}", context.Message.WorkcenterId);

                var machine = _context.Machines.FirstOrDefault(m => m.WorkcenterId.Equals(context.Message.WorkcenterId));

                if (machine != null)
                {
                    MachineDeviceState saveState = new MachineDeviceState
                    {
                        WorkcenterId = machine.WorkcenterId,
                        DeviceState = context.Message.NewDeviceState,
                        DeviceStateChangedAt = DateTime.Now                       
                    };

                    _context.Entry(saveState).State = EntityState.Added;

                    machine.HistoricalDeviceStates.Add(saveState);
                    machine.CurrentMachineState = context.Message.NewDeviceState;

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error consuming a new devicestate. Error was {error}", ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}
