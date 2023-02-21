using Smartstore.Core.Configuration;

namespace MyOrg.HelloWorld.Settings
{
    public class HelloWorldSettings : ISettings
    {
        public string Name { get; set; } = "John Smith";
    }
}