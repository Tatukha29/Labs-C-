using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyModel;

namespace ReportDAL.Entity
{
    public class Employee
    {
        public Employee(string name, Guid teamleadId)
        {
            Name = name;
            Id = Guid.NewGuid();
            TeamleadId = teamleadId;
            //Reports = new List<Report>();
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid TeamleadId { get; set; }
        //public List<Report> Reports { get; set; }
    }
}