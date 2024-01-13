using Microsoft.AspNetCore.Mvc;
using Moq;
using Sprout.Exam.Business.Contracts;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Models;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp;
using Sprout.Exam.WebApp.Controllers;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Sprout.Exam.Test
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private readonly EmployeesController _employeeController;
        public EmployeeControllerTests()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _employeeController = new EmployeesController(_employeeRepository.Object);
        }

        [Fact]
        public void GetAll_ReturnsAllActiveEmployees()
        {
            //arrange
            var employeeList = StaticEmployees.Result.AsEnumerable();
            _employeeRepository.Setup(x => x.GetAllAsync().Result)
                .Returns(employeeList);
            //act
            var result = _employeeController.Get().Result;
            //assert
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var actualEmployees = Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(okObjectResult.Value);
            Assert.Equal(employeeList.Count(), actualEmployees.Count());
        }


        [Fact]
        public void GetById_ReturnsEmployee()
        {
            //arrange
            var employeeList = StaticEmployees.Result;
            _employeeRepository.Setup(x => x.GetByIdAsync(2).Result)
                .Returns(employeeList[1]);
            //act
            var result = _employeeController.GetById(2).Result;
            //assert
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var actualEmployee = Assert.IsAssignableFrom<EmployeeDto>(okObjectResult.Value);
            Assert.Equal(employeeList[1].Id, actualEmployee.Id);
            Assert.True(employeeList[1].Id == actualEmployee.Id);
        }

        [Fact]
        public void AddEmployee_ReturnsCreated()
        {
            //arrange
            var employee = StaticEmployees.Result[1];

            var dto = new CreateEmployeeDto
            {
                Birthdate = employee.Birthdate,
                FullName = employee.FullName,
                Tin = employee.TIN,
                TypeId = employee.EmployeeTypeId
            };
            //act
            var result = _employeeController.Post(dto).Result;
            //assert
            Assert.NotNull(result);
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.IsType<int>(createdResult.Value);
        }

        [Fact]
        public void UpdatedEmployee_ReturnsOk()
        {
            //arrange
            var employee = StaticEmployees.Result[0];

            _employeeRepository.Setup(x => x.UpdateAsync(employee));
            _employeeRepository.Setup(x => x.GetByIdAsync(employee.Id).Result)
                .Returns(employee);

            var dto = new EditEmployeeDto
            {
                Id = employee.Id,
                Birthdate = employee.Birthdate,
                FullName = "Rutherson",
                Tin = employee.TIN,
                TypeId = employee.EmployeeTypeId
            };
            //act
            var result = _employeeController.Put(dto).Result;
            //assert
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var actualEmployee = Assert.IsAssignableFrom<Employee>(okObjectResult.Value);
            Assert.Equal(dto.FullName, actualEmployee.FullName);
            Assert.Equal(dto.Id, actualEmployee.Id);
        }

        [Fact]
        public void UpdatedEmployee_ReturnsNotFound()
        {
            //arrange
            var employee = StaticEmployees.Result[0];

            _employeeRepository.Setup(x => x.UpdateAsync(employee));
            _employeeRepository.Setup(x => x.GetByIdAsync(0).Result)
                .Returns((Employee)null);

            var dto = new EditEmployeeDto
            {
                Id = employee.Id,
                Birthdate = employee.Birthdate,
                FullName = "Rutherson",
                Tin = employee.TIN,
                TypeId = employee.EmployeeTypeId
            };
            //act
            var result = _employeeController.Put(dto).Result;
            //assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public void DeleteEmployee_ReturnsOk()
        {
            //arrange
            var employee = StaticEmployees.Result[0];
            _employeeRepository.Setup(x => x.DeleteAsync(employee));
            _employeeRepository.Setup(x => x.GetByIdAsync(employee.Id).Result)
                .Returns(employee);
            //act
            var result = _employeeController.Delete(employee.Id).Result;

            //assert
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<int>(okObjectResult.Value);
        }

        [Fact]
        public void DeleteEmployee_ReturnsNotFound()
        {
            //arrange
            var employee = StaticEmployees.Result[0];
            _employeeRepository.Setup(x => x.DeleteAsync(employee));
            _employeeRepository.Setup(x => x.GetByIdAsync(0).Result)
               .Returns((Employee)null);
            //act
            var result = _employeeController.Delete(employee.Id).Result;

            //assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void CalculateSalaryRegular_ReturnsOk()
        {
            //arrange
            var regularEmployee = StaticEmployees.Result.FirstOrDefault(e => e.EmployeeTypeId == (int)EmployeeType.Regular);

            _employeeRepository.Setup(x => x.GetByIdAsync(regularEmployee.Id).Result)
               .Returns(regularEmployee);

            //act
            var result = _employeeController.Calculate(regularEmployee.Id, new CalculateRequest
            {
                absentDays = 1,
                workedDays = 0
            }).Result;

            //assert
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var salary = Assert.IsType<decimal>(okObjectResult.Value);
            Assert.Equal(16690.91, (double)salary);
        }

        [Fact]
        public void CalculateSalaryContractual_ReturnsOk()
        {
            //arrange
            var regularEmployee = StaticEmployees.Result.FirstOrDefault(e => e.EmployeeTypeId == (int)EmployeeType.Contractual);

            _employeeRepository.Setup(x => x.GetByIdAsync(regularEmployee.Id).Result)
               .Returns(regularEmployee);

            //act
            var result = _employeeController.Calculate(regularEmployee.Id, new CalculateRequest
            {
                absentDays = 0,
                workedDays = 15.5m
            }).Result;

            //assert
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var salary = Assert.IsType<decimal>(okObjectResult.Value);
            Assert.Equal(7750.00, (double)salary);
        }
    }
}
