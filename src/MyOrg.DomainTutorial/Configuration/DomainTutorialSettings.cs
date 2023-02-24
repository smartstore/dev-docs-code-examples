using Smartstore.Core.Configuration;

namespace MyOrg.DomainTutorial.Settings
{
    public class DomainTutorialSettings : ISettings
    {
        public string Name { get; set; } = "John Smith";

        public int DaysToShowNotification { get; set; } = 8;

        public int DaysToKeepNotification { get; set; } = 10;
    }
}