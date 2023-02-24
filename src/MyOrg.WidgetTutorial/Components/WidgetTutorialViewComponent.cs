using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyOrg.WidgetTutorial.Models;
using Smartstore;
using Smartstore.Core.Data;
using Smartstore.Web.Components;
using Smartstore.Web.Models.Catalog;

namespace MyOrg.WidgetTutorial.Components
{
    public class WidgetTutorialViewComponent : SmartViewComponent
    {
        private readonly SmartDbContext _db;

        public WidgetTutorialViewComponent(SmartDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object model)
        {
            if (widgetZone != "productdetails_pictures_top")
            {
                return Empty();
            }

            if (model.GetType() != typeof(ProductDetailsModel))
            {
                return Empty();
            }

            var productModel = (ProductDetailsModel)model;
            var product = await _db.Products.FindByIdAsync(productModel.Id);
            var attrValue = product.GenericAttributes.Get<string>("WidgetTutorialMyTabValue");

            var viewComponentModel = new ViewComponentModel
            {
                MyTabValue = attrValue
            };

            return View(viewComponentModel);
        }
    }
}