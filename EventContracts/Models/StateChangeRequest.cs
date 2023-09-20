namespace EventContracts.Models
{
    public class StateChangeRequest
    {

        public string WorkcenterId { get; set; }

        public string RequestedDeviceState { get; set; }
    }
}
