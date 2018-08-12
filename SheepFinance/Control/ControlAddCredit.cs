using SheepFinance.Data;
using SheepFinance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Control
{
    public class ControlAddCredit
    {
        LocalDatabase database;

        private Goal Goal { get; set; }

        public ControlAddCredit(object goal)
        {
            database = LocalDatabase.GetInstance();
            Goal = (Goal)goal;
        }

        public object GetGoal() => Goal;

        internal void AddCredit(double value)
        {
            Goal.Credit(value);
            database.UpdateGoal();
        }
    }
}
