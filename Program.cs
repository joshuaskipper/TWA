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

                        Console.Clear();
                        Console.WriteLine(date);
                        Console.WriteLine($"Searched City: {name}");
                        Console.WriteLine($"Region: {region}");
                        Console.WriteLine($"Current weather in Celsius: {celcius}");
                        Console.WriteLine($"Current weather in Fahrenheit: {farenheit}\n");
                        foreach (var f in forecast.EnumerateArray())
                        {
                            var forcastdate = f.GetProperty("date");
                            DateTime parsedDate = DateTime.Parse(forcastdate.ToString());
                            string longDate = parsedDate.ToString("dddd, MMM dd");
                            var days = f.GetProperty("day");
                            var condition = days.GetProperty("condition").GetProperty("text");
                            Console.WriteLine($"--------{longDate}--------");
                            Console.WriteLine($"Min Temp in Fahrenheit: {days.GetProperty("mintemp_f")}℉");
                            Console.WriteLine($"Avg Temp in Fahrenheit: {days.GetProperty("avgtemp_f")}℉");
                            Console.WriteLine($"Max Temp in Fahrenheit: {days.GetProperty("maxtemp_f")}℉");
                            Console.WriteLine($"Weather Conditions: {condition}\n");

                            /*
                                Adding these fields later
                                humidity
                                feels like
                                wind mph
                                chance of rain
                                hour by hour
                             */
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
