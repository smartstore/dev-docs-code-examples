using FluentValidation;
using Smartstore.Core.Content.Blocks;
using Smartstore.Web.Modelling;

namespace MyOrg.BlockTutorial.Blocks
{
    [Block("blocktutorial", Icon = "fa fa-eye", FriendlyName = "Block Tutorial")]
    public class BlockTutorialBlockHandler : BlockHandlerBase<BlockTutorialBlock>
    {
        //Doing nothing means standard behaviour.
    }
    public class BlockTutorialBlock : IBlock
    {
        [LocalizedDisplay("Plugins.MyOrg.BlockTutorial.Name")]
        public string Name { get; set; }
    }
    public partial class BlockTutorialBlockValidator : AbstractValidator<BlockTutorialBlock>
    {
        public BlockTutorialBlockValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}