using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Model
{
    public class Box
    {
        public List<Goal> Goals { get; private set; }
        public double AmountAvailable { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TotalAmount">Total amount sum of all accounts</param>
        public Box(double TotalAmount, List<Goal> goals)
        {
            AmountAvailable = TotalAmount - goals.Sum(g=>g.Balance);
            Goals = goals;
        }
    }
}
