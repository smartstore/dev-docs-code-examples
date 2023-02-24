using System.Threading.Tasks;
using MyOrg.TabsTutorial.Settings;
using Smartstore.Engine.Modularity;
using Smartstore.Http;

namespace MyOrg.TabsTutorial
{
    internal class Module : ModuleBase, IConfigurable
    {
        public RouteInfo GetConfigurationRoute()
            => new("Configure", "TabsTutorialAdmin", new { area = "Admin" });

        public override async Task InstallAsync(ModuleInstallationContext context)
        {
            // Saves the default state of a settings class to the database 
            // without overwriting existing values.
            await TrySaveSettingsAsync<TabsTutorialSettings>();

            // Imports all language resources for the current module from 
            // xml files in "Localization" directory (if any found).
            await ImportLanguageResourcesAsync();

            // VERY IMPORTANT! Don't forget to call.
            await base.InstallAsync(context);
        }

        public override async Task UninstallAsync()
        {
            // Deletes all "MyGreatModuleSettings" properties settings from the database.
            await DeleteSettingsAsync<TabsTutorialSettings>();

            // Deletes all language resource for the current module 
            // if "ResourceRootKey" is module.json is not empty.
            await DeleteLanguageResourcesAsync();

            // VERY IMPORTANT! Don't forget to call.
            await base.UninstallAsync();
        }
    }
}