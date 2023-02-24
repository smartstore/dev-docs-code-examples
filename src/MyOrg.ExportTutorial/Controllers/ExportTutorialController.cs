using Microsoft.AspNetCore.Mvc;
using MyOrg.ExportTutorial.Models;
using MyOrg.ExportTutorial.Settings;
using Smartstore.ComponentModel;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;

namespace MyOrg.ExportTutorial.Controllers
{
    public class ExportTutorialController : PublicController
    {
        [LoadSetting]
        public IActionResult PublicInfo(ExportTutorialSettings settings)
        {
            var model = MiniMapper.Map<ExportTutorialSettings, PublicInfoModel>(settings);

            return View(model);
        }
    }
}