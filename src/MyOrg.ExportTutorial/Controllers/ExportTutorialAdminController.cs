using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyOrg.ExportTutorial.Models;
using MyOrg.ExportTutorial.Settings;
using Smartstore;
using Smartstore.ComponentModel;
using Smartstore.Core.Data;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;

namespace MyOrg.ExportTutorial.Controllers
{
    public class ExportTutorialAdminController : AdminController
    {
        private readonly SmartDbContext _db;

        public ExportTutorialAdminController(SmartDbContext db)
        {
            _db = db;
        }

        [LoadSetting]
        public IActionResult Configure(ExportTutorialSettings settings)
        {
            var model = MiniMapper.Map<ExportTutorialSettings, ConfigurationModel>(settings);
            return View(model);
        }

        [HttpPost, SaveSetting]
        public IActionResult Configure(ConfigurationModel model, ExportTutorialSettings settings)
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
                MyTabValue = product.GenericAttributes.Get<string>("ExportTutorialMyTabValue")
            };

            ViewData.TemplateInfo.HtmlFieldPrefix = "CustomProperties[MyTab]";
            return View(model);
        }
    }
}