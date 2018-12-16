using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Model
{
    public class GoalCategory
    {
        public Goal Essential { get; private set; }
        public Goal Education { get; private set; }
        public Goal Investiment { get; private set; }
        public Goal Other { get; private set; }

        public void SetValues(double amountAvailable)
        {
            Essential.Credit(amountAvailable * 0.55);
            Education.Credit(amountAvailable * 0.05);
            Investiment.Credit(amountAvailable * 0.3);
            Other.Credit(amountAvailable * 0.1);
        }

        public GoalCategory(Goal essentials, Goal education, Goal investiment, Goal other)
        {
            Essential = essentials;
            Education = education;
            Investiment = investiment;
            Other = other;
        }
    }
}
