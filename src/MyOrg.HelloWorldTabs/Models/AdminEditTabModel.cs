using Smartstore.Web.Modelling;

namespace MyOrg.HelloWorld.Models
{
    [CustomModelPart]
    public class AdminEditTabModel : ModelBase
    {
        public int EntityId { get; set; }

        [LocalizedDisplay("Plugins.MyOrg.HelloWorld.MyTabValue")]
        public string MyTabValue { get; set; }
    }
}