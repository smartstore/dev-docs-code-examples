using Smartstore.Web.Modelling;

namespace MyOrg.DomainTutorial.Models
{
    [LocalizedDisplay("Plugins.MyOrg.DomainTutorial.")]
    public class ConfigurationModel : ModelBase
    {
        [LocalizedDisplay("*Name")]
        public string Name { get; set; }

        [LocalizedDisplay("*NumberOfDaysToDisplayNotification")]
        public int NumberOfDaysToDisplayNotification { get; set; } = 3;

        [LocalizedDisplay("*NumberOfDaysToKeepNotification")]
        public int NumberOfDaysToKeepNotification { get; set; } = 7;
    }
}