using Steeltoe.Common.HealthChecks;
using System.Linq;
using static Steeltoe.Common.HealthChecks.HealthStatus;


namespace PalTracker {

    public class TimeEntryHealthContributor : IHealthContributor
    {
        private readonly ITimeEntryRepository repo;
        public const int MaxTimeEntries = 5;
        public TimeEntryHealthContributor(ITimeEntryRepository repo)
        {
            this.repo=repo;
        }
        public string Id => "timeEntry";

        public HealthCheckResult Health()
        {
            var status = repo.List().Count() >= MaxTimeEntries ? DOWN: UP; 
            var health = new HealthCheckResult {Status = status};
            health.Details.Add("threshold", MaxTimeEntries);
            health.Details.Add("count", repo.List().Count());
            health.Details.Add("status", status.ToString());

            return health;
        }
    }

}