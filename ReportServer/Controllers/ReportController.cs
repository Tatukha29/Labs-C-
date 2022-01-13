using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReportDAL.Entity;
using ReportServer.Services;

namespace ReportServer.Controllers
{
    [ApiController]
    [Route("/Report")]
    public class ReportController : ControllerBase
    {
        private ReportService _serviceReport;

        public ReportController(ReportService serviceReport)
        {
            _serviceReport = serviceReport;
        }

        [HttpPost("/Report/CreateDailyReport")]
        public IActionResult CreateDailyReport([FromQuery] string name, [FromQuery] Guid employeeId, [FromQuery] string text, [FromQuery] DateTime dateTime)
        {
            if (employeeId == Guid.Empty)
            {
                return BadRequest();
            }

            Report report = _serviceReport.CreateDailyReport(name, employeeId, dateTime, text);
            if (report != null)
            {
                return Ok(report);
            }

            return BadRequest();
        }

        [HttpPost("/Report/CreateSprintReport")]
        public IActionResult CreateSprintReport([FromQuery] string name, [FromQuery] Guid employeeId, [FromQuery] string text, [FromQuery] DateTime startData, [FromQuery] DateTime endData)
        {
            if (employeeId == Guid.Empty)
            {
                return BadRequest();
            }

            Report sprintReport = _serviceReport.CreateSprintReport(name, employeeId, text, startData, endData);
            if (sprintReport != null)
            {
                return Ok(sprintReport);
            }

            return BadRequest();
        }

        [HttpPost("/Report/AddTasksReport")]
        public IActionResult AddTasksReport([FromQuery] Guid reportId, [FromQuery] Guid employeeId)
        {
            if (employeeId == Guid.Empty || reportId == Guid.Empty)
            {
                return BadRequest();
            }

            Report report = _serviceReport.AddTaskDailyReport(reportId, employeeId);
            if (report != null)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("/Report/GetAllReport")]
        public IActionResult GetAllReport()
        {
            List<Report> reports = _serviceReport.GetAllReports();
            return Ok(reports);

        }
        
        [HttpPost("/Report/GetTaskSplintReport")]
        public IActionResult GetTaskSplintReport([FromQuery] Guid reportId)
        {
            if (reportId == Guid.Empty)
            {
                return BadRequest();
            }
            List<Task> tasks = _serviceReport.GetTaskSplintReport(reportId);
            if (tasks != null)
            {
                return Ok(tasks);
            }

            return BadRequest();

        }
        
        [HttpPost("/Report/GetDailyReportsEmployee")]
        public IActionResult GetDailyReportsEmployee([FromQuery] Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                return BadRequest();
            }

            List<Report> reports = _serviceReport.GetDailyReportsEmployee(employeeId);
            if (reports != null)
            {
                return Ok(reports);
            }

            return BadRequest();
        }
        
        [HttpPost("/Report/GetWhoDontDoDailyReports")]
        public IActionResult GetWhoDontDoDailyReports([FromQuery] Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                return BadRequest();
            }

            List<Employee> employees = _serviceReport.GetWhoDontDoDailyReports(employeeId);
            if (employees != null)
            {
                return Ok(employees);
            }

            return BadRequest();
        }
    }
}