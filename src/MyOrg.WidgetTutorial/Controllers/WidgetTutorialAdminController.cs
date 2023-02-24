using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyOrg.WidgetTutorial.Models;
using MyOrg.WidgetTutorial.Settings;
using Smartstore;
using Smartstore.ComponentModel;
using Smartstore.Core.Data;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;

namespace MyOrg.WidgetTutorial.Controllers
{
    public class WidgetTutorialAdminController : AdminController
    {
        private readonly SmartDbContext _db;
        
        public WidgetTutorialAdminController(SmartDbContext db)
        {
            _db = db;
        }

        [LoadSetting]
        public IActionResult Configure(WidgetTutorialSettings settings)
        {
            var model = MiniMapper.Map<WidgetTutorialSettings, ConfigurationModel>(settings);
            return View(model);
        }

        [HttpPost, SaveSetting]
        public IActionResult Configure(ConfigurationModel model, WidgetTutorialSettings settings)
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
                MyTabValue = product.GenericAttributes.Get<string>("WidgetTutorialMyTabValue")
            };

            ViewData.TemplateInfo.HtmlFieldPrefix = "CustomProperties[MyTab]";
            return View(model);
        }
    }
}