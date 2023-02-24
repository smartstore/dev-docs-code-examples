using Microsoft.AspNetCore.Mvc;
using MyOrg.ExportTutorial.Models;
using Smartstore.Web.Components;

namespace MyOrg.ExportTutorial.Components
{
    public class ExportTutorialConfigurationViewComponent : SmartViewComponent
    {
        public IViewComponentResult Invoke(object data)
        {
            var model = data as ProfileConfigurationModel;
            return View(model);
        }
    }
}
