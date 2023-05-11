using DesignCrowdJaimieCarter.src.Functionality;

namespace DesignCrowdJaimieCarter.src.Service
{
    public interface IProcessService
    {
        void Run();
    }
    internal class ProcessService : IProcessService
    {
        readonly BusinessDayCounter _businessdayCounter;
        public ProcessService()
        {
            _businessdayCounter = new BusinessDayCounter();
        }

        public void Run()
        {
            List<Entities.TotalWeekdays> totalWeekdays = new List<Entities.TotalWeekdays>();
            totalWeekdays.Add(new Entities.TotalWeekdays(){ FirstDate = new DateTime(2013, 10, 07), SecondDate = new DateTime(2013, 10, 09) });
            totalWeekdays.Add(new Entities.TotalWeekdays() { FirstDate = new DateTime(2013, 10, 05), SecondDate = new DateTime(2013, 10, 14) });
            totalWeekdays.Add(new Entities.TotalWeekdays() { FirstDate = new DateTime(2013, 10, 07), SecondDate = new DateTime(2014, 1, 01) });
            totalWeekdays.Add(new Entities.TotalWeekdays() { FirstDate = new DateTime(2013, 10, 07), SecondDate = new DateTime(2013, 10, 05) });

            List<Entities.TotalBusinessDays> totalBusinessdays = new List<Entities.TotalBusinessDays>();
            totalBusinessdays.Add(new Entities.TotalBusinessDays() { FirstDate = new DateTime(2013, 10, 07), SecondDate = new DateTime(2013, 10, 09) });
            totalBusinessdays.Add(new Entities.TotalBusinessDays() { FirstDate = new DateTime(2013, 12, 24), SecondDate = new DateTime(2013, 12, 27) });
            totalBusinessdays.Add(new Entities.TotalBusinessDays() { FirstDate = new DateTime(2013, 10, 07), SecondDate = new DateTime(2014, 1, 01) });

            List<Views.TotalWeekdays> totalWeekdaysView = _businessdayCounter.TotalWeekdaysViewList(totalWeekdays);

            Console.WriteLine();
            Console.WriteLine("Task One: Weekdays Between Two Dates");
            Console.WriteLine();

            foreach (var weekdaysView in totalWeekdaysView)
            {
                Console.WriteLine($"{weekdaysView.FirstDate}, {weekdaysView.SecondDate}, {weekdaysView.TotalDays}");
            }

            Console.WriteLine();
            Console.WriteLine("Task Two: Business Days Between Two Dates");
            Console.WriteLine();

            List<Views.TotalBusinessDays> totalBusinessDaysView = _businessdayCounter.TotalBusinessDaysViewList(totalBusinessdays);

            foreach (var businessDayaysView in totalBusinessDaysView)
            {
                Console.WriteLine($"{businessDayaysView.FirstDate}, {businessDayaysView.SecondDate}, {businessDayaysView.TotalDays}");
            }

            Console.WriteLine();
            Console.WriteLine("Thanks for the consideration, JC");
            Console.WriteLine();
        }
    }
}
