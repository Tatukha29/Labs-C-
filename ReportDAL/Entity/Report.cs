using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.DependencyModel;

namespace ReportDAL.Entity
{
    public class Report
    {
        public Report(string name, Guid employeeId, string text)
        {
            Id = Guid.NewGuid();
            Name = name;
            Text = text;
            State = ReportState.Draft;
            EmployeeId = employeeId;
            CreateData = DateTime.Now;
            Tasks = new List<Task>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public ReportState State { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreateData { get; set; }
        public List<Task> Tasks { get; set; }
    }
}