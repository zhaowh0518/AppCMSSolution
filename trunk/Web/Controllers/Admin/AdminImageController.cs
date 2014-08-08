using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Disappearwind.PortalSolution.PortalWeb.Utility;
using System.IO;

namespace Disappearwind.PortalSolution.PortalWeb.Controllers.Admin
{
    public class AdminImageController : AdminController
    {
        public AdminImageController()
        {
            ViewData[PortalSessionKey.ImageSubPath.ToString()] = string.Empty;
        }
        /// <summary>
        /// Sub path of image directory
        /// </summary>
        public string SubDirectory
        {
            get
            {
                if (ViewData[PortalSessionKey.ImageSubPath.ToString()] != null)
                {
                    return ViewData[PortalSessionKey.ImageSubPath.ToString()].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewData[PortalSessionKey.ImageSubPath.ToString()] = value;
            }
        }

        //
        // GET: /AdminImage/

        public ActionResult Index(string subPath)
        {
            SubDirectory = subPath;
            GetImageList();
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(FormCollection collection)
        {
            if (Request.Files.Count == 0)
            {
                return View();
            }
            var c = Request.Files[0];
            if (c != null && c.ContentLength > 0)
            {
                int lastSlashIndex = c.FileName.LastIndexOf("\\");
                string fileName = c.FileName.Substring(lastSlashIndex + 1, c.FileName.Length - lastSlashIndex - 1);
                fileName = Path.Combine(CommonUtility.DocImagePath, fileName);
                c.SaveAs(fileName);
            }
            GetImageList();
            return View();
        }

        private void GetImageList()
        {
            string dirPath = Path.Combine(CommonUtility.DocImagePath, SubDirectory);
            Dictionary<string, string> fileList = new Dictionary<string, string>();
            List<string> floderList = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            foreach (var item in dirInfo.GetDirectories())
            {
                floderList.Add(item.Name);
            }
            string currentRootUrl = CommonUtility.DocImageURL;
            if (!string.IsNullOrEmpty(SubDirectory))
            {
                currentRootUrl += SubDirectory + "/";
            }
            foreach (var item in dirInfo.GetFiles())
            {
                if (IsImage(item.Name))
                {
                    fileList.Add(item.Name, currentRootUrl + item.Name);
                }
            }
            ViewData[PortalSessionKey.ImageFolderList.ToString()] = floderList;
            ViewData[PortalSessionKey.ImageFileList.ToString()] = fileList;
        }
        /// <summary>
        /// If a file is image
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        private bool IsImage(string imageName)
        {
            int pointIndex = imageName.LastIndexOf(".") + 1;
            string extendName = imageName.Substring(pointIndex, imageName.Length - pointIndex);
            if (ConfigUtility.ImageType.Contains(extendName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
