using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOrg.DomainTutorial.Domain;
using MyOrg.DomainTutorial.Extensions;
using MyOrg.DomainTutorial.Models;
using Smartstore;
using Smartstore.ComponentModel;
using Smartstore.Core.Data;
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
                .AsNoTracking()
                .Where(x => x.Message.Length > 0);

            /*if (model.SearchMessage.HasValue())
            {
                query = query.ApplySearchFilterFor(x => x.Message, model.SearchMessage);
            }

            if (model.SearchPublished != null)
            {
                query = query.Where(x => x.Published == model.SearchPublished);
            }*/

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
    }
}
