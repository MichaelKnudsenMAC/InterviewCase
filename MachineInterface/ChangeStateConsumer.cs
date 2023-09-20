using EventContracts.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventContracts
{
    internal class ChangeStateConsumer : IConsumer<StateChangeRequest>
    {
        private MachineInterface _machineInterface; 
        private readonly ILogger<ChangeStateConsumer> _logger;  
        
        public ChangeStateConsumer(MachineInterface machineInterface, ILogger<ChangeStateConsumer> logger)
        {
            _machineInterface = machineInterface;
            _logger = logger;
        }
        
        public Task Consume(ConsumeContext<StateChangeRequest> context)
        {
            var workcenter = context.Message.WorkcenterId;
            var deviceState = context.Message.RequestedDeviceState;

            if (deviceState == "Starting")
            {
                _machineInterface.SetMachineToSetup(workcenter);
                _logger.LogDebug("Changed device state on workcenter {workcenter}, to device state {state}", workcenter, deviceState);
            }

            if (deviceState == "Running")
            {
                _machineInterface.SetMachineToRunning(workcenter);
                _logger.LogDebug("Changed device state on workcenter {workcenter}, to device state {state}", workcenter, deviceState);
            }
            
            return Task.CompletedTask;
        }
    }
}
