using System;
using System.Text.Json;

namespace TWA
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var date = DateTime.Today.ToString("D");
            Console.WriteLine($@"{date}

Welcome to Weather.io!!!
Start by typing a city, e.g., 'London'.
Then give us the forecast range, e.g., 6

");

            int dayCount;

            Console.WriteLine("Enter a city to get the weather!");
            var city = Console.ReadLine().Trim().ToLower();
            Console.WriteLine("How many days would you like to see? Range: 1–14.");
            while (!int.TryParse(Console.ReadLine(), out dayCount))
            {
                Console.WriteLine("Invalid input, please enter a numeric value. Try '2'.");
            }

            var apiKey = "YOUR_API_KEY_HERE";
            var url = $"http://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={city}&days={dayCount}&aqi=no&alerts=no";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine(data);

                        JsonDocument doc = JsonDocument.Parse(data);

                        // top level of JSON
                        var locationTOP = doc.RootElement.GetProperty("location");
                        var currentTOP = doc.RootElement.GetProperty("current");
                        var forecast = doc.RootElement.GetProperty("forecast").GetProperty("forecastday");

                        // specific location field data
                        var name = locationTOP.GetProperty("name");
                        var region = locationTOP.GetProperty("region");

                        // specific current weather field data
                        var celcius = currentTOP.GetProperty("temp_c");
                        var farenheit = currentTOP.GetProperty("temp_f");


                        Console.WriteLine("Do you want the temperature to be in a (Farenheit/Celcius) type 'f' or 'c'");
                        var conversion = Console.ReadLine().Trim().ToLower();

                        var windMPH = currentTOP.GetProperty("wind_mph");
                        var humidity = currentTOP.GetProperty("humidity");
                        var feelsLikeC = currentTOP.GetProperty("feelslike_c");
                        var feelsLikeF = currentTOP.GetProperty("feelslike_f");





                        if (conversion == "f")
                        {

                            Console.Clear();
                            Console.WriteLine(date);
                            Console.WriteLine($"Searched City: {name}");
                            Console.WriteLine($"Region: {region}");
                            //Console.WriteLine($"Current weather in Celsius: {celcius}");
                            Console.WriteLine($"Current weather in Fahrenheit: {farenheit}F");
                            Console.WriteLine($"Current Humidity: {humidity}");
                            Console.WriteLine($"Current Wind MPH: {windMPH}");
                            Console.WriteLine($"Feels Like: {feelsLikeF}F\n");

                            foreach (var f in forecast.EnumerateArray())
                            {
                                var forcastdate = f.GetProperty("date");
                                DateTime parsedDate = DateTime.Parse(forcastdate.ToString());
                                string longDate = parsedDate.ToString("dddd, MMM dd");
                                var days = f.GetProperty("day");
                                var condition = days.GetProperty("condition").GetProperty("text");
                                Console.WriteLine($"--------{longDate}--------");
                                Console.WriteLine($"Low: {days.GetProperty("mintemp_f")}°F");
                                Console.WriteLine($"Avg: {days.GetProperty("avgtemp_f")}°F");
                                Console.WriteLine($"High: {days.GetProperty("maxtemp_f")}°F");
                                Console.WriteLine($"Daily Chance of rain: {days.GetProperty("daily_chance_of_rain")}%");
                                Console.WriteLine($"Weather Conditions: {condition}\n");



                                /*
                                    humidity
                                    feels like
                                    wind mph
                                    chance of rain
                                    hour by hour
                                 */
                            }

                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(date);
                            Console.WriteLine($"Searched City: {name}");
                            Console.WriteLine($"Region: {region}");
                            Console.WriteLine($"Current weather in Celsius: {celcius}°C");
                            Console.WriteLine($"Current Humidity: {humidity}");
                            Console.WriteLine($"Current Wind MPH: {windMPH}");
                            Console.WriteLine($"Feels Like: {feelsLikeC}C\n");
                            
                            //Console.WriteLine($"Current weather in Fahrenheit: {farenheit}\n");
                            foreach (var f in forecast.EnumerateArray())
                            {
                                var forcastdate = f.GetProperty("date");
                                DateTime parsedDate = DateTime.Parse(forcastdate.ToString());
                                string longDate = parsedDate.ToString("dddd, MMM dd");
                                var days = f.GetProperty("day");
                                var condition = days.GetProperty("condition").GetProperty("text");
                                Console.WriteLine($"--------{longDate}--------");
                                Console.WriteLine($"Low: {days.GetProperty("mintemp_c")}°C");
                                Console.WriteLine($"Avg: {days.GetProperty("avgtemp_c")}°C");
                                Console.WriteLine($"High: {days.GetProperty("maxtemp_c")}°C");
                                Console.WriteLine($"Daily Chance of rain: {days.GetProperty("daily_chance_of_rain")}%");
                                Console.WriteLine($"Weather Conditions: {condition}\n");
                                //Console.WriteLine($"Min Temp: {minTemp}°{unitSymbol}");


                                /*
                                    humidity
                                    feels like
                                    wind mph
                                    chance of rain
                                    hour by hour
                                 */
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Message: {ex.Message}");
                }
            }
        }
    }
}
