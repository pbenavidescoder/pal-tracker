using Microsoft.EntityFrameworkCore;

namespace PalTracker
{
    public class TimeEntryContext : DbContext
    {

        private readonly DbContextOptions options;
        public DbSet<TimeEntryRecord> TimeEntryRecords { get; set; }
        public TimeEntryContext(DbContextOptions options) : base(options)
        {

            this.options = options;
        }

    }
}