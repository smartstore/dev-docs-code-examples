using Smartstore.Collections;
using Smartstore.Core.Content.Menus;
using Smartstore.Web.Rendering.Builders;

namespace MyOrg.ExportTutorial
{
    public class AdminMenu : AdminMenuProvider
    {
        protected override void BuildMenuCore(TreeNode<MenuItem> modulesNode)
        {
            var myMenuItem = new MenuItem().ToBuilder()
                .ResKey("Plugins.MyOrg.ExportTutorial.MyMenuItem")
                .Icon("gear", "bi")
                .Action("Configure", "ExportTutorialAdmin", new { area = "Admin" })
                .AsItem();

            var menuNode = new TreeNode<MenuItem>(myMenuItem);
            var refNode = modulesNode.Root.SelectNodeById("settings");
            menuNode.InsertAfter(refNode);

            var secondMenuItem = new MenuItem().ToBuilder()
                .ResKey("Plugins.MyOrg.ExportTutorial.MySecondMenuItem")
                .AsItem();
            var subMenuItem = new MenuItem().ToBuilder()
                .ResKey("Plugins.MyOrg.ExportTutorial.MySubMenuItem")
                .Action("Configure", "ExportTutorialAdmin", new { area = "Admin" })
                .AsItem();

            var secondMenuNode = new TreeNode<MenuItem>(secondMenuItem);
            var subMenuNode = new TreeNode<MenuItem>(subMenuItem);

            secondMenuNode.InsertAfter(menuNode);
            secondMenuNode.Append(subMenuNode);
        }
    }
}
