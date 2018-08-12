using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Model
{
    public class ItemChart
    {
        public DateTime Date { get; private set; }
        public string Month { get; private set; }
        public int Year { get; private set; }
        public double Expenses { get; private set; }
        public double Incomings { get; private set; }

        public ItemChart(string date)
        {
            Date = DateTime.Parse("1/" + date);
            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Date.Month);
            Year = Date.Year;
            Expenses = 0;
            Incomings = 0;
        }

        public void SetExpense(double value)
        {
            Expenses = value;
        }

        public void SetIncoming(double value)
        {
            Incomings = value;
        }
    }
}
