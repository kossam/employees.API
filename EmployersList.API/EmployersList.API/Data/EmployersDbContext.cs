using EmployersList.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployersList.API.Data
{
    public class EmployersDbContext : DbContext
    {
        public EmployersDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
