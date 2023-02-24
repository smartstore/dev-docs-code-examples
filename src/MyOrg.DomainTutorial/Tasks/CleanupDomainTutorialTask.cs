using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyOrg.DomainTutorial.Extensions;
using MyOrg.DomainTutorial.Settings;
using Smartstore.Core.Data;
using Smartstore.Scheduling;

namespace MyOrg.DomainTutorial.Tasks
{
    internal class CleanupDomainTutorialTask : ITask
    {
        private readonly DomainTutorialSettings _settings;
        private readonly SmartDbContext _db;

        public CleanupDomainTutorialTask(DomainTutorialSettings settings, SmartDbContext db)
        {
            _settings = settings;
            _db = db;
        }

        public async Task Run(TaskExecutionContext ctx, CancellationToken cancelToken = default)
        {
            var date = DateTime.UtcNow.AddDays(-_settings.DaysToKeepNotification);

            await _db.Notifications()
                .Where(x => x.Published < date)
                .ExecuteDeleteAsync();

            await _db.SaveChangesAsync();
        }
    }
}