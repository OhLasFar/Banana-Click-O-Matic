using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WindowsInput;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace Banana_Click_O_Matic
{
    public partial class MainWindow : Window
    {
        private static readonly TraceSource Logger = new TraceSource("AutoClicker");
        private System.Threading.Timer _timer;
        private InputSimulator _inputSimulator;
        private IKeyboardMouseEvents _globalHook;
        private int _interval;
        private Stopwatch _stopwatch;
        private bool _ignoreNextClick;
        private CancellationTokenSource _cts;

        public MainWindow()
        {
            InitializeComponent();
            _inputSimulator = new InputSimulator();
            _stopwatch = new Stopwatch();
            ConfigureLogging();
            ConfigureGlobalHook();
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

        private void ConfigureGlobalHook()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.MouseDownExt += GlobalHookMouseDownExt;
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            if (_ignoreNextClick)
            {
                _ignoreNextClick = false;
                return;
            }

            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                StopAutoClicker();
                Logger.TraceInformation($"Auto-clicker stopped due to {e.Button} mouse button click.");
            }
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(ClicksPerSecondTextBox.Text, out int clicksPerSecond) && clicksPerSecond > 0)
                {
                    _interval = 1000 / clicksPerSecond;
                    StartButton.IsEnabled = false;
                    StatusLabel.Content = "Status: Clicking";
                    Logger.TraceInformation($"Started clicking at {clicksPerSecond} clicks per second.");
                    _cts = new CancellationTokenSource();
                    _stopwatch.Start();
                    await StartAutoClicker(_cts.Token);
                }
                else
                {
                    System.Windows.MessageBox.Show("Please enter a valid number of clicks per second.");
                    Logger.TraceEvent(TraceEventType.Warning, 0, "Invalid clicks per second input.");
                }
            }
            catch (Exception ex)
            {
                Logger.TraceEvent(TraceEventType.Error, 0, $"Error starting the clicker: {ex.Message}");
            }
        }

        private async Task StartAutoClicker(CancellationToken token)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (_timer = new System.Threading.Timer(TimerCallback, null, 0, _interval))
                    {
                        token.WaitHandle.WaitOne();
                    }
                }
                catch (OperationCanceledException)
                {
                    Logger.TraceInformation("Auto-clicker operation cancelled.");
                }
            });
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopAutoClicker();
        }

        private void StopAutoClicker()
        {
            try
            {
                _cts?.Cancel();
                _timer?.Change(Timeout.Infinite, Timeout.Infinite);
                StartButton.IsEnabled = true;
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
                _ignoreNextClick = true;
                _inputSimulator.Mouse.LeftButtonClick();
                Logger.TraceEvent(TraceEventType.Verbose, 0, $"Mouse click performed at {_stopwatch.ElapsedMilliseconds} ms");
                _stopwatch.Restart();
            }
            catch (Exception ex)
            {
                Logger.TraceEvent(TraceEventType.Error, 0, $"Error during mouse click: {ex.Message}");
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _globalHook.MouseDownExt -= GlobalHookMouseDownExt;
            _globalHook.Dispose();
            base.OnClosed(e);
        }
    }
}
