using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using WindowsInput;

namespace Banana_Click_O_Matic
{
    public partial class MainWindow : Window
    {
        private static readonly TraceSource Logger = new TraceSource("AutoClicker");
        private Timer _timer;
        private InputSimulator _inputSimulator;
        private int _interval;
        private Stopwatch _stopwatch;

        public MainWindow()
        {
            InitializeComponent();
            _inputSimulator = new InputSimulator();
            _stopwatch = new Stopwatch();
            ConfigureLogging();
            Logger.TraceInformation("Application started.");
        }

        private void ConfigureLogging()
        {
            var logFile = "app.log";
            var fileListener = new TextWriterTraceListener(logFile)
            {
                TraceOutputOptions = TraceOptions.DateTime
            };
            Logger.Listeners.Add(fileListener);
            Logger.Switch = new SourceSwitch("SourceSwitch", "Verbose");
            Logger.TraceInformation("Logging configured.");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(ClicksPerSecondTextBox.Text, out int clicksPerSecond) && clicksPerSecond > 0)
                {
                    _interval = 1000 / clicksPerSecond;
                    _timer = new Timer(TimerCallback, null, 0, _interval);
                    StatusLabel.Content = "Status: Clicking";
                    Logger.TraceInformation($"Started clicking at {clicksPerSecond} clicks per second.");
                    _stopwatch.Start();
                }
                else
                {
                    MessageBox.Show("Please enter a valid number of clicks per second.");
                    Logger.TraceEvent(TraceEventType.Warning, 0, "Invalid clicks per second input.");
                }
            }
            catch (Exception ex)
            {
                Logger.TraceEvent(TraceEventType.Error, 0, $"Error starting the clicker: {ex.Message}");
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _timer?.Change(Timeout.Infinite, Timeout.Infinite);
                StatusLabel.Content = "Status: Stopped";
                Logger.TraceInformation("Stopped clicking.");
                _stopwatch.Stop();
                _stopwatch.Reset();
            }
            catch (Exception ex)
            {
                Logger.TraceEvent(TraceEventType.Error, 0, $"Error stopping the clicker: {ex.Message}");
            }
        }

        private void TimerCallback(object state)
        {
            try
            {
                _inputSimulator.Mouse.LeftButtonClick();
                Logger.TraceEvent(TraceEventType.Verbose, 0, $"Mouse click performed at {_stopwatch.ElapsedMilliseconds} ms");
                _stopwatch.Restart();
            }
            catch (Exception ex)
            {
                Logger.TraceEvent(TraceEventType.Error, 0, $"Error during mouse click: {ex.Message}");
            }
        }
    }
}
