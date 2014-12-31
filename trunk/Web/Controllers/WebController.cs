using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Disappearwind.PortalSolution.PortalWeb.Business;
using Disappearwind.PortalSolution.PortalWeb.Models;
using Disappearwind.PortalSolution.PortalWeb.Utility;
using System.IO;

namespace Disappearwind.PortalSolution.PortalWeb.Controllers
{
    public class WebController : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 处理登录
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Login(FormCollection form)
        {
            try
            {
                string username = form["txtUserName"];
                string password = form["txtPassword"];

                //密码客户端做了Base64加密此时需要解密
                password = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(password));
                ClientUserBusiness clientUserBusiness = new ClientUserBusiness();
                int uid = clientUserBusiness.UserLogin(username, password);
                if (uid > 0)
                {
                    Session["CLIENT_UID"] = uid;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["LOGIN_MESSAGE"] = "* 用户名或密码错误，请重试！";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["LOGIN_MESSAGE"] = "* 登录异常，请稍后再试！";
                return View();
            }
        }

        /// <summary>
        /// 展示用户的专辑列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["CLIENT_UID"] != null && Session["CLIENT_UID"].ToString() != string.Empty)
            {
                List<Album> albumnList = new AlbumBusiness().GetUserAlbumList(Convert.ToInt32(Session["CLIENT_UID"]), null);
                return View(albumnList);
            }
            else
            {
                //未登录就返回登陆页
                return View("Login");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize]
        public ActionResult Index(FormCollection forms)
        {
            int aid = 0;
            try
            {
                aid = Convert.ToInt32(Request["aid"]);
                List<Album> albumnList = new AlbumBusiness().GetUserAlbumList(Convert.ToInt32(Session["CLIENT_UID"]), null);
                var c = albumnList.Single(p => p.Id == aid);
                if (c == null || c.Id == 0) 
                {
                    throw new Exception("非法访问的专辑！");
                }
                string cmd = Request.Form["cmd"];
                switch (cmd)
                {
                    case "UploadCover":
                        UploadCover(aid);
                        break;
                    case "UploadImage":
                        UploadImages(aid);
                        break;
                    case "DeleteImage":
                        DeleteImages(aid);
                        break;
                    case "ViewImage":
                        GetImageList(aid);
                        break;
                    default:
                        break;
                }
                Session["INDEX_MESSAGE"] = "操作成功！";
            }
            catch (Exception ex)
            {
                Session["INDEX_MESSAGE"] = "服务器错误，请重试！";
            }
            Session["AID"] = aid;
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 得到图片的文件名
        /// </summary>
        /// <param name="clientFileName"></param>
        /// <returns></returns>
        private string GetFileName(string clientFileName)
        {
            int lastSlashIndex = 0;
            string extendName = string.Empty;
            string dir = CommonUtility.DocImagePath;
            lastSlashIndex = clientFileName.LastIndexOf(".");
            extendName = clientFileName.Substring(lastSlashIndex + 1, clientFileName.Length - lastSlashIndex - 1);
            if (!ConfigUtility.ImageType.Contains(extendName))
            {
                throw new Exception("非法格式的文件！");
            }
            return string.Format("{0}.{1}",  Guid.NewGuid(), extendName);
        }
        /// <summary>
        /// 上传封面
        /// </summary>
        /// <param name="aid"></param>
        private void UploadCover(int aid)
        {
            HttpPostedFileBase file = (HttpPostedFileBase)Request.Files[0];
            string fileName = GetFileName(file.FileName);
            CommonUtility.SaveStreamImage(file.InputStream, CommonUtility.DocImagePath + fileName, -1);
            //更新到数据库
            AlbumBusiness albumBusiness = new AlbumBusiness();
            Album album = albumBusiness.GetAlbum(aid);
            if (album != null)
            {
                album.ImageUrl = CommonUtility.DocImageURL + fileName;
                albumBusiness.UpdateAlbum(album);
            }
        }
        /// <summary>
        /// 上传专辑内的图片
        /// </summary>
        /// <param name="aid"></param>
        private void UploadImages(int aid)
        {
            string fileName = string.Empty;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = (HttpPostedFileBase)Request.Files[i];
                if (file != null && file.FileName != null && !string.IsNullOrEmpty(file.FileName))
                {
                    fileName = GetFileName(file.FileName);
                    fileName = string.Format("{0}/{1}/{2}",CommonUtility.DocImagePath,aid,fileName);
                    file.SaveAs(fileName);
                }
            }
            GetImageList(aid);
        }
        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="aid"></param>
        private void GetImageList(int aid)
        {
            string dirPath = Path.Combine(CommonUtility.DocImagePath, aid.ToString());
            Dictionary<string, string> fileList = new Dictionary<string, string>();
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            foreach (var item in dirInfo.GetFiles())
            {
                    fileList.Add(item.Name, string.Format("{0}/{1}/{2}",CommonUtility.DocImageURL,aid,item.Name));
            }
            Session["IMAGE_LIST"] = fileList;
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        private void DeleteImages(int aid)
        {
            string selectedImage = Request.Form["selectedImage"];
            string[] imageList = selectedImage.Split(',');
            string filePath = string.Empty;
            for (int i = 0; i < imageList.Length; i++)
            {
                filePath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, imageList[i]);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            GetImageList(aid);
        }
    }
}
