using Microsoft.AspNetCore.Mvc;
using MyOrg.MenuTutorial.Models;
using MyOrg.MenuTutorial.Settings;
using Smartstore.ComponentModel;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;

namespace MyOrg.MenuTutorial.Controllers
{
    public class MenuTutorialController : PublicController
    {
        [LoadSetting]
        public IActionResult PublicInfo(MenuTutorialSettings settings)
        {
            var model = MiniMapper.Map<MenuTutorialSettings, PublicInfoModel>(settings);

            return View(model);
        }
    }
}