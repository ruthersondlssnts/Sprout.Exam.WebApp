using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Models;
using System;
using System.Collections.Generic;

namespace Sprout.Exam.WebApp
{
    public static class StaticEmployees
    {
        public static List<EmployeeDto> ResultList = new()
        {
            new EmployeeDto
            {
                Birthdate = "1993-03-25",
                FullName = "Jane Doe",
                Id = 1,
                Tin = "123215413",
                TypeId = 1
            },
            new EmployeeDto
            {
                Birthdate = "1993-05-28",
                FullName = "John Doe",
                Id = 2,
                Tin = "957125412",
                TypeId = 2
            }
        };

        public static List<Employee> Result = new()
        {
            new Employee
            {
                Birthdate = Convert.ToDateTime("1993-03-25"),
                FullName = "Jane Doe",
                Id = 1,
                TIN = "123215413",
                EmployeeTypeId = 1,
                IsDeleted= true,
            },
            new Employee
            {
                Birthdate = Convert.ToDateTime("1993-05-28"),
                FullName = "John Doe",
                Id = 2,
                TIN = "957125412",
                 EmployeeTypeId = 2,
                IsDeleted= true,
            }
        };
    }
}
