using System;
using System.IO.Ports;
using System.Text.Json;
using System.Collections.Generic;

namespace ComWeather
{
	public class WeatherSensorHandler
	{
		private SerialPort serialPort;

		public event EventHandler<WeatherDataEventArgs> DataReceived;

		public void Connect(string portName, int baudRate)
		{
			if (serialPort != null && serialPort.IsOpen)
			{
				throw new InvalidOperationException("Already connected.");
			}

			serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
			serialPort.DataReceived += SerialPortDataReceived;
			serialPort.Open();
		}

		public void Disconnect()
		{
			if (serialPort != null && serialPort.IsOpen)
			{
				serialPort.Close();
				serialPort.Dispose();
				serialPort = null;
			}
		}

		private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			string data = serialPort.ReadExisting();
			ProcessData(data);
		}

		private void ProcessData(string data)
		{
			string[] messages = data.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var message in messages)
			{
				if (message.StartsWith("$"))
				{
					var parts = message.Substring(1).Split(',');
					if (parts.Length == 2 && double.TryParse(parts[0], out double windSpeed) && double.TryParse(parts[1], out double windDirection))
					{
						var record = new WeatherDataEventArgs
						{
							Time = DateTime.UtcNow,
							RawData = message,
							WindSpeed = windSpeed,
							WindDirection = windDirection
						};

						SaveToJson(record);
						DataReceived?.Invoke(this, record);
					}
				}
			}
		}

		private void SaveToJson(WeatherDataEventArgs data)
		{
			string filePath = "wind_data.json";
			List<WeatherDataEventArgs> records;

			if (System.IO.File.Exists(filePath))
			{
				string existingJson = System.IO.File.ReadAllText(filePath);
				records = JsonSerializer.Deserialize<List<WeatherDataEventArgs>>(existingJson) ?? new List<WeatherDataEventArgs>();
			}
			else
			{
				records = new List<WeatherDataEventArgs>();
			}

			records.Add(data);
			System.IO.File.WriteAllText(filePath, JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true }));
		}
	}

	public class WeatherDataEventArgs : EventArgs
	{
		public DateTime Time { get; set; }
		public string RawData { get; set; }
		public double WindSpeed { get; set; }
		public double WindDirection { get; set; }
	}
}
