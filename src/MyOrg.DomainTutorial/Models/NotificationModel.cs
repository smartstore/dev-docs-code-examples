using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyOrg.DomainTutorial.Domain;
using MyOrg.DomainTutorial.Settings;
using Smartstore;
using Smartstore.ComponentModel;
using Smartstore.Core.Data;
using Smartstore.Core.Localization;
using Smartstore.Core.Rules.Filters;
using Smartstore.Web.Modelling;

namespace MyOrg.DomainTutorial.Models
{
    [LocalizedDisplay("Plugins.MyOrg.DomainTutorial.Notification.")]
    public class NotificationModel : ModelBase
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        /// <summary>
        /// Name of the Author.
        /// </summary>
        [LocalizedDisplay("*Author")]
        public string Author { get; set; }

        /// <summary>
        /// Time the notification was published.
        /// </summary>
        [LocalizedDisplay("*Published")]
        public DateTime Published { get; set; } = DateTime.Now;

        /// <summary>
        /// Message of the notification.
        /// </summary>
        [LocalizedDisplay("*Message")]
        public string Message { get; set; } = string.Empty;

        [LocalizedDisplay("*Grid.RemovalMessage")]
        public string RemovalMessage { get; set; }

        public bool HasNotification => !string.IsNullOrEmpty(Message);
    }

    public class NotificationModelMapper :
        IMapper<Notification, NotificationModel>,
        IMapper<NotificationModel, Notification>
    {
        private readonly SmartDbContext _db;
        private readonly DomainTutorialSettings _settings;

        public NotificationModelMapper(SmartDbContext db, DomainTutorialSettings settings)
        {
            _db = db;
            _settings = settings;
        }
        public Localizer T { get; set; } = NullLocalizer.Instance;

        public async Task MapAsync(Notification from, NotificationModel to, dynamic parameters = null)
        {
            Guard.NotNull(from, nameof(from));
            Guard.NotNull(to, nameof(to));

            var customer = await _db.Customers.FindByIdAsync(from.AuthorId, false);

            MiniMapper.Map(from, to);

            to.Author = customer.FullName;

            var daysToRemoval = from.Published.AddDays(_settings.NumberOfDaysToKeepNotification).Subtract(DateTime.UtcNow).Days;
            to.RemovalMessage = daysToRemoval <= 0 ?
                T("Plugins.MyOrg.DomainTutorial.Notification.Grid.RemovalMessage.Today") :
                (daysToRemoval == 1 ?
                    T("Plugins.MyOrg.DomainTutorial.Notification.Grid.RemovalMessage.Tomorrow") :
                    T("Plugins.MyOrg.DomainTutorial.Notification.Grid.RemovalMessage.InXDays", daysToRemoval));
        }

        public async Task MapAsync(NotificationModel from, Notification to, dynamic parameters = null)
        {
            Guard.NotNull(from, nameof(from));
            Guard.NotNull(to, nameof(to));

            var customer = await _db.Customers
                .AsNoTracking()
                .Where(x => x.FullName.Equals(from.Author))
                .FirstOrDefaultAsync();

            MiniMapper.Map(from, to);

            to.AuthorId = customer.Id;
        }
    }
}