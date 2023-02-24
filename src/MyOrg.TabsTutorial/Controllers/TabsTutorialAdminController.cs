using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyOrg.TabsTutorial.Models;
using MyOrg.TabsTutorial.Settings;
using Smartstore;
using Smartstore.ComponentModel;
using Smartstore.Core.Data;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;

namespace MyOrg.TabsTutorial.Controllers
{
    public class TabsTutorialAdminController : AdminController
    {
        private readonly SmartDbContext _db;
        
        public TabsTutorialAdminController(SmartDbContext db)
        {
            _db = db;
        }

        [LoadSetting]
        public IActionResult Configure(TabsTutorialSettings settings)
        {
            var model = MiniMapper.Map<TabsTutorialSettings, ConfigurationModel>(settings);
            return View(model);
        }

        [HttpPost, SaveSetting]
        public IActionResult Configure(ConfigurationModel model, TabsTutorialSettings settings)
        {
            if (!ModelState.IsValid)
            {
                return Configure(settings);
            }

            ModelState.Clear();
            MiniMapper.Map(model, settings);

            return RedirectToAction(nameof(Configure));
        }

        public async Task<IActionResult> AdminEditTab(int entityId)
        {
            var product = await _db.Products.FindByIdAsync(entityId, false);

            var model = new AdminEditTabModel
            {
                EntityId = entityId,
                MyTabValue = product.GenericAttributes.Get<string>("TabsTutorialMyTabValue")
            };

            ViewData.TemplateInfo.HtmlFieldPrefix = "CustomProperties[MyTab]";
            return View(model);
        }
    }
}