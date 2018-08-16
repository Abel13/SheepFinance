using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Model
{
    public class Category
    {
        public CategoryGroup Group { get; private set; }
        public string Name { get; private set; }

        public Category(string name, CategoryGroup group)
        {
            Name = name;
            Group = group;
        }
    }
}
