namespace DesignCrowdJaimieCarter.src.Functionality
{
    public class BusinessDayCounter
    {
        readonly PublicHolidays _publicHolidays;

        public BusinessDayCounter()
        {
            _publicHolidays = new PublicHolidays();
        }
        
        static List<DayOfWeek> weekend = new List<DayOfWeek>(
            new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday });

        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            if (firstDate > secondDate)
                return 0;

            TimeSpan timeSpan = secondDate - firstDate;
            int totalDaysCount = timeSpan.Days + 1;

            int weekendDaysCount = Enumerable.Range(0, totalDaysCount)
              .Select(d => firstDate.AddDays(d))
              .Count(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday);

            totalDaysCount += DateFallsOnWeekDay(firstDate);
            totalDaysCount += DateFallsOnWeekDay(secondDate);

            return totalDaysCount - weekendDaysCount;
        }
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            TimeSpan timeSpan = secondDate - firstDate;
            int totalDaysCount = timeSpan.Days + 1;
      
            int weekendDaysCount = Enumerable.Range(0, totalDaysCount)
                .Select(d => firstDate.AddDays(d))
                .Count(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday);

            int publicHolidaysCount = Enumerable.Range(0, totalDaysCount)
                .Select(d => firstDate.AddDays(d))
                .Count(d => publicHolidays.Contains(d));

            totalDaysCount += DateFallsOnWeekDay(firstDate);
            totalDaysCount += DateFallsOnWeekDay(secondDate);

            return totalDaysCount - weekendDaysCount - publicHolidaysCount;
        }

        private int DateFallsOnWeekDay(DateTime date) // does what it says
        {
            if (!weekend.Contains(date.DayOfWeek))
                return -1;
            else
                return 0;
        }

        public string CreateDateString(DateTime date) // Creates a string like 7th October 2023
        {

            if (date.Day == 1 || date.Day == 21 || date.Day == 31)
            {
                return date.ToString("d\"st\" MMMM yyyy");
            }
            else if (date.Day == 2 || date.Day == 22)
            {
                return  date.ToString("d\"nd\" MMMM yyyy");
            }
            else if (date.Day == 3 || date.Day == 23)
            {
                return date.ToString("d\"rd\" MMMM yyyy");
            }
            else
            {
                return date.ToString("d\"th\" MMMM yyyy");
            }
        }

        public List<Views.TotalWeekdays> TotalWeekdaysViewList(List<Entities.TotalWeekdays> totalWeekdaysInput) // Create the list of View objects to be displayed
        {
            List<Views.TotalWeekdays> totalWeekdaysView = new List<Views.TotalWeekdays>();

            foreach (var weekday in totalWeekdaysInput)
            {
                var businessdayCounter = new BusinessDayCounter();
                var weekdaysView = new Views.TotalWeekdays()
                {
                    FirstDate = businessdayCounter.CreateDateString(weekday.FirstDate),
                    SecondDate = businessdayCounter.CreateDateString(weekday.SecondDate),
                    TotalDays = businessdayCounter.WeekdaysBetweenTwoDates(weekday.FirstDate, weekday.SecondDate)
                };
                totalWeekdaysView.Add(weekdaysView);
            }
            return totalWeekdaysView;
        }

        public List<Views.TotalBusinessDays> TotalBusinessDaysViewList(List<Entities.TotalBusinessDays> totalBusinessDaysInput) // Create the list of View objects to be displayed
        {
            List<Views.TotalBusinessDays> totalBusinessDaysView = new List<Views.TotalBusinessDays>();
            List<DateTime> holidays = new List<DateTime>();

            foreach (var businessDay in totalBusinessDaysInput)
            {
                var firstDateYear = businessDay.FirstDate.Year;
                var secondDateYear = businessDay.SecondDate.Year;
                holidays.AddRange(_publicHolidays.AllPublicHolidays(firstDateYear));
                holidays.AddRange(_publicHolidays.AllPublicHolidays(secondDateYear));

                var businessDayCounter = new BusinessDayCounter();
                var businessDayView = new Views.TotalBusinessDays()
                {
                    FirstDate = businessDayCounter.CreateDateString(businessDay.FirstDate),
                    SecondDate = businessDayCounter.CreateDateString(businessDay.SecondDate),
                    TotalDays = businessDayCounter.BusinessDaysBetweenTwoDates(businessDay.FirstDate, businessDay.SecondDate, holidays)
                };
                totalBusinessDaysView.Add(businessDayView);
            }
            return totalBusinessDaysView;
        }

    }
}
