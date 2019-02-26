using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PalTracker
{
    public class MySqlTimeEntryRepository : ITimeEntryRepository
    {
        private readonly TimeEntryContext context;
        public MySqlTimeEntryRepository(TimeEntryContext context)
        {
            this.context = context;
        }
        public bool Contains(long id) => context.TimeEntryRecords
            .AsNoTracking()
            .Any(t => t.Id == id);

        public TimeEntry Create(TimeEntry timeEntry)
        {
            var record = timeEntry.ToRecord();
            context.TimeEntryRecords.Add(record);
            context.SaveChanges();
            return Find(record.Id.Value);

        }

        public void Delete(long id)
        {
            var recordToDelete = FindRecord(id);
            context.TimeEntryRecords.Remove(recordToDelete);
            context.SaveChanges();
        }

        public TimeEntry Find(long id) => FindRecord(id).ToEntity();
        public IEnumerable<TimeEntry> List() => context.TimeEntryRecords.AsNoTracking().Select(t=>t.ToEntity());

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            var recordToUpdate = timeEntry.ToRecord();
            recordToUpdate.Id = id;
            context.Update(recordToUpdate);
            context.SaveChanges();
            return Find(id);
        }

        private TimeEntryRecord FindRecord(long id) => context.TimeEntryRecords.AsNoTracking().Single(t => t.Id == id);
    }
}