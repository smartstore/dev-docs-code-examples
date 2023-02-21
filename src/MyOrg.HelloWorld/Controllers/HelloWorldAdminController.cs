using Microsoft.AspNetCore.Mvc;
using Smartstore.ComponentModel;
using Smartstore.Core.Security;
using MyOrg.HelloWorld.Models;
using MyOrg.HelloWorld.Settings;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;

namespace MyOrg.HelloWorld.Controllers
{
    [Area("Admin")]
    public class HelloWorldAdminController : ModuleController
    {
        [LoadSetting, AuthorizeAdmin]
        public IActionResult Configure(HelloWorldSettings settings)
        {
            var model = MiniMapper.Map<HelloWorldSettings, ConfigurationModel>(settings);
            return View(model);
        }

        [HttpPost, SaveSetting, AuthorizeAdmin]
        public IActionResult Configure(ConfigurationModel model, HelloWorldSettings settings)
        {
            if (!ModelState.IsValid)
            {
                return Configure(settings);
            }

            ModelState.Clear();
            MiniMapper.Map(model, settings);

            return RedirectToAction(nameof(Configure));
        }
    }
}