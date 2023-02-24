using Microsoft.AspNetCore.Mvc;
using MyOrg.BlockTutorialAdvanced.Models;
using MyOrg.BlockTutorialAdvanced.Settings;
using Smartstore.ComponentModel;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;

namespace MyOrg.BlockTutorialAdvanced.Controllers
{
    public class BlockTutorialAdvancedController : PublicController
    {
        [LoadSetting]
        public IActionResult PublicInfo(BlockTutorialAdvancedSettings settings)
        {
            var model = MiniMapper.Map<BlockTutorialAdvancedSettings, PublicInfoModel>(settings);

            return View(model);
        }
    }
}