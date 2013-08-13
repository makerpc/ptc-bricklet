using Tinkerforge;

class Example
{
	private static string HOST = "localhost";
	private static int PORT = 4223;
	private static string UID = "XYZ"; // Change to your UID

	// Callback function for temperature callback (parameter has unit °C/100)
	static void TemperatureCB(BrickletPTC sender, int temperature)
	{
		System.Console.WriteLine("Temperature: " + temperature/100.0 + " °C");
	}

	static void Main() 
	{
		IPConnection ipcon = new IPConnection(); // Create IP connection
		BrickletPTC ptc = new BrickletPTC(UID, ipcon); // Create device object

		ipcon.Connect(HOST, PORT); // Connect to brickd
		// Don't use device before ipcon is connected

		// Set Period for temperature callback to 1s (1000ms)
		// Note: The temperature callback is only called every second if the 
		//       temperature has changed since the last call!
		ptc.SetTemperatureCallbackPeriod(1000);

		// Register temperature callback to function TemperatureCB
		ptc.Temperature += TemperatureCB;

		System.Console.WriteLine("Press key to exit");
		System.Console.ReadKey();
		ipcon.Disconnect();
	}
}