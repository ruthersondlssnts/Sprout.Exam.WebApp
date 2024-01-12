using Sprout.Exam.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task AddAsync(Employee employee);
        Task<Employee> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(Employee id);
    }
}
