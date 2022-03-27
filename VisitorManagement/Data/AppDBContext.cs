using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VisitorManagement.Models;

namespace VisitorManagement.Data
{
    public class AppDBContext : IdentityDbContext<Admin>
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<HealthCheck> HealthCheck { get; set; }
        public DbSet<Temperature> Temperature { get; set; }
        public DbSet<Visitor> Visitor { get; set; }
        public DbSet<EmployeeRegister> EmployeeRegister { get; set; }
        public DbSet<VisittorRegister> VisittorRegister { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
