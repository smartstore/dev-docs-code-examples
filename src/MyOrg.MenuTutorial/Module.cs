﻿using System.Threading.Tasks;
using MyOrg.MenuTutorial.Components;
using MyOrg.MenuTutorial.Settings;
using Smartstore.Core.Widgets;
using Smartstore.Engine.Modularity;
using Smartstore.Http;

namespace MyOrg.MenuTutorial
{
    internal class Module : ModuleBase, IConfigurable, IActivatableWidget
    {
        public RouteInfo GetConfigurationRoute()
            => new("Configure", "MenuTutorialAdmin", new { area = "Admin" });

        public Widget GetDisplayWidget(string widgetZone, object model, int storeId)
        => new ComponentWidget(typeof(MenuTutorialViewComponent), new { widgetZone, model, storeId });

        public string[] GetWidgetZones()
        {
            return new string[] { "productdetails_pictures_top" };
        }

        public override async Task InstallAsync(ModuleInstallationContext context)
        {
            // Saves the default state of a settings class to the database 
            // without overwriting existing values.
            await TrySaveSettingsAsync<MenuTutorialSettings>();

            // Imports all language resources for the current module from 
            // xml files in "Localization" directory (if any found).
            await ImportLanguageResourcesAsync();

            // VERY IMPORTANT! Don't forget to call.
            await base.InstallAsync(context);
        }

        public override async Task UninstallAsync()
        {
            // Deletes all "MyGreatModuleSettings" properties settings from the database.
            await DeleteSettingsAsync<MenuTutorialSettings>();

            // Deletes all language resource for the current module 
            // if "ResourceRootKey" is module.json is not empty.
            await DeleteLanguageResourcesAsync();

            // VERY IMPORTANT! Don't forget to call.
            await base.UninstallAsync();
        }
    }
}