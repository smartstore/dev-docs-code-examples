using Smartstore.Web.Modelling;

namespace MyOrg.WidgetTutorial.Models
{
    [CustomModelPart]
    public class AdminEditTabModel : ModelBase
    {
        public int EntityId { get; set; }

        [LocalizedDisplay("Plugins.MyOrg.WidgetTutorial.MyTabValue")]
        public string MyTabValue { get; set; }
    }
}