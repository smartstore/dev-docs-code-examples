using System;
using Smartstore.Web.Modelling;

namespace MyOrg.DomainTutorial.Models
{
    public class NotificationListModel : EntityModelBase
    {
        public DateTime SearchPublished { get; set; }

        public string SearchMessage { get; set; }
    }
}