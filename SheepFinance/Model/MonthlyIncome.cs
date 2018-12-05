using LiveCharts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Model
{
    public class MonthlyIncome
    {
        public DateTime Date { get; private set; }
        public string Month { get; private set; }
        public int Year { get; private set; }
        public double Incomings { get; private set; }

        public MonthlyIncome(string date)
        {
            Date = DateTime.Parse("1/" + date);
            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Date.Month);
            Year = Date.Year;
            Incomings = 0;
        }
        
        public void SetIncoming(double value)
        {
            Incomings = value;
        }
    }
}
