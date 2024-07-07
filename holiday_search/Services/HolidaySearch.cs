﻿// See https://aka.ms/new-console-template for more information
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

        Flight bestValueFlight = null;

        List<Airport> airports = GetAirports();
        bool isCode = false;
        bool isCity = false;

        List<string> airportCodes = airports.Select(airport => airport.Code).ToList();
        List<string> airportCities = airports.Select(airport => airport.City).ToList();

        if (airportCodes.Contains(searchInput.DepartingFrom))
            isCode = true;

        if (airportCities.Contains(searchInput.DepartingFrom))
            isCity = true;

        if (isCode)
            bestValueFlight = GetBestValueFlight(searchInput.DepartingFrom);

        if(isCity)
        {
            bestValueFlight = GetBestValueFlightFromCity(searchInput.DepartingFrom, airports);
        }

        Hotel bestValueHotel = GetBestValueHotel();

        bestValueHoliday = $"Flight {bestValueFlight.Id} and Hotel {bestValueHotel.Id}";


        return bestValueHoliday;
    }

    private Flight GetBestValueFlightFromCity(string City, List<Airport> airports)
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

    private Airport GetDepartureAirport()
    {
        List<Airport> airports = GetAirports();
        Airport departureAirport = airports.Where(airport => airport.Code == this.searchInput.DepartingFrom || airport.City == this.searchInput.DepartingFrom).FirstOrDefault();
        return departureAirport;
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

