# Calculate the number of week days and business days between two dates.

## Run Commands

`dotnet run`

# Task One: Weekdays Between Two Dates

Calculates the number of weekdays in between two dates.
● Weekdays are Monday, Tuesday, Wednesday, Thursday, Friday.
● The returned count should not include either firstDate or secondDate -
e.g. between Monday 07-Oct-2013 and Wednesday 09-Oct-2013 is one weekday.
● If secondDate is equal to or before firstDate, return 0.

# Task Two: Business Days Between Two Dates

Calculate the number of business days in between two dates.
● Business days are Monday, Tuesday, Wednesday, Thursday, Friday, but excluding any
dates which appear in the supplied list of public holidays.
● The returned count should not include either firstDate or secondDate - e.g. between Monday
07-Oct-2013 and Wednesday 09-Oct-2013 is one weekday.
● If secondDate is equal to or before firstDate, return 0.

Sample list of Public Holidays:
● 25th December 2013
● 26th December 2013
● 1st January 2014

# Summary

Date range is input via a List of TotalWeekdays or TotalBusinessDays objects in
the Run() method in ProcessService.cs

I have included only one 'FloatingDate' public holiday calculation e.g. King's Birthday public holiday. 
It can be seen how other 'Floating Date' holidays can be calculated using the same or simplar mehodology.

Likewise with 'Fixed Date' public holidays, only Christmas Day, BoxingDay and New Years Day are included.
It can be seen how other 'Fixed Date' holidays can be calculated using the same or simplar mehodology.