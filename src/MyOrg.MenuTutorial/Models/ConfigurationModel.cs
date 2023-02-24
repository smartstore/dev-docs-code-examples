using Smartstore.Web.Modelling;

namespace MyOrg.MenuTutorial.Models
{
    [LocalizedDisplay("Plugins.MyOrg.MenuTutorial.")]
    public class ConfigurationModel : ModelBase
    {
        [LocalizedDisplay("*Name")]
        public string Name { get; set; }
    }
}