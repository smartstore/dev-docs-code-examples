using Smartstore.Web.Modelling;

namespace MyOrg.WidgetTutorial.Models
{
    [LocalizedDisplay("Plugins.MyOrg.WidgetTutorial.")]
    public class ConfigurationModel : ModelBase
    {
        [LocalizedDisplay("*Name")]
        public string Name { get; set; }
    }
}