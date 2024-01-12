using System;

namespace Sprout.Exam.Business.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string TIN { get; set; }
        public int EmployeeTypeId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual decimal CalculateSalary()
        {
            // Base implementation for generic employees
            return 0;
        }
    }
}