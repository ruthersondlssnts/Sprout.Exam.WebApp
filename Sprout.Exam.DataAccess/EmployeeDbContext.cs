﻿using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.Models;

namespace Sprout.Exam.DataAccess
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
    }
}
