using Smartstore.Web.Modelling;

namespace MyOrg.TabsTutorial.Models
{
    [LocalizedDisplay("Plugins.MyOrg.TabsTutorial.")]
    public class ConfigurationModel : ModelBase
    {
        [LocalizedDisplay("*Name")]
        public string Name { get; set; }
    }
}