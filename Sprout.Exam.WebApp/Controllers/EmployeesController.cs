using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Business.Contracts;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Models;
using Sprout.Exam.Common.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeRepository.GetAllAsync();
            return Ok(result.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Birthdate = e.Birthdate.ToString("yyyy-MM-dd"),
                FullName = e.FullName,
                Tin = e.TIN,
                TypeId = e.EmployeeTypeId
            }));
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _employeeRepository.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            var item = await _employeeRepository.GetByIdAsync(input.Id);

            if (item == null) return NotFound();

            item.FullName = input.FullName;
            item.TIN = input.Tin;
            item.Birthdate = input.Birthdate;
            item.EmployeeTypeId = input.TypeId;

            await _employeeRepository.UpdateAsync(item);

            return Ok(item);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            var employee = new Employee()
            {
                FullName = input.FullName,
                TIN = input.Tin,
                Birthdate = input.Birthdate,
                EmployeeTypeId = input.TypeId,
                IsDeleted = false
            };

            await _employeeRepository.AddAsync(employee);


            return Created($"/api/employees/{employee.Id}", employee.Id);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeRepository.GetByIdAsync(id);

            if (result == null) return NotFound();

            await _employeeRepository.DeleteAsync(result);

            return Ok(id);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(int id, decimal absentDays, decimal workedDays)
        {
            var result = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == id));

            if (result == null) return NotFound();
            var type = (EmployeeType)result.TypeId;
            return type switch
            {
                EmployeeType.Regular =>
                    //create computation for regular.
                    Ok(25000),
                EmployeeType.Contractual =>
                    //create computation for contractual.
                    Ok(20000),
                _ => NotFound("Employee Type not found")
            };

        }

    }
}
