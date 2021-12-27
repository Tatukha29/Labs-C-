using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.EntityFrameworkCore;
using ReportDAL.DataBase;
using ReportDAL.Entity;
using ReportServer.Controllers;

namespace ReportServer.Services
{
    public class EmployeeService
    {
        private ReportDbContext _context;

        public EmployeeService(ReportDbContext context)
        {
            _context = context;
        }

        public Employee CreateEmployee(string name, Guid teamleadId)
        {
            var employee = new Employee(name, teamleadId);
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public void DeleteEmployee(Guid id)
        {
            Employee employee = _context.Employees.FirstOrDefault(employees => employees.Id.Equals(id));
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public Employee UpdateNameEmployee(Guid id, string newName)
        {
            Employee employee = _context.Employees.FirstOrDefault(employees => employees.Id.Equals(id));
            employee.Name = newName;
            _context.SaveChanges();
            return employee;

        }
        
        public Employee UpdateTeamleadEmployee(Guid id, Guid teamleadId)
        {
            Employee employee = _context.Employees.FirstOrDefault(employees => employees.Id.Equals(id));
            employee.TeamleadId = teamleadId;
            _context.SaveChanges();
            return employee;

        }

        public Employee GetById(Guid id)
        { 
            Employee employee = _context.Employees.FirstOrDefault(employee => employee.Id.Equals(id));
            return employee;
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }
    }
}