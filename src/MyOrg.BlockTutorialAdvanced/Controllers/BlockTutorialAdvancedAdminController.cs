using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyOrg.BlockTutorialAdvanced.Models;
using MyOrg.BlockTutorialAdvanced.Settings;
using Smartstore;
using Smartstore.ComponentModel;
using Smartstore.Core.Data;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;

namespace MyOrg.BlockTutorialAdvanced.Controllers
{
    public class BlockTutorialAdvancedAdminController : AdminController
    {
        private readonly SmartDbContext _db;

        public BlockTutorialAdvancedAdminController(SmartDbContext db)
        {
            _db = db;
        }

        [LoadSetting]
        public IActionResult Configure(BlockTutorialAdvancedSettings settings)
        {
            var model = MiniMapper.Map<BlockTutorialAdvancedSettings, ConfigurationModel>(settings);
            return View(model);
        }

        [HttpPost, SaveSetting]
        public IActionResult Configure(ConfigurationModel model, BlockTutorialAdvancedSettings settings)
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
                MyTabValue = product.GenericAttributes.Get<string>("BlockTutorialAdvancedMyTabValue")
            };

            ViewData.TemplateInfo.HtmlFieldPrefix = "CustomProperties[MyTab]";
            return View(model);
        }
    }
}