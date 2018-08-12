using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Model
{
    public class MenuItem
    {
        public MenuItem(string title, PackIconKind icon)
        {
            Title = title;
            Icon = icon;
        }

        public string Title { get; private set; }
        public PackIconKind Icon { get; private set; }
    }
}
