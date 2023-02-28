using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyOrg.ExportTutorial.Components;
using MyOrg.ExportTutorial.Providers;
using MyOrg.ExportTutorial.Settings;
using Smartstore;
using Smartstore.Core.Data;
using Smartstore.Core.DataExchange.Export;
using Smartstore.Core.Widgets;
using Smartstore.Engine.Modularity;
using Smartstore.Http;

namespace MyOrg.ExportTutorial
{
    public class Module : ModuleBase, IConfigurable, IActivatableWidget
    {
        private readonly SmartDbContext _db;
        private readonly IExportProfileService _exportProfileService;

        public Module(SmartDbContext db, IExportProfileService exportProfileService)
        {
            _db = db;
            _exportProfileService = exportProfileService;
        }

        public RouteInfo GetConfigurationRoute()
            => new("Configure", "ExportTutorialAdmin", new { area = "Admin" });

        public Widget GetDisplayWidget(string widgetZone, object model, int storeId)
            => new ComponentWidget(typeof(ExportTutorialViewComponent), new { widgetZone, model, storeId });

        public string[] GetWidgetZones()
            => new string[] { "productdetails_pictures_top" };

        public override async Task InstallAsync(ModuleInstallationContext context)
        {
            // Saves the default state of a settings class to the database 
            // without overwriting existing values.
            await TrySaveSettingsAsync<ExportTutorialSettings>();

            // Imports all language resources for the current module from 
            // xml files in "Localization" directory (if any found).
            await ImportLanguageResourcesAsync();

            // VERY IMPORTANT! Don't forget to call.
            await base.InstallAsync(context);
        }

        public override async Task UninstallAsync()
        {
            // Delete existing export profiles.
            var profiles = await _db.ExportProfiles
                .Include(x => x.Deployments)
                .Include(x => x.Task)
                .Where(x => x.ProviderSystemName == ExportTutorialCsvExportProvider.SystemName || x.ProviderSystemName == ExportTutorialXmlExportProvider.SystemName)
                .ToListAsync();

            await profiles.EachAsync(x => _exportProfileService.DeleteExportProfileAsync(x, true));

            // Deletes all "MyGreatModuleSettings" properties settings from the database.
            await DeleteSettingsAsync<ExportTutorialSettings>();

            // Deletes all language resource for the current module 
            // if "ResourceRootKey" is module.json is not empty.
            await DeleteLanguageResourcesAsync();

            // VERY IMPORTANT! Don't forget to call.
            await base.UninstallAsync();
        }
    }
}