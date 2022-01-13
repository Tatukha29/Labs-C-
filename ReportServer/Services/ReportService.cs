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
    public class ReportService
    {
        private ReportDbContext _context;

        public ReportService(ReportDbContext context)
        {
            _context = context;
        }

        public Report CreateDailyReport(string name, Guid employeeId, DateTime dateTime, string text)
        {
            Report report = new Report(name, employeeId, text);
            foreach (var task in _context.Tasks)
            {
                if (task.EmployeeId == employeeId && task.CreateData.ToString("d") == dateTime.ToString("d"))
                {
                    report.Tasks.Add(task);
                }
            }
            _context.Reports.Add(report);
            _context.SaveChanges();
            return report;
        }

        public Report CreateSprintReport(string name, Guid employeeId, string text, DateTime startData, DateTime endData)
        {
            Employee employee = _context.Employees.FirstOrDefault(employees => employees.Id.Equals(employeeId));
            if (employee.TeamleadId != Guid.Empty)
            {
                return null;
            }
            Report sprintReport = new Report(name, employeeId, text);
            foreach (var allEmployee in _context.Employees)
            {
                if (allEmployee.TeamleadId != employeeId) continue;
                foreach (var allTask in _context.Tasks)
                {
                    if (allTask.EmployeeId == allEmployee.Id && allTask.CreateData > startData && allTask.CreateData < endData)
                    {
                        sprintReport.Tasks.Add(allTask);
                    }
                }
            }

            foreach (var task in _context.Tasks)
            {
                if (employee != null && task.EmployeeId == employee.Id)
                {
                    sprintReport.Tasks.Add(task);
                }
            }

            _context.Reports.Add(sprintReport);
            _context.SaveChanges();
            return sprintReport;
        }

        public Report AddTaskDailyReport(Guid reportId, Guid taskId)
        {
            Report report = _context.Reports.Include("Include").FirstOrDefault(reports => reports.Id.Equals(reportId));
            Task task = _context.Tasks.Include("Updates").FirstOrDefault(tasks => tasks.Id.Equals(taskId));
            report.Tasks.Add(task);
            _context.SaveChanges();
            return report;
        }

        public List<Task> GetTaskSplintReport(Guid reportId)
        {
            Report report = _context.Reports.Include("Tasks").FirstOrDefault(reports => reports.Id.Equals(reportId));
            List<Task> tasks = new List<Task>();
            tasks.AddRange(report.Tasks);
            _context.SaveChanges();
            return tasks;
        }

        public List<Report> GetDailyReportsEmployee(Guid employeeId)
        {
            List<Employee> employees = _context.Employees.Where(allEmployee => allEmployee.TeamleadId == employeeId).ToList();
            List<Report> reports = new List<Report>();
            foreach (var allReports in _context.Reports)
            {
                foreach (var allEmployees in employees)
                {
                    if (allReports.EmployeeId == allEmployees.Id)
                    {
                        reports.Add(allReports);
                    }
                }
            }

            return reports;

        }
        
        public List<Employee> GetWhoDontDoDailyReports(Guid employeeId)
        {
            List<Employee> employees = _context.Employees.Where(allEmployee => allEmployee.TeamleadId == employeeId).ToList();
            foreach (var allReports in _context.Reports)
            {
                foreach (var allEmployees in employees)
                {
                    if (allReports.EmployeeId == allEmployees.Id)
                    {
                        employees.Remove(allEmployees);
                        break;
                    }
                }
            }

            return employees;

        }

        public Report UpdateTextReport(Guid reportId, string updateText)
        {
            Report report = _context.Reports.Include("Tasks").FirstOrDefault(reports => reports.Id.Equals(reportId));
            if (report.State == ReportState.Ready)
            {
                return null;
            }
            report.Text = updateText;
            _context.SaveChanges();
            return report;
        }

        public Report UpdateStateReport(Guid reportId)
        {
            Report report = _context.Reports.Include("Tasks").FirstOrDefault(reports => reports.Id.Equals(reportId));
            report.State = ReportState.Ready;
            return report;
        }

        public List<Report> GetAllReports()
        {
            return _context.Reports.Include("Tasks").ToList();
        }
    }
}