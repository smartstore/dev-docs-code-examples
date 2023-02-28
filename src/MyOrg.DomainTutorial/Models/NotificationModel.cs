using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyOrg.DomainTutorial.Domain;
using Smartstore;
using Smartstore.ComponentModel;
using Smartstore.Core.Data;
using Smartstore.Core.Rules.Filters;
using Smartstore.Web.Modelling;

namespace MyOrg.DomainTutorial.Models
{
    public class NotificationModel : ModelBase
    {
        /// <summary>
        /// Name of the Author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Time the notification was published.
        /// </summary>
        public DateTime Published { get; set; }

        /// <summary>
        /// Message of the notification.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        public bool HasNotification => !string.IsNullOrEmpty(Message);
    }

    public class NotificationModelMapper :
        IMapper<Notification, NotificationModel>,
        IMapper<NotificationModel, Notification>
    {
        private readonly SmartDbContext _db;

        public NotificationModelMapper(SmartDbContext db)
        {
            _db = db;
        }

        public async Task MapAsync(Notification from, NotificationModel to, dynamic parameters = null)
        {
            Guard.NotNull(from, nameof(from));
            Guard.NotNull(to, nameof(to));

            var customer = await _db.Customers.FindAsync(from.Id, false);

            to.Author = customer.FullName;
            to.Published = from.Published;
            to.Message = from.Message;
        }

        public async Task MapAsync(NotificationModel from, Notification to, dynamic parameters = null)
        {
            Guard.NotNull(from, nameof(from));
            Guard.NotNull(to, nameof(to));

            var customer = await _db.Customers
                .AsNoTracking()
                .Where(x => x.FullName.Equals(from.Author))
                .FirstOrDefaultAsync();

            to.AuthorId = customer.Id;
            to.Published = from.Published;
            to.Message = from.Message;
        }
    }
}
