using Microsoft.AspNetCore.Mvc;
using MyOrg.WidgetTutorial.Models;
using MyOrg.WidgetTutorial.Settings;
using Smartstore.ComponentModel;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;

namespace MyOrg.WidgetTutorial.Controllers
{
    public class WidgetTutorialController : PublicController
    {
        [LoadSetting]
        public IActionResult PublicInfo(WidgetTutorialSettings settings)
        {
            var model = MiniMapper.Map<WidgetTutorialSettings, PublicInfoModel>(settings);

            return View(model);
        }
    }
}