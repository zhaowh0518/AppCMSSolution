using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Disappearwind.PortalSolution.PortalWeb.Utility;

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
            ViewData["SubDirectory"] = SubDirectory;
            GetImageList();
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize]
        public ActionResult Index(HttpPostedFileBase[] fileList)
        {
            if (fileList == null || fileList.Length == 0)
            {
                return View();
            }
            string subPath = string.Empty;
            int lastSlashIndex = 0;
            string fileName = string.Empty;
            string dir = CommonUtility.DocImagePath;
            //ÓÐ×ÓÄ¿Â¼
            if (!string.IsNullOrEmpty(Request["subpath"]))
            {
                dir = Path.Combine(dir, Request["subpath"]);
            }
            foreach (HttpPostedFileBase c in fileList)
            {
                if (c != null && c.FileName != null && !string.IsNullOrEmpty(c.FileName))
                {
                    lastSlashIndex = c.FileName.LastIndexOf(".");
                    fileName = c.FileName.Substring(lastSlashIndex + 1, c.FileName.Length - lastSlashIndex - 1);
                    if (fileName != "exe")
                    {
                        fileName = string.Format("{0}.{1}", Guid.NewGuid(), fileName);
                        fileName = Path.Combine(dir, fileName);
                        c.SaveAs(fileName);
                    }
                }
            }
            GetImageList();
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize]
        public ActionResult Delete()
        {
            string selectedImage = Request.Form["selectedImage"];
            string[] imageList = selectedImage.Split(',');
            string filePath = string.Empty;
            for (int i = 0; i < imageList.Length; i++)
            {
                filePath = string.Format("{0}\\{1}",AppDomain.CurrentDomain.BaseDirectory, imageList[i]);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            return RedirectToAction("Index");
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
