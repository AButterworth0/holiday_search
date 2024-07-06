// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

public class HolidaySearch {

    public HolidaySearch(SearchInput searchInput)
    {

    }

    public string GetBestValueHoliday()
    {
        throw new NotImplementedException();
    }

}

public class SearchInput {
    public string DepartingFrom { get; set; }
    public string TravellingTo { get; set; }
    public DateTime DepartureDate { get; set; }
    public int DurationInNights { get; set; }
}
