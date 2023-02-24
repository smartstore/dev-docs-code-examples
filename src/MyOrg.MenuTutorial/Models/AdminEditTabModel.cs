using Smartstore.Web.Modelling;

namespace MyOrg.MenuTutorial.Models
{
    [CustomModelPart]
    public class AdminEditTabModel : ModelBase
    {
        public int EntityId { get; set; }

        [LocalizedDisplay("Plugins.MyOrg.MenuTutorial.MyTabValue")]
        public string MyTabValue { get; set; }
    }
}