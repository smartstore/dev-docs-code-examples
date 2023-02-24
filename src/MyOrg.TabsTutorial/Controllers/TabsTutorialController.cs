using Microsoft.AspNetCore.Mvc;
using MyOrg.TabsTutorial.Models;
using MyOrg.TabsTutorial.Settings;
using Smartstore.ComponentModel;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;

namespace MyOrg.TabsTutorial.Controllers
{
    public class TabsTutorialController : PublicController
    {
        [LoadSetting]
        public IActionResult PublicInfo(TabsTutorialSettings settings)
        {
            var model = MiniMapper.Map<TabsTutorialSettings, PublicInfoModel>(settings);

            return View(model);
        }
    }
}