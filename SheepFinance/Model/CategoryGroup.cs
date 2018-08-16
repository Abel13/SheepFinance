using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Model
{
    public class CategoryGroup
    {
        public string Name;

        public CategoryGroup(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
