using Smartstore.Web.Modelling;

namespace MyOrg.TabsTutorial.Models
{
    [CustomModelPart]
    public class AdminEditTabModel : ModelBase
    {
        public int EntityId { get; set; }

        [LocalizedDisplay("Plugins.MyOrg.TabsTutorial.MyTabValue")]
        public string MyTabValue { get; set; }
    }
}