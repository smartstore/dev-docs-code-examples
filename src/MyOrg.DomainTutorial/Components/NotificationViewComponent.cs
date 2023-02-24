using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOrg.DomainTutorial.Extensions;
using MyOrg.DomainTutorial.Models;
using Smartstore;
using Smartstore.Core.Data;
using Smartstore.Core.Identity;
using Smartstore.Web.Components;

namespace MyOrg.DomainTutorial.Components
{
    public class NotificationViewComponent : SmartViewComponent
    {
        private readonly SmartDbContext _db;

        public NotificationViewComponent(SmartDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new NotificationModel
            {
                Author = string.Empty,
                Published = DateTime.MinValue,
                Message = string.Empty
            };

            var notification = await _db.Notifications().OrderByDescending(x => x.Published).FirstOrDefaultAsync();

            if (notification == null)
            {
                return View(model);
            }

            var customer = await _db.Customers.FindByIdAsync(notification.AuthorId, false);

            if (customer == null)
            {
                return View(model);
            }

            model.Author = customer.FirstName.First() + ". " + customer.LastName;
            model.Published = notification.Published;
            model.Message = notification.Message;

            return View(model);
        }
    }
}