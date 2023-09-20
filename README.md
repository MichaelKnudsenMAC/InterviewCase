# InterviewCase

## Installation instructions

1. Install RabbitMQ on Docker
  - You can use the InstallRabbit Powershell file
2. Build the projects
3. Run MachineInterface
  - This will also run the machine simulation that is a very simple simulation of 8 machines that once in a while stops, which creates an event for the MachineInterface to pickup
4. Run MachineMonitoring
  - You have swagger interface where you can, see the current state, and order of a device, set states, get the historical states of devices, and get the backlog of orders on a device

## Good to know

MachineMonitoring uses an Azure SQL database, which you should not be able to access, so if you want to run the code please let me know so i can grant access to your IP adresse
Other than this there is not given any consideration to security. 

## Design choices

The idea is basically that we have a workcenter with one or more devices (could be a moulding machine, could be a packing line), where we place an edge device to connect to the workcenter.
We also have a message broker on the edge device that can pick up messages from the MachineInterface and expose to any interested applications. 
In this case the MachineMonitoring which subscribes to DeviceStateChanges, and save these to the database

With more time i would have split the changing of states into its own api, and have added a web front end with two user roles (supervior and user), and a mobile app for the user. Both the web front end and the mobile app would use the same backend services
