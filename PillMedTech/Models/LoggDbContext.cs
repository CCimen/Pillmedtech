using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PillMedTech.Models.POCO;

namespace PillMedTech.Models
{
    /* Loggning: 6.3: Förvara loggningarna i en annan databas*/
    /* Olika databaser som hanterar olika typer av data (ex. Loggar)*/
    public class LoggDbContext : DbContext
    {
        public LoggDbContext(DbContextOptions<LoggDbContext> options) : base(options) { }
        public DbSet<Logger> Loggers { get; set; }
    }
}