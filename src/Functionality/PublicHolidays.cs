namespace DesignCrowdJaimieCarter.src.Functionality
{
    public class PublicHolidays
    {
        static List<DayOfWeek> weekend = new List<DayOfWeek>(
            new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday });

        static List<DateTime> fixedDateHolidaysList = new()
        {
            new DateTime(2013, 12, 25),
            new DateTime(2013, 12, 26),
            new DateTime(2014, 1, 14)
        };

        public List<DateTime> AdjustedFloatingDatePublicHolidays(int year) // these do NOT fall on weekeds by definition
        {
            List<Func<int, DateTime>> floatingDatePublicHolidays = new List<Func<int, DateTime>>()
            {
                // King's birthday is the first Monday of June
                (y) => GetFirstMondayInMonth(y, 6),

                // Add other floating date holidays here
            };

            List<DateTime> publicHolidays = new List<DateTime>();
            foreach (var holidayFunc in floatingDatePublicHolidays)
            {
                publicHolidays.Add(holidayFunc(year));
            }

            return publicHolidays;
        }

        private static DateTime GetFirstMondayInMonth(int year, int month)
        {
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DayOfWeek dayOfWeek = firstDayOfMonth.DayOfWeek;
            int daysToAdd = DayOfWeek.Monday - dayOfWeek;
            if (daysToAdd < 0) // iterated until the desired DayOfTheWeek is reached
            {
                daysToAdd += 7;
            }
            return firstDayOfMonth.AddDays(daysToAdd);
        }


        public List<DateTime> AdjustedFixedDatePublicHolidays(int year) // If a holiday falls on a weekend or not
        {
            List<DateTime> dateTimes = new List<DateTime>();
                
                DateTime previousDateWithYear = DateTime.MinValue; // this value is substituted on line 84 every iteration
                foreach(DateTime date in fixedDateHolidaysList)
                {
                    var dateWithYear = new DateTime(year, date.Month, date.Day); // The 'DayofTheWeek' will be correct for any year
                    if  (weekend.Contains(dateWithYear.DayOfWeek)) // if the iterated date falls on a weekend
                    {
                        TimeSpan checkForConsecutiveDays = dateWithYear - previousDateWithYear;
                        if (checkForConsecutiveDays.Days == 1) // Test if the previous and current dates are consecutive
                        {
                            if (date.DayOfWeek == DayOfWeek.Sunday) // this means the previous date is Saturday, and has been shifted to Monday
                            {
                                dateWithYear.AddDays(3); // because the previous have been shifted to Monday, this date must be shifted to Tuesday
                                dateTimes.Add(dateWithYear);
                            } 
                        }
                        else // iterating dates are NOT consecutive
                        {
                            if (date.DayOfWeek == DayOfWeek.Saturday)
                            {
                            dateWithYear.AddDays(2);
                                dateTimes.Add(dateWithYear);
                            }
                            else
                            {
                            dateWithYear.AddDays(1);
                                dateTimes.Add(dateWithYear);
                            }
                        }
                    }
                    else // the iterated date DOES NOT fall on a weekend and is added
                    {
                        dateTimes.Add(dateWithYear);
                    }
                    previousDateWithYear = dateWithYear;
                }
           
            return dateTimes;
        }
        public IList<DateTime> AllPublicHolidays(int year) // Collate all holidays in aparticular year
        {
            List<DateTime> allHolidays = new List<DateTime>();
            var floatingDateHolidays = AdjustedFloatingDatePublicHolidays(year);
            var fixedDateHolidays = AdjustedFixedDatePublicHolidays(year);
            allHolidays.AddRange(floatingDateHolidays);
            allHolidays.AddRange(fixedDateHolidays);
      
            return allHolidays;
        }
    }
}
