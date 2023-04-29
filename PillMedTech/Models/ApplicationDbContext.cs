using Microsoft.EntityFrameworkCore;

namespace PillMedTech.Models
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<SickErrand> SickErrands { get; set; }
    public DbSet<Children> Childrens { get; set; }
  }
}
