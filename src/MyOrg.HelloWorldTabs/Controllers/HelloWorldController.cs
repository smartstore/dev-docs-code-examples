using Microsoft.AspNetCore.Mvc;
using MyOrg.HelloWorld.Models;
using MyOrg.HelloWorld.Settings;
using Smartstore.ComponentModel;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;

namespace MyOrg.HelloWorld.Controllers
{
    public class HelloWorldController : PublicController
    {
        [LoadSetting]
        public IActionResult PublicInfo(HelloWorldSettings settings)
        {
            var model = MiniMapper.Map<HelloWorldSettings, PublicInfoModel>(settings);

            return View(model);
        }
    }
}