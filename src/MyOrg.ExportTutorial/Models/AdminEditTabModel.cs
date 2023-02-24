using Smartstore.Web.Modelling;

namespace MyOrg.ExportTutorial.Models
{
    [CustomModelPart]
    public class AdminEditTabModel : ModelBase
    {
        public int EntityId { get; set; }

        [LocalizedDisplay("Plugins.MyOrg.ExportTutorial.MyTabValue")]
        public string MyTabValue { get; set; }
    }
}