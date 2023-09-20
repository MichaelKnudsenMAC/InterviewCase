using EventContracts.Enums;

namespace MachineSimulator
{
    public class SimulatorService
    {
        private readonly bool _running;
        private readonly ILogger<SimulatorService> _logger;
        public List<LocalMachine> Machines { get; private set; }

        private List<string> _workCenters = new List<string>() { "140491", "140494", "150370", "150372", "195930", "227430", "267838", "153576" };

        public SimulatorService(ILogger<SimulatorService> logger)
        {
            _logger = logger;
            Machines = new List<LocalMachine>();
            _running = true;
        }

        public async Task StartAsync()
        {
            CreateMachines();
                        
            while (_running)
            {
                _logger.LogInformation("SIMULATOR - Randomly updating machine states at: {time}", DateTimeOffset.Now);
                UpdateMachineStates();
                await Task.Delay(5000);
            }
        }

        private void UpdateMachineStates()
        {
            var rand = new Random();
            var change = rand.Next(0, 100);

            if (change > 85)
            {
                var id = rand.Next(1, Machines.Count);

                if (Machines[id].CurrentMachineState != DeviceState.Stopped)
                {
                    Machines[id].CurrentMachineState = DeviceState.Stopped;

                    _logger.LogDebug("SIMULATOR - Machine {workcenter} stopped at {time}", Machines[id].WorkcenterId, DateTime.Now.ToString());
                }
            }

        }

        private void CreateMachines()
        {
            int count = 1;
            
            foreach (var workcenter in _workCenters)
            {
                LocalMachine machine = new LocalMachine(count, workcenter);

                Machines.Add(machine);

                _logger.LogDebug("SIMULATOR - Created machine with Id {id} and WorkcenterId {workcenter}. Initial state {state}", machine.Id, machine.WorkcenterId, machine.CurrentMachineState);

                count++;
            }
        }
    }
}