using Smartstore.Web.Modelling;

namespace MyOrg.HelloWorld.Models
{
    [LocalizedDisplay("Plugins.MyOrg.HelloWorld.")]
    public class ConfigurationModel : ModelBase
    {
        [LocalizedDisplay("*Name")]
        public string Name { get; set; }
    }
}