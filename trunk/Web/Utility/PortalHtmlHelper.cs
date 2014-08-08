using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Disappearwind.PortalSolution.PortalWeb.Business;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Utility
{
    /// <summary>
    /// Extend MVC HtmlHelper to support more html output in portalweb
    /// </summary>
    public static class PortalHtmlHelper
    {
        /// <summary>
        /// The feature list
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selectedNode"></param>
        /// <returns></returns>
        public static string FeatureList(this HtmlHelper helper, string selectedNode)
        {
            var sb = new StringBuilder();

            // Create opening unordered list tag
            sb.Append("<ul class='menu'>");

            // Render each top level node
            var topLevelNodes = SiteMap.RootNode.ChildNodes;
            foreach (SiteMapNode node in topLevelNodes)
            {
                if (SiteMap.CurrentNode == node || selectedNode == node.ResourceKey)
                {
                    sb.AppendLine("<li class='selectedMenuItem'>");
                }
                else
                {
                    sb.AppendLine("<li>");
                }

                sb.AppendFormat("<a href='{0}'>{1}</a>", node.Url, helper.Encode(node.Title));
                sb.AppendLine("</li>");
            }

            // Close unordered list tag
            sb.Append("</ul>");

            return sb.ToString();
        }
        /// <summary>
        /// Portal menu type
        /// </summary>
        /// <returns></returns>
        public static SelectList PortalMenuTypeList()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            SelectListItem docItem = new SelectListItem();
            docItem.Text = MenuType.DocumentMenu.ToString();
            docItem.Value = ((int)MenuType.DocumentMenu).ToString();
            itemList.Add(docItem);
            SelectListItem nodeItem = new SelectListItem();
            nodeItem.Text = MenuType.NodeMenu.ToString();
            nodeItem.Value = ((int)MenuType.NodeMenu).ToString();
            itemList.Add(nodeItem);
            SelectList list = new SelectList(itemList, "Value", "Text");
            return list;
        }
        /// <summary>
        /// Image in PortalDocument
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="menuKeyword"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string DocumentImage(this HtmlHelper helper, string menuKeyword, int width, int height)
        {
            var sb = new StringBuilder();
            PortalDocument doc = CommonUtility.GetDocument(menuKeyword).SingleOrDefault();
            if (doc != null)
            {
                sb.AppendFormat("<img src='{0}' width='{1}' height='{2}' title={3}/>", doc.ImageUrl, width, height, doc.DisplayName);
            }
            return sb.ToString();
        }

        #region TreeView
        /// <summary>
        /// TreeView of PortalMenu
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="state"></param>
        /// <param name="portalId"></param>
        /// <returns></returns>
        public static string TreeViewPortalMenu(this HtmlHelper helper, bool? state, int portalId)
        {
            StringBuilder sb = new StringBuilder();
            PortalMenuBusiness pmBusiness = new PortalMenuBusiness();
            var menuList = pmBusiness.GetPortalMenuList(portalId);
            if (menuList != null && state != null)
            {
                menuList = menuList.Where(p => p.State == (bool)state).ToList();
            }
            if (menuList != null && menuList.Count > 0)
            {
                sb.AppendLine("<ul class='ulTree'>");
                var rootMenu = menuList.Where(p => p.ParentMenu.Value == 0).ToList(); //the root menu's parent id is 0
                foreach (var item in rootMenu)
                {
                    sb.AppendFormat("<li class='liTreeNode'><a href='?id={0}'>{1}</a></li>\n", item.Id, item.Name);
                    TreeViewAppendChild(sb, item.Id, menuList);
                }
                sb.AppendLine("</ul>");
            }
            return sb.ToString();
        }
        /// <summary>
        /// Append child to TreeView
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="parentId"></param>
        /// <param name="menuList"></param>
        /// <returns></returns>
        public static string TreeViewAppendChild(StringBuilder sb, int parentId, List<Models.PortalMenu> menuList)
        {
            sb.AppendLine("<ul>");
            var childList = menuList.Where(p => p.ParentMenu.Value == parentId).ToList();
            foreach (var item in childList)
            {
                if (item.Type == (int)MenuType.NodeMenu)
                {
                    sb.AppendFormat("<li class='liTreeNode'><a href='?id={0}'>{1}</a></li>\n", item.Id, item.Name);
                    TreeViewAppendChild(sb, item.Id, menuList);
                }
                else
                {
                    sb.AppendFormat("<li class='liTreeLeaf'><a href='?id={0}'>{1}</a></li>\n", item.Id, item.Name);
                }
            }
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
        #endregion
    }
}
