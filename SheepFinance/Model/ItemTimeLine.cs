using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SheepFinance.Model
{
    public class ItemTimeLine
    {
        

        public string Type { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public string Value { get; private set; }
        public Brush Brush { get; private set; }

        public ItemTimeLine(string type, string description, string value, DateTime date)
        {
            switch(type)
            {
                case "expense":
                    Type = "SAÍDA";
                    Brush = new SolidColorBrush(Color.FromRgb(222,41,41));
                    break;
                case "incoming":
                    Type = "ENTRADA";
                    Brush = new SolidColorBrush(Color.FromRgb(41, 130, 222));
                    break;
                case "transfer":
                    Type = "TRANSFERÊNCIA";
                    Brush = new SolidColorBrush(Color.FromRgb(41, 222, 41));
                    break;
            }

            Description = description;
            Value = value;
            Date = date;
        }
    }
}
