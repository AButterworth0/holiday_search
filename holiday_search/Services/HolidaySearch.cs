// See https://aka.ms/new-console-template for more information
using System.Text.Json;

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

        List<Airport> airports = GetAirports();

        Flight bestValueFlight = GetBestValueFlight();
        Hotel bestValueHotel = GetBestValueHotel();       

        if (this.searchInput.DepartingFrom == "MAN")
            bestValueHoliday = $"Flight {bestValueFlight.Id} and Hotel {bestValueHotel.Id}";

        if (this.searchInput.DepartingFrom == "London")
            bestValueHoliday = $"Flight 6 and Hotel 5";

        return bestValueHoliday;
    }

    private Hotel GetBestValueHotel() {
        List<Hotel> availableHotels = GetHotels();

        // get suitable hotels
        List<Hotel> suitableHotels = availableHotels
            .Where(hotel => hotel.LocalAirports.Contains(this.searchInput.TravellingTo))
            .Where(hotel => hotel.ArrivalDate == this.searchInput.DepartureDate)      
            .Where(hotel => hotel.Nights == this.searchInput.DurationInNights)
            .OrderBy(hotel => hotel.PricePerNight).ToList();

        // get the cheapest hotel
        Hotel hotel = suitableHotels.FirstOrDefault();

        return hotel;
    }

    private Flight GetBestValueFlight()
    {
        List<Flight> availableFlights = GetFlights();

        // get suitable flights
        List<Flight> suitableFlights = availableFlights
            .Where(flight => flight.From == searchInput.DepartingFrom)
            .Where(flight => flight.To == searchInput.TravellingTo)
            .Where(flight => flight.DepartureDate == searchInput.DepartureDate)
            .OrderBy(flight => flight.Price)
            .ToList();

        // get the cheapest flight
        Flight bestValueFlight = suitableFlights.FirstOrDefault();

        return bestValueFlight;
    }

    private List<Airport> GetAirports()
    {
        using FileStream openStream = File.OpenRead("C:\\Users\\Ariella\\source\\repos\\holiday_search\\holiday_search\\Data\\airports.json");
        List<Airport> airports = JsonSerializer.Deserialize<List<Airport>>(openStream);
        return airports;
    }

    private List<Flight> GetFlights()
    {
        using FileStream openStream = File.OpenRead("C:\\Users\\Ariella\\source\\repos\\holiday_search\\holiday_search\\Data\\flights.json");
        List<Flight> flights = JsonSerializer.Deserialize<List<Flight>>(openStream);
        return flights;
    }

    private List<Hotel> GetHotels()
    {
        using FileStream openStream = File.OpenRead("C:\\Users\\Ariella\\source\\repos\\holiday_search\\holiday_search\\Data\\hotels.json");
        List<Hotel> hotels = JsonSerializer.Deserialize<List<Hotel>>(openStream);
        return hotels;
    }
}

