using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.Business.Models
{
    public class EmployeeFactory
    {
        public static Employee CreateEmployee(EmployeeType employeeType, CalculateRequest calculateRequest)
        {
            switch (employeeType)
            {
                case EmployeeType.Regular:
                    return new RegularEmployee().SetAbsences(calculateRequest.absentDays);
                case EmployeeType.Contractual:
                    return new ContractualEmployee().SetWorkedDays(calculateRequest.workedDays);
                default:
                    return new Employee();
            }
        }
    }
}