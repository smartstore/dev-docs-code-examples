using Smartstore.Web.Modelling;

namespace MyOrg.BlockTutorialAdvanced.Models
{
    [LocalizedDisplay("Plugins.MyOrg.BlockTutorialAdvanced.")]
    public class ConfigurationModel : ModelBase
    {
        [LocalizedDisplay("*Name")]
        public string Name { get; set; }
    }
}