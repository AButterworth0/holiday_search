// See https://aka.ms/new-console-template for more information
using System.Text.Json;

public class HolidaySearch
{
    private SearchInput searchInput;
    private List<Airport> airports;
    public List<Holiday> Results { get; set; }

    public HolidaySearch(SearchInput searchInput)
    {
        this.searchInput = searchInput;
        this.airports = this.GetAirports();
    }

    public string GetBestValueHoliday()
    {
        Flight bestValueFlight = null;

        if (IsAirportCode(searchInput.DepartingFrom))
            bestValueFlight = GetBestValueFlight(searchInput.DepartingFrom);
        else if (IsCity(searchInput.DepartingFrom))
            bestValueFlight = GetBestValueFlightFromCity(searchInput.DepartingFrom);
        else
            bestValueFlight = GetBestValueFlightFromAnyAirport();

        Hotel bestValueHotel = GetBestValueHotel();

        string bestValueHoliday = $"Flight {bestValueFlight.Id} and Hotel {bestValueHotel.Id}";

        return bestValueHoliday;
    }

    private Flight GetBestValueFlight(string airportCode)
    {
        List<Flight> availableFlights = GetFlights();

        // get suitable flights
        List<Flight> suitableFlights = availableFlights
            .Where(flight => flight.From == airportCode)
            .Where(flight => flight.To == searchInput.TravellingTo)
            .Where(flight => flight.DepartureDate == searchInput.DepartureDate)
            .OrderBy(flight => flight.Price)
            .ToList();

        // get the cheapest flight
        Flight bestValueFlight = suitableFlights.FirstOrDefault();

        return bestValueFlight;
    }

    private Flight GetBestValueFlightFromCity(string City)
    {
        List<Airport> airportsFromCity = airports.Where(airport => airport.City == searchInput.DepartingFrom).ToList();
        List<Flight> availableFlights = new List<Flight>();
        foreach (Airport airport in airportsFromCity)
        {
            Flight flight = GetBestValueFlight(airport.Code);
            availableFlights.Add(flight);
        }
        Flight bestValueFlight = availableFlights.OrderBy(flight => flight.Price).First();
        return bestValueFlight;
    }

    private Flight GetBestValueFlightFromAnyAirport()
    {
        List<Flight> availableFlights = GetFlights();

        // get suitable flights
        List<Flight> suitableFlights = availableFlights
            .Where(flight => flight.To == searchInput.TravellingTo)
            .Where(flight => flight.DepartureDate == searchInput.DepartureDate)
            .OrderBy(flight => flight.Price)
            .ToList();

        // get the cheapest flight
        Flight bestValueFlight = suitableFlights.FirstOrDefault();

        return bestValueFlight;
    }

    private Airport GetDepartureAirport()
    {
        List<Airport> airports = GetAirports();
        Airport departureAirport = airports.Where(airport => airport.Code == this.searchInput.DepartingFrom || airport.City == this.searchInput.DepartingFrom).FirstOrDefault();
        return departureAirport;
    }

    private Hotel GetBestValueHotel()
    {
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

    private bool IsCity(string departingFrom)
    {
        List<string> airportCities = airports.Select(airport => airport.City).ToList();
        return airportCities.Contains(departingFrom);
    }

    private bool IsAirportCode(string departingFrom)
    {
        List<string> airportCodes = airports.Select(airport => airport.Code).ToList();
        return airportCodes.Contains(departingFrom);
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