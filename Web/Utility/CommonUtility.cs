using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Disappearwind.PortalSolution.PortalWeb.Business;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Utility
{
    /// <summary>
    /// Some common utility
    /// </summary>
    public static class CommonUtility
    {
        /// <summary>
        /// Document image path, windows path
        /// </summary>
        public static string DocImagePath
        {
            get
            {
                string currentPath = AppDomain.CurrentDomain.BaseDirectory;
                currentPath = currentPath.Replace("bin\\", "");
                return currentPath + "Resource\\DocImages\\";
            }
        }
        /// <summary>
        /// Document image url
        /// </summary>
        public static string DocImageURL { get { return string.Format("/resource/docimages/"); } }
        /// <summary>
        /// Page size of pagger
        /// </summary>
        public static readonly int PageSize = 15;
        /// <summary>
        /// Get MenuName by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetMenuName(object id)
        {
            string menuName = string.Empty;
            try
            {
                int menuId = Convert.ToInt32(id);
                PortalMenuBusiness menuBusiness = new PortalMenuBusiness();
                menuName = menuBusiness.GetPortalMenu(menuId).DisplayName;
            }
            catch
            {
                menuName = string.Empty;
            }
            return menuName;
        }
        /// <summary>
        /// Get MenuName by keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static string GetMenuName(string keyword)
        {
            string menuName = string.Empty;
            try
            {
                PortalMenuBusiness menuBusiness = new PortalMenuBusiness();
                menuName = menuBusiness.GetPortalMenu(keyword).DisplayName;
            }
            catch
            {
                menuName = string.Empty;
            }
            return menuName;
        }
        /// <summary>
        /// Get documents by menu keyword
        /// </summary>
        /// <param name="menuKeyWord"></param>
        /// <returns></returns>
        public static List<PortalDocument> GetDocument(string menuKeyWord)
        {
            PortalDocumentBusiness docBusiness = new PortalDocumentBusiness();
            List<PortalDocument> docList = docBusiness.GetPortalDocumentList(menuKeyWord);
            if (docList == null)
            {
                docList = new List<PortalDocument>();
            }
            return docList;
        }
    }
}
