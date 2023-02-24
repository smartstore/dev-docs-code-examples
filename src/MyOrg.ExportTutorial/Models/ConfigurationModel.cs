using Smartstore.Web.Modelling;

namespace MyOrg.ExportTutorial.Models
{
    [LocalizedDisplay("Plugins.MyOrg.ExportTutorial.")]
    public class ConfigurationModel : ModelBase
    {
        [LocalizedDisplay("*Name")]
        public string Name { get; set; }
    }
}