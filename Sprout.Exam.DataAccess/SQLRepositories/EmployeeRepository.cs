using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.Contracts;
using Sprout.Exam.Business.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.SQLRepositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext dbContext;

        public EmployeeRepository(EmployeeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var employee = await dbContext.Employee
                .FirstOrDefaultAsync(e => e.IsDeleted == false && e.Id == id);

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = await dbContext.Employee.Where(e => e.IsDeleted == false).ToListAsync();

            return employees;
        }

        public async Task AddAsync(Employee employee)
        {
            await dbContext.Employee.AddAsync(employee);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            dbContext.Employee.Update(employee);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            if (employee != null)
            {
                employee.IsDeleted = true;
                dbContext.Employee.Update(employee);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
