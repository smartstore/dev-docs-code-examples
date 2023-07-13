using System.Threading.Tasks;
using Smartstore.Engine.Modularity;

namespace Smartstore.Themes.MyTheme
{
    internal class Module : ModuleBase
    {
        public override async Task InstallAsync(ModuleInstallationContext context)
        {
            await ImportLanguageResourcesAsync();
            await base.InstallAsync(context);
        }

        public override async Task UninstallAsync()
        {
            await DeleteLanguageResourcesAsync();
            await base.UninstallAsync();
        }
    }
}
