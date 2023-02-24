using Smartstore.Web.Modelling;

namespace MyOrg.DomainTutorial.Models
{
    [CustomModelPart]
    public class AdminEditTabModel : ModelBase
    {
        public int EntityId { get; set; }

        [LocalizedDisplay("Plugins.MyOrg.DomainTutorial.MyTabValue")]
        public string MyTabValue { get; set; }
    }
}