using EventContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventContracts.Models
{
    public class StateChangeMessage
    {
        public string WorkcenterId { get; set; }

        public DeviceState NewDeviceState { get; set; }

        public string Timestamp { get; set; }

        public StateChangeMessage(string workcenter, DeviceState devicestate)
        {
            WorkcenterId = workcenter;
            NewDeviceState = devicestate;
            Timestamp = "";
        }

        public StateChangeMessage()
        {

        }

    }
}
