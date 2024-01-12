using System;

namespace Sprout.Exam.Business.Models
{
    public class ContractualEmployee : Employee
    {
        private decimal dailyRate = 500;
        public decimal workedDays { get; set; }

        public ContractualEmployee SetWorkedDays(decimal workedDays)
        {
            this.workedDays = workedDays;
            return this;
        }

        public override decimal CalculateSalary()
        {
            return Math.Round(dailyRate * workedDays, 2);
        }
    }
}
