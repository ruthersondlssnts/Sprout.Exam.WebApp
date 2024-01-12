using System;

namespace Sprout.Exam.Business.Models
{
    public class RegularEmployee : Employee
    {
        private decimal monthlySalary = 20000;
        private decimal absences;

        public RegularEmployee SetAbsences(decimal absences)
        {
            this.absences = absences;
            return this;
        }

        public override decimal CalculateSalary()
        {
            decimal dailyDeduction = monthlySalary / 22;
            decimal totalDeduction = absences * dailyDeduction;
            decimal tax = monthlySalary * 0.12m;

            return Math.Round(monthlySalary - totalDeduction - tax, 2);
        }
    }
}
