using System;
using System.Threading;
using System.Windows;
using WindowsInput;

namespace Banana_Click_O_Matic
{
    public partial class MainWindow : Window
    {
        private Timer _timer;
        private InputSimulator _inputSimulator;
        private int _interval;

        public MainWindow()
        {
            InitializeComponent();
            _inputSimulator = new InputSimulator();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ClicksPerSecondTextBox.Text, out int clicksPerSecond) && clicksPerSecond > 0)
            {
                _interval = 1000 / clicksPerSecond;
                _timer = new Timer(TimerCallback, null, 0, _interval);
                StatusLabel.Content = "Status: Clicking";
            }
            else
            {
                MessageBox.Show("Please enter a valid number of clicks per second.");
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            StatusLabel.Content = "Status: Stopped";
        }

        private void TimerCallback(object state)
        {
            _inputSimulator.Mouse.LeftButtonClick();
        }
    }
}
