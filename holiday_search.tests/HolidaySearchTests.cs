using static System.Runtime.InteropServices.JavaScript.JSType;

namespace holiday_search.tests
{
    public class HolidaySearchTests
    {
        [Theory]
        [InlineData("Flight 2 and Hotel 9", "MAN", "AGP", "2023/07/01", 7)]
        [InlineData("Flight 6 and Hotel 5", "London", "PMI", "2023/06/15", 10)]
        [InlineData("Flight 7 and Hotel 6", "Any", "LPA", "2022/11/10", 14)]
        public void GivenSearchInputReturnBestValueHoliday(string expected, string departingFrom, string travellingTo, DateTime departureDate, int durationInNights)
        {
            //arrange
            SearchInput searchInput = new SearchInput() { DepartingFrom = departingFrom, TravellingTo = travellingTo, DepartureDate = departureDate, DurationInNights = durationInNights };
            HolidaySearch holidaySearch = new HolidaySearch(searchInput);

            //act
            string actual = holidaySearch.GetBestValueHoliday();

            //assert
            Assert.Equal(expected, actual);
        }
    }
}