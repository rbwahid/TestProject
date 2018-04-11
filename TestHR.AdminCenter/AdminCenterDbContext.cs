using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TestHR.AdminCenter
{
    public class AdminCenterDbContext : DbContext
    {
        public DbSet<Company> Company { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Holiday> Holiday { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DbSet<LeaveApplication> LeaveApplication { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
        public DbSet<LeaveComment> LeaveComment { get; set; }

        public AdminCenterDbContext()
            : base("DefaultConnection")
        {
            //Database.SetInitializer<AdminCenterDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<Employee>()
            //    .HasOptional(e => e.Department) // Mark Department property optional in Employee entity
            //    .WithRequired(d => d.DepartmentHead); // mark Employee property as required in Department entity. Cannot save Department without Department Head
        }
    }
}
