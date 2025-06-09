# TWA - True Weather Application

**TWA (True Weather Application)** is a .NET console application that provides users with real-time weather information. This project serves as a foundation for a full featured weather app that will be accessible on all mobile devices and as a desktop application.

## Features

- Retrieve and display:
  - Current temperature in Celsius and Fahrenheit
  - Region and city name
  - Current date
  - Minimum, average, and maximum temperatures
- Display basic weather details in a readable format
- Support for multiple cities and forecast ranges (1–14 days)
- Clean and simple console interface

## Planned Enhancements

Future development goals include:

- Hourly weather forecast display
- Humidity levels
- Wind speed (mph)
- Chance of precipitation
- “Feels like” temperature
- Sleek, modern UI with desktop and mobile support

## Usage

1. Clone or download the repository.
2. Open the project in Visual Studio or any .NET-compatible IDE.
3. Run the application.
4. Follow the console prompts to:
   - Enter a city name.
   - Specify the number of forecast days (1–14).
   - View current weather and forecast data.

## Sample Flow

- User launches the app.
- Enters a city name (e.g., `new york`).
- Enters the number of forecast days (e.g., `5`).
- The app displays:
  - Current temperature
  - Regional information
  - A 5-day forecast with daily max/min temperatures

## Prerequisites

- .NET 6 SDK or higher
- Internet connection for API access
- [WeatherAPI.com](https://www.weatherapi.com/) API key

## Configuration

Replace the placeholder API key in `Program.cs` with your actual WeatherAPI key:

```csharp
var apiKey = "YOUR_API_KEY_HERE";
