using Smartstore.Core.Configuration;

namespace MyOrg.DomainTutorial.Settings
{
    public class DomainTutorialSettings : ISettings
    {
        public string Name { get; set; } = "John Smith";

        public int NumberOfDaysToDisplayNotification { get; set; } = 8;

        public int NumberOfDaysToKeepNotification { get; set; } = 10;
    }
}