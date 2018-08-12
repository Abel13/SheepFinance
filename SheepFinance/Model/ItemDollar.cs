using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SheepFinance.Model
{
    public class ItemDollar
    {
        public string Value { get; private set; }
        public string Day { get; private set; }

        public ItemDollar(double value, DateTime day)
        {
            Value = value.ToString("c");

            if (day.Day.Equals(DateTime.Now.Day))
                Day = "hoje";
            else if (day.Day.Equals(DateTime.Now.AddDays(-1).Day))
                Day = "ontem";
            else
                Day = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(day.DayOfWeek);
        }
    }
}
