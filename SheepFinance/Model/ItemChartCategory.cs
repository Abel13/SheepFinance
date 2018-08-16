using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace SheepFinance.Model
{
    public class ItemChartCategory
    {
        public string Month { get; private set; }
        public int Year { get; private set; }
        public SeriesCollection SeriesCollection { get; private set; }
        public DateTime Date { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueByGroup"></param>
        /// <param name="date">only MM/YY, the day will be set 01 as default</param>
        public ItemChartCategory(string date)
        {
            Date = DateTime.Parse("1/" + date);
            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Date.Month);
            Year = Date.Year;
            SeriesCollection = new SeriesCollection();
        }

        public void SetSeries(string categoryGroup, double value)
        {
            SeriesCollection.Add(
                new PieSeries
                {
                    Title = categoryGroup,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(value) },
                    DataLabels = true
                });
            
        }
    }
}
