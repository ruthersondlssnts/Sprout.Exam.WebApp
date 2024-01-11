using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.Contracts;
using Sprout.Exam.Business.DataTransferObjects;
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

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var employee = await dbContext.Employee
                .FirstOrDefaultAsync(e => e.IsDeleted == false && e.Id == id);

            return new EmployeeDto
            {
                Birthdate = employee.Birthdate.ToShortDateString(),
                FullName = employee.FullName,
                Id = employee.Id,
                Tin = employee.TIN,
                TypeId = employee.EmployeeTypeId,
            };
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await dbContext.Employee.Where(e => e.IsDeleted == false).ToListAsync();

            return employees.Select(employee => new EmployeeDto
            {
                Birthdate = employee.Birthdate.ToShortDateString(),
                FullName = employee.FullName,
                Id = employee.Id,
                Tin = employee.TIN,
                TypeId = employee.EmployeeTypeId,
            });
        }

        public async Task AddAsync(CreateEmployeeDto employee)
        {
            var employeeEntity = new Employee
            {
                Birthdate = employee.Birthdate,
                FullName = employee.FullName,
                TIN = employee.Tin,
                EmployeeTypeId = employee.TypeId
            };

            await dbContext.Employee.AddAsync(employeeEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditEmployeeDto employee)
        {
            var employeeEntity = new Employee
            {
                Id = employee.Id,
                Birthdate = employee.Birthdate,
                FullName = employee.FullName,
                TIN = employee.Tin,
                EmployeeTypeId = employee.TypeId
            };

            dbContext.Employee.Update(employeeEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await dbContext.Employee
                .FirstOrDefaultAsync(e => e.IsDeleted == false && e.Id == id);

            if (employee != null)
            {
                employee.IsDeleted = true;
                dbContext.Employee.Update(employee);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
