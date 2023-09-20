using EventContracts.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MachineMonitoring.Data.Models
{
    public class Machine
    {
        [Key]
        public int MachineId { get; set; }

        public DeviceState? CurrentMachineState { get; set; }
                
        public string? WorkcenterId { get; set; }

        public string? OrderCurrent { get; set; }

        public List<Order>? OrderBacklog { get; set; }

        public List<MachineDeviceState>? HistoricalDeviceStates { get; set; }

        public Machine(int id, string workCenter)
        {
            CurrentMachineState = DeviceState.Running;
            MachineId = id;
            WorkcenterId = workCenter;
            OrderBacklog = new List<Order>();
            HistoricalDeviceStates = new List<MachineDeviceState>();
        }

        public Machine()
        {
            OrderBacklog = new List<Order>();
            HistoricalDeviceStates = new List<MachineDeviceState>();
        }
    }
}



