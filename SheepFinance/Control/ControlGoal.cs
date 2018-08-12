using SheepFinance.Data;
using SheepFinance.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Control
{
    public class ControlGoal
    {
        LocalDatabase database;

        Box Box;

        public ControlGoal()
        {
            database = LocalDatabase.GetInstance();
        }

        public List<Goal> GetGoalList() => database.GetGoals().Where(g => !g.Done).ToList();

        internal void SaveGoal(string name, double goalValue, DateTime deadline) => database.AddGoal(name, goalValue, deadline);

        private void LoadBox() => Box = new Box(database.GetAccounts().Sum(a => a.Amount), database.GetGoals().ToList());

        internal Box GetBox()
        {
            LoadBox();
            return Box;
        }

        internal void GoalDone(object goal)
        {
            ((Goal)goal).SetDone();
            database.UpdateGoal();
        }
    }
}
