using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Disappearwind.PortalSolution.PortalWeb.Business;
using Disappearwind.PortalSolution.PortalWeb.Models;
using System.IO;
using System.Drawing;

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
        public static string DocImageURL { get { return string.Format("/Resource/DocImages/"); } }
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

        /// <summary>
        /// 把数字类专辑状态转化成用户能读懂的字符串
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        public static string ShowAlbumState(int? stat)
        {
            string strState = "未审核";
            switch (stat)
            {
                case 1:
                    strState = "审核通过";
                    break;
                case -1:
                    strState = "审核未通过";
                    break;
            }
            return strState;
        }

        /// <summary>
        /// 保存流中的图片
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="path"></param>
        /// <param name="width"></param>
        public static void SaveStreamImage(Stream fileStream, string path, int width = 0)
        {
            System.Drawing.Image img = System.Drawing.Bitmap.FromStream(fileStream);
            Bitmap bmp = new Bitmap(img);
            MemoryStream bmpStream = new MemoryStream();
            if (width == -1)
            {
                width = bmp.Width;
            }
            if (width > 0 && width<bmp.Height)
            {
                Graphics g = Graphics.FromImage(bmp);
                int startY = (bmp.Height - width) / 2;
                int endY = width + startY;
                Rectangle rect = new Rectangle(0, startY, width, endY);
                bmp.MakeTransparent();
                g.DrawImage(bmp, rect);
            }
            bmp.Save(bmpStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            FileStream fs = new FileStream(path, FileMode.Create);
            bmpStream.WriteTo(fs);
            bmpStream.Close();
            fs.Close();
            bmpStream.Dispose();
            fs.Dispose();
        }
    }
}
