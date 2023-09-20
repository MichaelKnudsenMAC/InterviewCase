using EventContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MachineSimulator
{
    public class LocalMachine : INotifyPropertyChanged
    {
        private DeviceState _currentDeviceState;
        
        public DeviceState CurrentMachineState
        {
            get { return _currentDeviceState; }

            set
            {
                _currentDeviceState = value;
                NotifyPropertyChanged(nameof(CurrentMachineState));
            }
        }
        
        public int Id { get; set; }

        public string WorkcenterId { get; set; }

        public LocalMachine(int id, string workCenter)
        {
            CurrentMachineState = DeviceState.Running;
            Id = id;
            WorkcenterId = workCenter;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
