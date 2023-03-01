using Smartstore.Web.Modelling;

namespace MyOrg.DomainTutorial.Models
{
    [LocalizedDisplay("Plugins.MyOrg.DomainTutorial.Notification.Grid.")]
    public class NotificationListModel : EntityModelBase
    {
        [LocalizedDisplay("*Search.Message")]
        public string SearchMessage { get; set; }
    }
}