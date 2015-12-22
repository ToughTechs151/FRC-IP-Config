# FRC IP Configurator

Utility to configure the computer's IP settings to allow it to connect to a robot with a specific team number. Note that the same team number needs to also be set in the Driver Station.

## Usage:

The program will first prompt you to select the network device you are using to connect to the robot. This will typically be "Ethernet", "Wireless Card", or similar depending on the computer.



Then it will ask you to choose one of 3 IP settings:

1. DHCP
2. Wired (IP will end in .5)
3. Wireless (IP will end in .9)

If options 2 or 3 are selected, it will ask for the robot's team number, and finally it will set up the computer's IP.
