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
        public bool IsCategory { get; private set; }

        public Goal(string name, double goalValue, DateTime deadline)
        {
            Name = name;
            GoalValue = goalValue;
            Deadline = deadline;
        }

        /// <summary>
        /// Constructor for Category Goal
        /// </summary>
        /// <param name="name"></param>
        public Goal(string name)
        {
            Name = name;
            Done = true;
            IsCategory = true;
        }

        [JsonConstructor]
        public Goal(string name, double goalValue, DateTime deadline, double balance, bool done, bool isCategory)
        {
            Name = name;
            GoalValue = goalValue;
            Deadline = deadline;
            Balance = balance;
            Done = done;
            IsCategory = isCategory;
        }

        public void Credit(double value) => Balance += value;

        public void Debit(double value) => Balance -= value;

        internal void SetDone()
        {
            Balance = 0;
            Done = true;
        }

        internal void Clean()
        {
            Balance = 0;
        }
    }
}