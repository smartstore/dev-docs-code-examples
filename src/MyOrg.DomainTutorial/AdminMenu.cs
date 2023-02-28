using Smartstore.Collections;
using Smartstore.Core.Content.Menus;
using Smartstore.Web.Rendering.Builders;

namespace MyOrg.DomainTutorial
{
    public class AdminMenu : AdminMenuProvider
    {
        protected override void BuildMenuCore(TreeNode<MenuItem> modulesNode)
        {
            var myMenuItem = new MenuItem().ToBuilder()
                .ResKey("Plugins.MyOrg.DomainTutorial.MyMenuItem")
                .Icon("gear", "bi")
                .Action("Configure", "DomainTutorialAdmin", new { area = "Admin" })
                .AsItem();

            var menuNode = new TreeNode<MenuItem>(myMenuItem);
            var refNode = modulesNode.Root.SelectNodeById("settings");
            menuNode.InsertAfter(refNode);

            var secondMenuItem = new MenuItem().ToBuilder()
                .ResKey("Plugins.MyOrg.DomainTutorial.MySecondMenuItem")
                .AsItem();
            var subMenuItem = new MenuItem().ToBuilder()
                .ResKey("Plugins.MyOrg.DomainTutorial.MySubMenuItem")
                .Action("Configure", "DomainTutorialAdmin", new { area = "Admin" })
                .AsItem();

            var secondMenuNode = new TreeNode<MenuItem>(secondMenuItem);
            var subMenuNode = new TreeNode<MenuItem>(subMenuItem);

            secondMenuNode.InsertAfter(menuNode);
            secondMenuNode.Append(subMenuNode);

            // All notifications
            modulesNode.Append(new MenuItem().ToBuilder()
                .ResKey("Plugins.MyOrg.DomainTutorial.Grid.Notification.Title")
                .Icon("chat-left-text", "bi")
                .Id("domain-tutorial-notifications")
                .Action("List", "NotificationAdmin", new { area = "Admin" })
                .AsItem());
        }
    }
}
