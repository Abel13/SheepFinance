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

        public List<Goal> GetGoalList(bool done) => database.GetGoals().Where(g => g.Done == done && !g.IsCategory).OrderByDescending(g => g.Balance).ToList();
        public List<Goal> GetCategoryList() => database.GetGoals().Where(g => g.IsCategory).ToList();

        internal void SaveGoal(string name, double goalValue, DateTime deadline) => database.AddGoal(name, goalValue, deadline);

        private void LoadBox() => Box = new Box(database.GetAccounts().Sum(a => a.Amount), database.GetGoals().ToList());

        internal Box GetBox()
        {
            LoadBox();
            return Box;
        }

        internal void GoalDone(object goal)
        {
            if (!((Goal)goal).IsCategory)
            {
                ((Goal)goal).SetDone();
                database.UpdateGoal();
            }
        }

        internal GoalCategory GetCategories()
        {
            var essential = GetCategoryList().Where(g => g.Name.Equals("Essencial")).Select(g => g).FirstOrDefault();
            var education = GetCategoryList().Where(g => g.Name.Equals("Educação")).Select(g => g).FirstOrDefault();
            var investiment = GetCategoryList().Where(g => g.Name.Equals("Investimento")).Select(g => g).FirstOrDefault();
            var other = GetCategoryList().Where(g => g.Name.Equals("Foda-se")).Select(g => g).FirstOrDefault();

            return new GoalCategory(essential, education, investiment, other);
        }

        internal void Categorize()
        {
            if (Box.AmountAvailable > 0)
            {
                var category = GetCategories();
                category.SetValues(Box.AmountAvailable);
                database.UpdateGoal();
            }
        }

        internal void Clean()
        {
            var category = GetCategories();
            category.CleanValues();
            database.UpdateGoal();
        }
    }
}