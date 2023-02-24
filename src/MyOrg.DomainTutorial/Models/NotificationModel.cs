using System;
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
}
