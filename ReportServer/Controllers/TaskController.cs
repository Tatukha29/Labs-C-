using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReportDAL.Entity;
using ReportServer.Services;

namespace ReportServer.Controllers
{
    [ApiController]
    [Route("/Task")]
    public class TaskController : ControllerBase
    {
        private TaskService _serviceTask;

        public TaskController(TaskService serviceTask)
        {
            _serviceTask = serviceTask;
        }

        [HttpPost("/Task/CreateTask")]
        public IActionResult CreateTask([FromQuery] string taskName, [FromQuery] Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                return BadRequest();
            }
            Task task = _serviceTask.CreateTask(taskName, employeeId);
            return Ok(task);
        }
        
        [HttpPost("/Task/FindTaskById")]
        public IActionResult FindTaskById([FromQuery] Guid taskId)
        {
            if (taskId == Guid.Empty)
            {
                return BadRequest();
            }
            Task task = _serviceTask.FindTaskById(taskId);
            if (task != null)
            {
                return Ok(task);
            }
        
            return NotFound();
        }
        
        [HttpPost("/Task/FindTaskCreateData")]
        public IActionResult FindTaskCreateData([FromQuery] DateTime dateTime)
        {
            Task task = _serviceTask.FindTaskByCreateData(dateTime);
            if (task != null)
            {
                return Ok(task);
            }
        
            return NotFound();
        }
        
        [HttpPost("/Task/FindTaskLastUpdateData")]
        public IActionResult FindTaskLastUpdateData([FromQuery] DateTime dateTime)
        {
            List<Task> tasks = _serviceTask.FindTaskByLastUpdateData(dateTime);
            if (tasks != null)
            {
                return Ok(tasks);
            }
        
            return NotFound();
        }
        
        [HttpPost("/Task/ChangeTaskState")]
        public IActionResult ChangeTaskState([FromQuery] Guid taskId, [FromQuery] TaskState state)
        {
            if (taskId == Guid.Empty)
            {
                return BadRequest();
            }
            Task task = _serviceTask.ChangeTaskState(taskId, state);
            if (task != null)
            {
                return Ok(task);
            }
        
            return NotFound();
        }
        
        [HttpPost("/Task/AddTaskComment")]
        public IActionResult AddTaskComment([FromQuery] Guid taskId, [FromQuery] Guid employeeId, [FromQuery] string comment)
        {
            if (taskId == Guid.Empty || employeeId == Guid.Empty || comment == string.Empty)
            {
                return BadRequest();
            }
            
            Task task = _serviceTask.AddCommentTask(taskId, employeeId, comment);
            if (task != null)
            {
                return Ok(task);
            }
        
            return NotFound();
        }
        
        [HttpPost("/Task/ChangeTaskEmployee")]
        public IActionResult ChangeTaskEmployee([FromQuery] Guid taskId, [FromQuery] Guid employeeId)
        {
            if (taskId == Guid.Empty || employeeId == Guid.Empty)
            {
                return BadRequest();
            }
            Task task = _serviceTask.ChangeTaskEmployee(taskId, employeeId);
            if (task != null)
            {
                return Ok(task);
            }
        
            return NotFound();
        }
        
        [HttpPost("/Task/GetAllTask")]
        public IActionResult GetAllTask()
        {
            List<Task> tasks = _serviceTask.GetAllTask();
            return Ok(tasks);
        }
        
        [HttpPost("/Task/GetUpdateEmployee")]
        public IActionResult GetUpdateEmployee([FromQuery] Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                return BadRequest();
            }
            List<Task> tasks = _serviceTask.GetUpdateEmployee(employeeId);
            if (tasks != null)
            {
                return Ok(tasks);
            }
        
            return NotFound();
        }
    }
}
