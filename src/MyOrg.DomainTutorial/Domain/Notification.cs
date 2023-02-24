using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Smartstore.Domain;

namespace MyOrg.DomainTutorial.Domain
{
    [Table("Notification")]
    [Index(nameof(AuthorId), Name = "IX_Notification_AuthorId")]
    [Index(nameof(Published), Name = "IX_Notification_Published")]
    public class Notification : BaseEntity
    {
        /// <summary>
        /// Customer id of the Author.
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Time the notification was published.
        /// </summary>
        public DateTime Published { get; set; }

        /// <summary>
        /// Message of the notification.
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}