using Microsoft.EntityFrameworkCore;
using ReportDAL.Entity;

namespace ReportDAL.DataBase
{
    public class ReportDbContext : DbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public ReportDbContext()
        {
            DbPath = @"D:\oopitmo\ReportDAL\data.db";
        }

        public string DbPath { get; private set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Update> Updates { get; set; }
        public DbSet<Report> Reports { get; set; }

    }
}
