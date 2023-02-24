using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyOrg.BlockTutorialAdvanced.Components;
using Smartstore.Core.Content.Blocks;
using Smartstore.Core.Widgets;
using Smartstore.Web.Modelling;
using Smartstore.Web.Models.Catalog;

namespace MyOrg.BlockTutorialAdvanced.Blocks
{
    [Block("blocktutorialadvanced", Icon = "fa fa-eye", FriendlyName = "Advanced Block Tutorial")]
    public class BlockTutorialAdvancedBlockHandler : BlockHandlerBase<BlockTutorialAdvancedBlock>
    {
        public override async Task<BlockTutorialAdvancedBlock> LoadAsync(IBlockEntity entity, StoryViewMode viewMode)
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
            var block = (BlockTutorialAdvancedBlock)element.Block;

            return new ComponentWidget(typeof(BlockTutorialAdvancedViewComponent), new
            {
                widgetZone = "productdetails_pictures_top",
                model = new ProductDetailsModel { Id = 1 }
            });
        }
    }
    public class BlockTutorialAdvancedBlock : IBlock
    {
        [LocalizedDisplay("Plugins.MyOrg.BlockTutorialAdvanced.Name")]
        public string Name { get; set; }
        public string MyLocalVar { get; set; } = "Initialised in Block";
    }
    public partial class BlockTutorialAdvancedBlockValidator : AbstractValidator<BlockTutorialAdvancedBlock>
    {
        public BlockTutorialAdvancedBlockValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}