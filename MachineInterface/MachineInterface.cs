using EventContracts.Enums;
using EventContracts.Models;
using MachineSimulator;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

namespace EventContracts
{
    public class MachineInterface : BackgroundService
    {
        private SimulatorService _simulatorService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MachineInterface> _logger;
        private readonly IBus _bus;

        public MachineInterface(ILogger<MachineInterface> logger, IServiceProvider serviceProvider, IBus bus)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _bus = bus;

            _simulatorService = serviceProvider.GetRequiredService<SimulatorService>();
            _simulatorService.StartAsync();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SubscribeToMachineEvents();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Worker running. Sending heartbeat, and waiting on events");
                
                await Task.Delay(10000, stoppingToken);

                //await _bus.Publish(new StateChangeRequest() { WorkcenterId = "140491", RequestedDeviceState = "Setup" });

            }
        }

        private void SubscribeToMachineEvents()
        {
            foreach (var machine in _simulatorService.Machines)
            {
                machine.PropertyChanged += new PropertyChangedEventHandler(OnMachineChange);
            }
        }

        public bool SetMachineToSetup(string workcenter)
        {
            _simulatorService.Machines.FirstOrDefault(d => d.WorkcenterId.Equals(workcenter)).CurrentMachineState = DeviceState.Starting;
            return true;
        }

        public bool SetMachineToRunning(string workcenter)
        {
            _simulatorService.Machines.FirstOrDefault(d => d.WorkcenterId.Equals(workcenter)).CurrentMachineState = DeviceState.Running;
            return true;
        }
        
        private void OnMachineChange(object? sender, PropertyChangedEventArgs e)
        {
            LocalMachine machine = (LocalMachine)sender;
            string property = e.PropertyName;

            if (property == "CurrentMachineState")
            {
                _logger.LogDebug("Device state changed on machine {workcenter}, to status {state}", machine.WorkcenterId, machine.CurrentMachineState);
                
                var message = new StateChangeMessage(machine.WorkcenterId, machine.CurrentMachineState);
                
                _bus.Publish(message);
            }
        }
    }
}