using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Model
{
    public class Goal
    {
        public string Name { get; private set; }
        public double Balance { get; private set; }
        public double GoalValue { get; private set; }
        public DateTime Deadline { get; private set; }
        public bool Done { get; private set; }

        public Goal(string name, double goalValue, DateTime deadline)
        {
            Name = name;
            GoalValue = goalValue;
            Deadline = deadline;
        }

        [JsonConstructor]
        public Goal(string name, double goalValue, DateTime deadline, double balance, bool done)
        {
            Name = name;
            GoalValue = goalValue;
            Deadline = deadline;
            Balance = balance;
            Done = done;
        }

        public void Credit(double value) => Balance += value;

        public void Debit(double value) => Balance -= value;

        public void AchievementComplete() => Done = true;

        internal void SetDone()
        {
            Balance = 0;
            Done = true;
        }
    }
}
