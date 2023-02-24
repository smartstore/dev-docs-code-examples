using Smartstore.Web.Modelling;

namespace MyOrg.BlockTutorial.Models
{
    [LocalizedDisplay("Plugins.MyOrg.BlockTutorial.")]
    public class ConfigurationModel : ModelBase
    {
        [LocalizedDisplay("*Name")]
        public string Name { get; set; }
    }
}