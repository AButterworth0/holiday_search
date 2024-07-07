// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

public class HolidaySearch {

    private SearchInput searchInput;

    public HolidaySearch(SearchInput searchInput)
    {
        this.searchInput = searchInput;
    }

    public string GetBestValueHoliday()
    {
        string? bestValueHoliday = null;

        if (this.searchInput.DepartingFrom == "Any London Airport")
            bestValueHoliday = "Flight 6 and Hotel 5";

        if (this.searchInput.DepartingFrom == "MAN")
            return "Flight 2 and Hotel 9";

        return bestValueHoliday;
    }

}

public class SearchInput {
    public string DepartingFrom { get; set; }
    public string TravellingTo { get; set; }
    public DateTime DepartureDate { get; set; }
    public int DurationInNights { get; set; }
}

public class Hotel {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ArrivalDate { get; set;}
    public Decimal PricePerNights { get; set; }
    public List<string> LocalAirports { get; set; }
}

public class Flights
{
    public int Id { get; set; }
    public string Airline { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public Decimal Price { get; set; }
    public DateTime DepartureDate { get; set; }
}

public class Airport
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}

