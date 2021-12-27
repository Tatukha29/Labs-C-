using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReportDAL.Entity;
using ReportServer.Services;

namespace ReportServer.Controllers
{
    [ApiController]
    [Route("/Employee")]
    public class EmployeeController : ControllerBase
    {
        private EmployeeService _service;
        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        [HttpPost("/Employee/CreateEmployee")]
        public IActionResult CreateEmployee([FromQuery] string name, [FromQuery] Guid teamleadId)
        {
            Employee employee = _service.CreateEmployee(name, teamleadId);
            if (employee != null)
            {
                return Ok(employee);
            }

            return BadRequest();
        }
        
        [HttpDelete("/Employee/DeleteEmployee")]
        public IActionResult DeleteEmployee([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            _service.DeleteEmployee(id);
            return Ok();
        }
        
        [HttpPost("/Employee/UpdateNameEmployee")]
        public IActionResult UpdateNameEmployee([FromQuery] Guid id, [FromQuery] string newName)
        {
            if (id == Guid.Empty || newName == string.Empty)
            {
                return BadRequest();
            }
            Employee employee = _service.UpdateNameEmployee(id, newName);
            return Ok(employee);
        }
        
        [HttpPost("/Employee/UpdateTeamleadEmployee")]
        public IActionResult UpdateTeamleadEmployee([FromQuery] Guid id, [FromQuery] Guid newTeamleadId)
        {
            if (id == Guid.Empty || newTeamleadId == Guid.Empty)
            {
                return BadRequest();
            }
            Employee employee = _service.UpdateTeamleadEmployee(id, newTeamleadId);
            return Ok(employee);
        }

        [HttpGet("/Employee/GetById")]
        public IActionResult GetById([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            var employee = _service.GetById(id);
            if (employee != null)
            {
                return Ok(employee);
            }

            return NotFound();
        }

        [HttpGet("/Employee/GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            List<Employee> employees = _service.GetAllEmployees();
            return Ok(employees);
        }
    }
}