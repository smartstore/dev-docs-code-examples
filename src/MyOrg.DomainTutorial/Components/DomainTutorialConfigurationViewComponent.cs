using Microsoft.AspNetCore.Mvc;
using MyOrg.DomainTutorial.Models;
using Smartstore.Web.Components;

namespace MyOrg.DomainTutorial.Components
{
    public class DomainTutorialConfigurationViewComponent : SmartViewComponent
    {
        public IViewComponentResult Invoke(object data)
        {
            var model = data as ProfileConfigurationModel;
            return View(model);
        }
    }
}
