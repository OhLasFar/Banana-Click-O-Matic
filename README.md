
# Banana Click-O-Matic

Banana Click-O-Matic is a high-performance auto clicker application designed for Windows. It allows users to automate mouse clicks at a specified rate, making it ideal for repetitive tasks that require rapid mouse clicks.

## Features

- **Custom Click Rate**: Users can specify the number of clicks per second (CPS), allowing for precise control over the click rate.
- **Start/Stop Control**: Simple start and stop buttons to control the auto clicker.
- **Status Display**: Visual indication of whether the auto clicker is active or stopped.
- **High Precision**: Utilizes high-resolution timers for consistent click rates, ensuring up to 20 clicks per second.

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 or later

### Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yourusername/Banana_Click_O_Matic.git
   cd Banana_Click_O_Matic
   ```

2. **Install Dependencies**:
   Open the project in Visual Studio and install the required NuGet packages:
   - `InputSimulatorPlus`

### Running the Application

1. **Open the Solution** in Visual Studio.
2. **Build the Project** to ensure all dependencies are correctly installed.
3. **Run the Application**.

### Usage

1. **Specify Click Rate**:
   - Enter the desired number of clicks per second (default is 20 CPS).
   
2. **Start Clicking**:
   - Click the "Start" button to begin the auto clicker.
   - The status label will update to "Status: Clicking".

3. **Stop Clicking**:
   - Click the "Stop" button to stop the auto clicker.
   - The status label will update to "Status: Stopped".

## Project Structure

- **MainWindow.xaml**: Defines the user interface.
- **MainWindow.xaml.cs**: Contains the logic for starting and stopping the auto clicker, as well as handling the timer events.

## Technologies Used

- **.NET 8.0**
- **WPF (Windows Presentation Foundation)**
- **InputSimulatorPlus**: A library for simulating keyboard and mouse input.

## Contributing

Contributions are welcome! If you want to help with the project, feel free to do so. Proper credits will be given. Please fork the repository and create a pull request with your changes.

## Support

If you like this project and want to support its development, consider making a donation. Your support is greatly appreciated!

- Follow me on Twitter/X: [@OhLasFar](https://twitter.com/OhLasFar)
- Steam Trade Link: [Trade Link](https://steamcommunity.com/tradeoffer/new/?partner=325412381&token=Ictamh5K)
- Twitch: [LasFar](https://www.twitch.tv/LasFar)
- Donate crypto: `TDAsZuzqhS64kCqZwphmCQqW7MrMNdAGb6` (USDT Tron(TRC20))

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Thanks to the developers of [InputSimulatorPlus](https://www.nuget.org/packages/InputSimulatorPlus) for providing a robust input simulation library.
- Inspired by various open-source auto clicker projects available on GitHub.

---

**Note**: Things may change a bit, and this README will be updated accordingly.

Feel free to use and modify this application as needed. For any bugs or feature requests, please open an issue in the GitHub repository.
