using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOrg.DomainTutorial.Domain;
using MyOrg.DomainTutorial.Extensions;
using MyOrg.DomainTutorial.Models;
using MyOrg.DomainTutorial.Settings;
using Smartstore.ComponentModel;
using Smartstore.Core.Data;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;

namespace MyOrg.DomainTutorial.Controllers
{
    public class DomainTutorialController : PublicController
    {
        private readonly SmartDbContext _db;

        public DomainTutorialController(SmartDbContext db)
        {
            _db = db;
        }
        [LoadSetting]
        public IActionResult PublicInfo(DomainTutorialSettings settings)
        {
            var model = MiniMapper.Map<DomainTutorialSettings, PublicInfoModel>(settings);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GenerateNotification(IFormCollection form)
        {
            var hasMessage = form.TryGetValue("NotificationMessage", out var notificationString);
            var wasSuccessful = false;

            if (hasMessage)
            {
                _db.Notifications().Add(new Notification
                {
                    AuthorId = Services.WorkContext.CurrentCustomer.Id,
                    Message = notificationString,
                    Published = DateTime.UtcNow
                });
                wasSuccessful = _db.SaveChanges() == 1;
            }

            var vcHTML = await InvokeComponentAsync("Notification", null, null);

            return Json(new
            {
                success = wasSuccessful,
                html = vcHTML
            });
        }
    }
}