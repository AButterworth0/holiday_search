using static System.Runtime.InteropServices.JavaScript.JSType;

namespace holiday_search.tests
{
    public class HolidaySearchTests
    {
        //Customer 1
        //Inputs:
        //  Departing from: Manchester Airport(MAN)
        //  Traveling to: Malaga Airport(AGP)
        //  Departure Date: 2023/07/01 
        //  Duration: 7 nights
        //Expected:
        //  Flight 2 and Hotel 9
        [Fact]
        public void GivenExactDepartingAirportReturnBestValueHoliday()
        {
            //arrange
            string expected = "Flight 2 and Hotel 9";
            SearchInput searchInput = new SearchInput() { DepartingFrom = "MAN", TravellingTo = "AGP", DepartureDate = new DateTime(2023, 07, 01), DurationInNights = 7 };
            HolidaySearch holidaySearch = new HolidaySearch(searchInput);

            //act
            string actual = holidaySearch.GetBestValueHoliday();

            //assert
            Assert.Equal(expected, actual);
        }

        //Customer 2
        //Inputs:
        //  Departing from: Any London Airport
        //  Traveling to: Mallorca Airport (PMI)
        //  Departure Date: 2023/06/15
        //  Duration: 10 nights
        //Expected:
        //  Flight 6 and Hotel 5 

        //Customer 3
        //Inputs:
        //  Departing from: Any Airport 
        //  Traveling to: Gran Canaria Airport(LPA)
        //  Departure Date: 2022/11/10
        //  Duration: 14 nights
        // Expected:
        //  Flight 7 and Hotel 6 
    }
}