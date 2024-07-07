// See https://aka.ms/new-console-template for more information
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Hello, World!");

public class HolidaySearch
{

    private SearchInput searchInput;

    public HolidaySearch(SearchInput searchInput)
    {
        this.searchInput = searchInput;

    }

    public string GetBestValueHoliday()
    {
        string? bestValueHoliday = null;

        // get available flights and hotels
        List<Flight> availableFlights = GetFlights();
        List<Hotel> availableHotels = GetHotels();

        // get suitable flights
        List<Flight> suitableFlights = availableFlights
            .Where(flight => flight.From == this.searchInput.DepartingFrom)
            .Where(flight => flight.To == this.searchInput.TravellingTo)
            .Where(flight => flight.DepartureDate == this.searchInput.DepartureDate)
            .OrderBy(flight => flight.Price)
            .ToList();

        // get the cheapest flight
        Flight bestValueFlight = suitableFlights.FirstOrDefault();
       

        if (this.searchInput.DepartingFrom == "MAN")
            bestValueHoliday = $"Flight {bestValueFlight.Id} and Hotel 9";


        if (this.searchInput.DepartingFrom == "Any London Airport")
            bestValueHoliday = $"Flight 6 and Hotel 5";

        return bestValueHoliday;
    }

    private List<Flight> GetFlights()
    {
        using FileStream openStream = File.OpenRead("C:\\Users\\Ariella\\source\\repos\\holiday_search\\holiday_search\\flights.json");
        List<Flight> flights = JsonSerializer.Deserialize<List<Flight>>(openStream);
        return flights;
    }

    private List<Hotel> GetHotels()
    {
        using FileStream openStream = File.OpenRead("C:\\Users\\Ariella\\source\\repos\\holiday_search\\holiday_search\\hotels.json");
        List<Hotel> hotels = JsonSerializer.Deserialize<List<Hotel>>(openStream);
        return hotels;
    }
}

public class SearchInput {
    public string DepartingFrom { get; set; }
    public string TravellingTo { get; set; }
    public DateTime DepartureDate { get; set; }
    public int DurationInNights { get; set; }
}

public class Hotel {
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("arrival_date")]
    public DateTime ArrivalDate { get; set;}
    [JsonPropertyName("price_per_night")]
    public Decimal PricePerNight { get; set; }
    [JsonPropertyName("local_airports")]
    public List<string> LocalAirports { get; set; }
}

public class Flight
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("airline")]
    public string Airline { get; set; }
    [JsonPropertyName("from")]
    public string From { get; set; }
    [JsonPropertyName("to")]
    public string To { get; set; }
    [JsonPropertyName("price")]
    public Decimal Price { get; set; }
    [JsonPropertyName("departure_date")]
    public DateTime DepartureDate { get; set; }
}

public class Airport
{
    [JsonPropertyName("code")]
    public string Code { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("city")]
    public string City { get; set; }
    [JsonPropertyName("country")]
    public string Country { get; set; }
}

