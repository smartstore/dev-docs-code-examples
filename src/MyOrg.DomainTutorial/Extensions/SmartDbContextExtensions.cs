using Microsoft.EntityFrameworkCore;
using MyOrg.DomainTutorial.Domain;
using Smartstore.Core.Data;

namespace MyOrg.DomainTutorial.Extensions
{
    public static class SmartDbContextExtensions
    {
        public static DbSet<Notification> Notifications(this SmartDbContext db)
            => db.Set<Notification>();
    }
}
