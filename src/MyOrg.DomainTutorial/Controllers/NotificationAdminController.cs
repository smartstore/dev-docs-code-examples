using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOrg.DomainTutorial.Domain;
using MyOrg.DomainTutorial.Extensions;
using MyOrg.DomainTutorial.Models;
using Smartstore;
using Smartstore.Admin.Models.Catalog;
using Smartstore.ComponentModel;
using Smartstore.Core.Data;
using Smartstore.Core.Rules.Filters;
using Smartstore.Web.Controllers;
using Smartstore.Web.Models.DataGrid;

namespace MyOrg.DomainTutorial.Controllers
{
    public class NotificationAdminController : AdminController
    {
        private readonly SmartDbContext _db;

        public NotificationAdminController(SmartDbContext db)
        {
            _db = db;
        }

        public IActionResult List()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GridRead(GridCommand command, NotificationListModel model)
        {
            var query = _db.Notifications()
                .AsNoTracking();

            if (model.SearchMessage.HasValue())
            {
                query = query.ApplySearchFilterFor(x => x.Message, model.SearchMessage);
            }

            var notifications = await query
                .ApplyGridCommand(command)
                .ToPagedList(command)
                .LoadAsync();

            var mapper = MapperFactory.GetMapper<Notification, NotificationModel>();
            var notificationModels = await notifications
                .SelectAwait(async x => await mapper.MapAsync(x))
                .ToListAsync();

            var gridModel = new GridModel<NotificationModel>
            {
                Rows = notificationModels,
                Total = await notifications.GetTotalCountAsync()
            };

            return Json(gridModel);
        }

        [HttpPost]
        public async Task<IActionResult> GridInsert(NotificationModel model)
        {
            var customer = await _db.Customers.AsNoTracking().Where(x => x.FullName.ToLower().Equals(model.Author.ToLower())).FirstOrDefaultAsync();

            if (customer != null)
            {
                var notification = new Notification
                {
                    AuthorId = customer.Id,
                    Published = model.Published,
                    Message = model.Message,
                };

                try
                {
                    _db.Notifications().Add(notification);
                    await _db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    NotifyError(ex.GetInnerMessage());
                }
            }
            else
            {
                NotifyError(T("Plugins.MyOrg.DomainTutorial.Notification.Grid.UserNotFound"));
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> GridUpdate(NotificationModel model)
        {
            var notification = await _db.Notifications().FindByIdAsync(model.Id);

            await MapperFactory.GetMapper<NotificationModel, Notification>().MapAsync(model, notification);

            try
            {
                await _db.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                NotifyError(ex.GetInnerMessage());
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GridDelete(GridSelection selection)
        {
            var success = false;
            var numDeleted = 0;
            var ids = selection.GetEntityIds();

            if (ids.Any())
            {
                var notifications = await _db.Notifications().GetManyAsync(ids, true);

                _db.Notifications().RemoveRange(notifications);

                numDeleted = await _db.SaveChangesAsync();
                success = true;
            }

            return Json(new { Success = success, Count = numDeleted });
        }
    }
}