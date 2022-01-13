using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace ReportDAL.Entity
{
    public class Update
    {
        public Update(Guid employeeId, string comment)
        {
            Data = DateTime.Now;
            EmployeeId = employeeId;
            Comment = comment;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public Guid EmployeeId { get; set; }
        public string Comment { get; set; }
        public Update() {}
    }
}