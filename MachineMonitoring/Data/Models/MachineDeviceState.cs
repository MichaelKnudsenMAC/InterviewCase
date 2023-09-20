using EventContracts.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MachineMonitoring.Data.Models
{
    public class MachineDeviceState
    {
        [Key]
        public int MachineDeviceStateId { get; set; }
        public string WorkcenterId { get; set; }
        public DeviceState DeviceState { get; set; }
        public DateTime DeviceStateChangedAt { get; set; }

    }
}
