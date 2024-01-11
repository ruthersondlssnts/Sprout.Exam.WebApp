using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Models;
using Sprout.Exam.DataAccess.Contracts;
using Sprout.Exam.WebApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.SQLRepositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var employee = await dbContext.Employees.FindAsync(id);

            return new EmployeeDto
            {
                Birthdate = employee.Birthdate,
                FullName = employee.FullName,
                Id = employee.Id,
                Tin = employee.Tin,
                TypeId = employee.TypeId,
            };
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await dbContext.Employees.ToListAsync();

            return employees.Select(employee => new EmployeeDto
            {
                Birthdate = employee.Birthdate,
                FullName = employee.FullName,
                Id = employee.Id,
                Tin = employee.Tin,
                TypeId = employee.TypeId
            });
        }

        public async Task AddAsync(CreateEmployeeDto employee)
        {
            var employeeEntity = new Employee
            {
                Birthdate = employee.Birthdate.ToShortDateString(),
                FullName = employee.FullName,
                Tin = employee.Tin,
                TypeId = employee.TypeId
            };

            await dbContext.Employees.AddAsync(employeeEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditEmployeeDto employee)
        {
            var employeeEntity = new Employee
            {
                Id = employee.Id,
                Birthdate = employee.Birthdate.ToShortDateString(),
                FullName = employee.FullName,
                Tin = employee.Tin,
                TypeId = employee.TypeId
            };

            dbContext.Employees.Update(employeeEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                dbContext.Employees.Remove(employee);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
