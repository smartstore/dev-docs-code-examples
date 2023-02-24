using Microsoft.AspNetCore.Mvc;
using MyOrg.BlockTutorial.Models;
using MyOrg.BlockTutorial.Settings;
using Smartstore.ComponentModel;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;

namespace MyOrg.BlockTutorial.Controllers
{
    public class BlockTutorialController : PublicController
    {
        [LoadSetting]
        public IActionResult PublicInfo(BlockTutorialSettings settings)
        {
            var model = MiniMapper.Map<BlockTutorialSettings, PublicInfoModel>(settings);

            return View(model);
        }
    }
}