using Sprout.Exam.Business.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task AddAsync(CreateEmployeeDto employee);
        Task<EmployeeDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(EditEmployeeDto id);
    }
}
