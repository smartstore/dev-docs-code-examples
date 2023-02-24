using System.Threading.Tasks;
using MyOrg.TabsTutorial.Models;
using Smartstore;
using Smartstore.Core.Data;
using Smartstore.Events;
using Smartstore.Web.Modelling;
using Smartstore.Web.Rendering.Events;

namespace MyOrg.TabsTutorial
{
    public class Events : IConsumer
    {
        private readonly SmartDbContext _db;

        public Events(SmartDbContext db)
        {
            _db = db;
        }

        public async Task HandleEventAsync(TabStripCreated eventMessage)
        {
            var tabStripName = eventMessage.TabStripName;

            if (tabStripName == "product-edit")
            {
                var entityId = ((TabbableModel)eventMessage.Model).Id;
                // Add in a custom tab
                await eventMessage.TabFactory.AppendAsync(builder => builder
                    .Text("My Tab")
                    .Name("tab-MyTab")
                    .Icon("star", "bi")
                    .LinkHtmlAttributes(new { data_tab_name = "MyTab" })
                    .Action("AdminEditTab", "TabsTutorialAdmin", new { entityId })
                    .Ajax());
            }
        }

        public async Task HandleEventAsync(ModelBoundEvent message)
        {
            if (!message.BoundModel.CustomProperties.ContainsKey("MyTab"))
                return;

            if (message.BoundModel.CustomProperties["MyTab"] is not AdminEditTabModel model)
                return;

            var product = await _db.Products.FindByIdAsync(model.EntityId);
            product.GenericAttributes.Set("TabsTutorialMyTabValue", model.MyTabValue);

            await _db.SaveChangesAsync();
        }
    }
}