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
    public class TaskService
    {
        private ReportDbContext _context;
        
        public TaskService(ReportDbContext context)
        {
            _context = context;
        }

        public Task CreateTask(string name, Guid employeeId)
        {
            Task task = new Task(name, employeeId);
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public Task FindTaskById(Guid taskId)
        {
            return _context.Tasks.FirstOrDefault(task => task.Id.Equals(taskId));
        }
        
        public Task FindTaskByCreateData(DateTime dateTime)
        {
            return _context.Tasks.FirstOrDefault(task => task.CreateData.Equals(dateTime));
        }
        
        public Task FindTaskByCreateEmployee(Guid employeeId)
        {
            return _context.Tasks.FirstOrDefault(task => task.EmployeeId.Equals(employeeId));
        }
        
        public List<Task> FindTaskByLastUpdateData(DateTime dateTime)
        {
            return _context.Tasks.Where(task => task.Updates.Last().Data == dateTime).ToList();
        }
        
        public Task ChangeTaskState(Guid taskId, TaskState state)
        {
            Task task = _context.Tasks.FirstOrDefault(task => task.Id.Equals(taskId));
            task.State = state;
            _context.SaveChanges();
            return task;
        }
        
        public Task AddCommentTask(Guid taskId, Guid employeeId, string comment)
        {
            Update update = new Update(employeeId, comment);
            Task task = _context.Tasks.Include("Updates").FirstOrDefault(task => task.Id.Equals(taskId));
            if (task.State == TaskState.Open)
            {
                task.State = TaskState.Active;
            }
        
            if (task.State == TaskState.Resolved)
            {
                return null;
            }
            task.Updates.Add(update);
            _context.Updates.Add(update);
            _context.SaveChanges();
            return task;
        }
        
        public Task ChangeTaskEmployee(Guid taskId, Guid employeeId)
        {
            Task task = _context.Tasks.FirstOrDefault(task => task.Id.Equals(taskId));
            task.EmployeeId = employeeId;
            _context.SaveChanges();
            return task;
        }
        
        public List<Task> GetAllTask()
        {
            return _context.Tasks.Include("Updates").ToList();
        }
        
        public List<Task> GetUpdateEmployee(Guid employeeId)
        {
            List<Task> tasks = new List<Task>();
            tasks.AddRange(_context.Tasks.Where(task => task.Updates.Last().EmployeeId == employeeId));
            return tasks;
        }
        
        public List<Task> GetTaskEmployee(Guid employeeId)
        {
            List<Task> tasks = new List<Task>();
            tasks.AddRange(_context.Tasks.Where(task => task.EmployeeId == employeeId));
            return tasks;
        }
    }
}