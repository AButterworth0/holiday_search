using static System.Runtime.InteropServices.JavaScript.JSType;

namespace holiday_search.tests
{
    public class HolidaySearchTests
    {

        [Theory]
        [InlineData("Flight 2 and Hotel 9", "MAN", "AGP", "2023/07/01", 7 )]
        public void GivenExactDepartingAirportReturnBestValueHoliday(string expected, string departingFrom, string travellingTo, DateTime departureDate, int durationInNights)
        {
            //arrange
            SearchInput searchInput = new SearchInput() { DepartingFrom = departingFrom, TravellingTo = travellingTo, DepartureDate = departureDate, DurationInNights = durationInNights };
            HolidaySearch holidaySearch = new HolidaySearch(searchInput);

            //act
            string actual = holidaySearch.GetBestValueHoliday();

            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Flight 6 and Hotel 5", "London", "PMI", "2023/06/15", 10)]
        public void GivenCityOfDepartingAirportReturnBestValueHoliday(string expected, string departingFrom, string travellingTo, DateTime departureDate, int durationInNights)
        {
            //arrange
            SearchInput searchInput = new SearchInput() { DepartingFrom = departingFrom, TravellingTo = travellingTo, DepartureDate = departureDate, DurationInNights = durationInNights };
            HolidaySearch holidaySearch = new HolidaySearch(searchInput);

            //act
            string actual = holidaySearch.GetBestValueHoliday();

            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Flight 7 and Hotel 6", "Any", "LPA", "2022/11/10", 14)]
        public void GivenAnyDepartingAirportReturnBestValueHoliday(string expected, string departingFrom, string travellingTo, DateTime departureDate, int durationInNights)
        {
            //arrange
            SearchInput searchInput = new SearchInput() { DepartingFrom = departingFrom, TravellingTo = travellingTo, DepartureDate = departureDate, DurationInNights = durationInNights };
            HolidaySearch holidaySearch = new HolidaySearch(searchInput);

            //act
            string actual = holidaySearch.GetBestValueHoliday();

            //assert
            Assert.Equal(expected, actual);
        }

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