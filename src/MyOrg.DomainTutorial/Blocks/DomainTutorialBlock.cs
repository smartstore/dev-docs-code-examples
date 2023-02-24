using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyOrg.DomainTutorial.Components;
using Smartstore.Core.Content.Blocks;
using Smartstore.Core.Widgets;
using Smartstore.Web.Modelling;
using Smartstore.Web.Models.Catalog;

namespace MyOrg.DomainTutorial.Blocks
{
    [Block("domaintutorial", Icon = "fa fa-eye", FriendlyName = "Domain Tutorial")]
    public class DomainTutorialBlockHandler : BlockHandlerBase<DomainTutorialBlock>
    {
        public override async Task<DomainTutorialBlock> LoadAsync(IBlockEntity entity, StoryViewMode viewMode)
        {
            var block = base.Load(entity, viewMode);

            if (viewMode == StoryViewMode.Edit)
            {
                // This only gets called in Edit-Mode
                block.MyLocalVar += " - Running in Edit-Mode";
            }
            else if (viewMode == StoryViewMode.Preview)
            {
                // This only gets called in Preview-Mode
                block.MyLocalVar += " - Running in Preview-Mode";
            }
            else if (viewMode == StoryViewMode.GridEdit)
            {
                // This only gets called in Grid-Edit-Mode
                block.MyLocalVar += " - Running in Grid-Edit-Mode";
            }
            else if (viewMode == StoryViewMode.Public)
            {
                // This only gets called in Public-Mode
                block.MyLocalVar += " - Running in Public-Mode";
            }

            return block;
        }
        protected override Task RenderCoreAsync(IBlockContainer element, IEnumerable<string> templates, IHtmlHelper htmlHelper, TextWriter textWriter)
        {
            if (templates.First() == "Edit")
            {
                return base.RenderCoreAsync(element, templates, htmlHelper, textWriter);
            }
            else
            {
                return RenderByWidgetAsync(element, templates, htmlHelper, textWriter);
            }
        }

        protected override Widget GetWidget(IBlockContainer element, IHtmlHelper htmlHelper, string template)
        {
            var block = (DomainTutorialBlock)element.Block;

            return new ComponentWidget(typeof(DomainTutorialViewComponent), new
            {
                widgetZone = "productdetails_pictures_top",
                model = new ProductDetailsModel { Id = 1 }
            });
        }
    }
    public class DomainTutorialBlock : IBlock
    {
        [LocalizedDisplay("Plugins.MyOrg.DomainTutorial.Name")]
        public string Name { get; set; }
        public string MyLocalVar { get; set; } = "Initialised in Block";
    }
    public partial class DomainTutorialBlockValidator : AbstractValidator<DomainTutorialBlock>
    {
        public DomainTutorialBlockValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}