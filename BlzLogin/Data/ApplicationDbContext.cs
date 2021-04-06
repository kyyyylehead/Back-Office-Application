using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using BlzLogin.Models;

namespace BlzLogin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Metal> Metals { get; set; }

        /*
        private Job DbMatch(Job fromJob)
        {

            Job retJob = null;
            if (fromJob.Name != null && fromJob.CustomerPartNumber != 0 && (fromJob.Quantity != null || fromJob.Size != null))
            {
                retJob = _context.Jobs("SELECT TOP 1 * FROM Jobs WHERE Name='" + fromJob.Name + "' AND CustomerPartNumber='" + fromJob.CustomerPartNumber + "' ");
                return retJob;
            }
        }
        */
    }
}
