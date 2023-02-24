using Smartstore.Web.Modelling;

namespace MyOrg.BlockTutorialAdvanced.Models
{
    [CustomModelPart]
    public class AdminEditTabModel : ModelBase
    {
        public int EntityId { get; set; }

        [LocalizedDisplay("Plugins.MyOrg.BlockTutorialAdvanced.MyTabValue")]
        public string MyTabValue { get; set; }
    }
}