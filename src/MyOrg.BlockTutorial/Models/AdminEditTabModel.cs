using Smartstore.Web.Modelling;

namespace MyOrg.BlockTutorial.Models
{
    [CustomModelPart]
    public class AdminEditTabModel : ModelBase
    {
        public int EntityId { get; set; }

        [LocalizedDisplay("Plugins.MyOrg.BlockTutorial.MyTabValue")]
        public string MyTabValue { get; set; }
    }
}