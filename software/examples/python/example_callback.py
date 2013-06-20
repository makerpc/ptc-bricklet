#!/usr/bin/env python
# -*- coding: utf-8 -*-  

HOST = "localhost"
PORT = 4223
UID = "XYZ" # Change to your UID

from tinkerforge.ip_connection import IPConnection
from tinkerforge.bricklet_ptc import PTC

# Callback function for temperature callback (parameter has unit °C/100)
def cb_temperature(temperature):
    print('Temperature: ' + str(temperature/100.0) + ' °C')

if __name__ == "__main__":
    ipcon = IPConnection() # Create IP connection
    ptc = PTC(UID, ipcon) # Create device object

    ipcon.connect(HOST, PORT) # Connect to brickd
    # Don't use device before ipcon is connected

    # Set Period for temperature callback to 1s (1000ms)
    # Note: The callback is only called every second if the 
    #       temperature has changed since the last call!
    ptc.set_temperature_callback_period(1000)

    # Register temperature callback to function cb_temperature
    ptc.register_callback(ptc.CALLBACK_TEMPERATURE, cb_temperature)

    raw_input('Press key to exit\n') # Use input() in Python 3
    ipcon.disconnect()
