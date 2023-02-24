using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyOrg.DomainTutorial.Components;
using MyOrg.DomainTutorial.Providers;
using MyOrg.DomainTutorial.Settings;
using MyOrg.DomainTutorial.Tasks;
using Smartstore;
using Smartstore.Core.Data;
using Smartstore.Core.DataExchange.Export;
using Smartstore.Core.Localization;
using Smartstore.Core.Widgets;
using Smartstore.Engine.Modularity;
using Smartstore.Http;
using Smartstore.Scheduling;

namespace MyOrg.DomainTutorial
{
    public class Module : ModuleBase, IConfigurable, IActivatableWidget
    {
        private readonly SmartDbContext _db;
        private readonly ITaskStore _taskStore;
        private readonly IExportProfileService _exportProfileService;

        public Module(SmartDbContext db, IExportProfileService exportProfileService, ITaskStore taskStore)
        {
            _db = db;
            _exportProfileService = exportProfileService;
            _taskStore = taskStore;
        }

        public Localizer T { get; set; } = NullLocalizer.Instance;

        public RouteInfo GetConfigurationRoute()
            => new("Configure", "DomainTutorialAdmin", new { area = "Admin" });

        public Widget GetDisplayWidget(string widgetZone, object model, int storeId)
        => new ComponentWidget(typeof(DomainTutorialViewComponent), new { widgetZone, model, storeId });

        public string[] GetWidgetZones()
        {
            return new string[] { "productdetails_pictures_top" };
        }

        public override async Task InstallAsync(ModuleInstallationContext context)
        {
            // Saves the default state of a settings class to the database 
            // without overwriting existing values.
            await TrySaveSettingsAsync<DomainTutorialSettings>();

            // Imports all language resources for the current module from 
            // xml files in "Localization" directory (if any found).
            await ImportLanguageResourcesAsync();

            // Install Tasks.
            await _taskStore.GetOrAddTaskAsync<CleanupDomainTutorialTask>(x =>
            {
                x.Name = T("Plugins.Smartstore.DomainTutorial.TaskName");
                x.CronExpression = "0 5 * * *";
                x.Enabled = true;
            });

            // VERY IMPORTANT! Don't forget to call.
            await base.InstallAsync(context);
        }

        public override async Task UninstallAsync()
        {
            // Delete existing export profiles.
            var profiles = await _db.ExportProfiles
                .Include(x => x.Deployments)
                .Include(x => x.Task)
                .Where(x => x.ProviderSystemName == DomainTutorialCsvExportProvider.SystemName)
                .ToListAsync();

            await profiles.EachAsync(x => _exportProfileService.DeleteExportProfileAsync(x, true));

            // Deletes all "MyGreatModuleSettings" properties settings from the database.
            await DeleteSettingsAsync<DomainTutorialSettings>();

            // Delete Tasks.
            await _taskStore.TryDeleteTaskAsync<CleanupDomainTutorialTask>();

            // Deletes all language resource for the current module 
            // if "ResourceRootKey" is module.json is not empty.
            await DeleteLanguageResourcesAsync();

            // VERY IMPORTANT! Don't forget to call.
            await base.UninstallAsync();
        }
    }
}