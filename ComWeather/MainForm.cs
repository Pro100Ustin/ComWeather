using System;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ComWeather
{
    public partial class MainForm : Form
    {
        private SerialPort serialPort;
        private StringBuilder dataBuffer = new StringBuilder();
        private readonly Regex messageRegex = new Regex(@"\$(?<speed>[0-9]+\.[0-9]+),(?<direction>[0-9]+\.[0-9]+)\r\n");

        private float? previousWindSpeed = null;
        private float? previousWindDirection = null;
        private string fileName = "weather_data.txt";

        public MainForm()
        {
            InitializeComponent();
            btnDisconnect.Enabled = false; // Изначально кнопка "Отключиться" неактивна
            LoadFileData(); // Загружаем данные из файла при старте программы
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string portName = txtPortName.Text;
                int baudRate = int.Parse(txtBaudRate.Text);
                int dataBits = 8;
                StopBits stopBits = StopBits.One;
                Parity parity = Parity.None;

                serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits)
                {
                    Encoding = Encoding.ASCII,
                    ReadTimeout = 1000
                };

                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.Open();

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;

                Log($"Подключено к порту {portName}. Скорость обмена: {baudRate} бод.");
            }
            catch (Exception ex)
            {
                Log($"Ошибка подключения: {ex.Message}");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                    serialPort.Dispose();
                    serialPort = null;

                    btnConnect.Enabled = true;
                    btnDisconnect.Enabled = false;

                    Log("Соединение с портом закрыто.");
                }
            }
            catch (Exception ex)
            {
                Log($"Ошибка при отключении: {ex.Message}");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort.ReadExisting(); // Читаем все доступные данные
                Invoke((Action)(() => ProcessData(data))); // Обработка данных в главном потоке
            }
            catch (TimeoutException)
            {
                Log("Таймаут при чтении данных.");
            }
            catch (Exception ex)
            {
                Log($"Ошибка чтения: {ex.Message}");
            }
        }

        private void ProcessData(string data)
        {
            dataBuffer.Append(data);
            string bufferString = dataBuffer.ToString();

            var matches = messageRegex.Matches(bufferString);

            foreach (Match match in matches)
            {
                try
                {
                    string speed = match.Groups["speed"].Value;
                    string direction = match.Groups["direction"].Value;

                    float windSpeed = float.Parse(speed, CultureInfo.InvariantCulture);
                    float windDirection = float.Parse(direction, CultureInfo.InvariantCulture);

                    // Добавляем в лог необработанные данные
                    Log($"${speed},{direction}");

                    // Проверяем, изменились ли данные
                    if (windSpeed != previousWindSpeed || windDirection != previousWindDirection)
                    {
                        previousWindSpeed = windSpeed;
                        previousWindDirection = windDirection;

                        string portName = serialPort?.PortName ?? "UnknownPort";
                        string logEntry = $"{portName} {DateTime.Now:dd.MM.yyyy HH:mm:ss}: ${speed},{direction}";

                        AppendToFile(fileName, logEntry); // Записываем данные в файл
                        LoadFileData(); // Обновляем данные в поле для парсинга
                    }
                }
                catch (FormatException ex)
                {
                    Log($"Ошибка парсинга данных: {ex.Message}");
                }
            }

            if (matches.Count > 0)
            {
                dataBuffer.Clear();
            }
        }

        private void AppendToFile(string fileName, string content)
        {
            try
            {
                File.AppendAllText(fileName, content + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Log($"Ошибка записи в файл: {ex.Message}");
            }
        }

        private void LoadFileData()
        {
            try
            {
                // Создаем файл, если его нет
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Close();
                }

                // Загружаем данные из файла и отображаем их в поле для парсинга
                string fileContent = File.ReadAllText(fileName);
                txtParsedData.Text = fileContent;
            }
            catch (Exception ex)
            {
                Log($"Ошибка при загрузке файла: {ex.Message}");
            }
        }

        private void Log(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => txtLog.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}")));
            }
            else
            {
                txtLog.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
            }
        }
    }
}
