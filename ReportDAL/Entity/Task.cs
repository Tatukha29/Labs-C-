using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace ReportDAL.Entity
{
    public class Task
    {
        public Task(string name, Guid employeeId)
        {
            Name = name;
            Id = Guid.NewGuid();
            EmployeeId = employeeId;
            State = TaskState.Open;
            CreateData = DateTime.Now;
            Updates = new List<Update>();
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public TaskState State { get; set; }
        public DateTime CreateData { get; set; }
        public List<Update> Updates { get; set; }

    }
}