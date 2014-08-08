using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Disappearwind.PortalSolution.PortalWeb.Business;
using Disappearwind.PortalSolution.PortalWeb.Models;
using Disappearwind.PortalSolution.PortalWeb.Utility;

namespace Disappearwind.PortalSolution.PortalWeb.Controllers
{
    /// <summary>
    /// App 接口
    /// </summary>
    public class ServiceController : Controller
    {
        public const string Client_Content_Type = "text/json;charset=UTF-8";

        AlbumBusiness albumBusiness = new AlbumBusiness();
        ClientUserBusiness clientUserBusiness = new ClientUserBusiness();

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public JsonResult Index()
        {
            //获取首页照片墙
            List<Album> albumList = albumBusiness.GetAlbumList();
            return Json(albumList, Client_Content_Type, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 专辑下的所有图片
        /// [{"uid":1,"aid":1}]
        /// </summary>
        /// <returns></returns>
        public JsonResult List()
        {
            string albumID = Request["aid"];
            //string albumID = "1";
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(albumID))
            {
                list = albumBusiness.GetAlbumContentList(Convert.ToInt32(albumID));
            }
            return Json(list, Client_Content_Type, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public JsonResult Register()
        {
            //先判断用户是否存在
            ClientUser userInfo = clientUserBusiness.GetClientUser(Request["username"]);
            if (userInfo != null && userInfo.UserID != 0)
            {
                return Json(new ClientMessage("Repeat", "-1"), Client_Content_Type, JsonRequestBehavior.AllowGet);
            }
            else
            {
                userInfo.DeviceNum = Request["devicenum"];
                userInfo.NickName = Request["nickyname"];
                userInfo.Phone = Request["phone"];
                userInfo.Pwd = Request["pwd"];
                userInfo.UserName = Request["username"];
                int uid = clientUserBusiness.AddClientUser(userInfo);
                return Json(new ClientMessage("OK", uid.ToString()), Client_Content_Type, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public JsonResult Login()
        {
            string uid = Request["uid"];
            ClientUser userInfo = new ClientUser();
            if (!string.IsNullOrEmpty(uid))
            {
                ClientUserLogin loginInfo = new ClientUserLogin();
                loginInfo.DeviceNum = Request["devicenum"];
                loginInfo.UserID = Convert.ToInt32(Request["uid"]);
                clientUserBusiness.AddClientUserLogin(loginInfo);
                userInfo = clientUserBusiness.GetClientUser(loginInfo.UserID);
            }
            return Json(new ClientMessage("OK", userInfo.Score.ToString()), Client_Content_Type, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取静态页
        /// </summary>
        /// <returns></returns>
        public string Page()
        {
            string result = string.Empty;
            string pageName = Request["pname"];
            //pageName = "1.txt";
            string pageFileName = string.Format("{0}/Resource/Pages/{1}", AppDomain.CurrentDomain.BaseDirectory, pageName);
            if (HttpRuntime.Cache.Get(pageFileName) == null || string.IsNullOrEmpty(HttpRuntime.Cache.Get(pageFileName).ToString()))
            {
                System.Web.Caching.CacheDependency depe = new System.Web.Caching.CacheDependency(pageFileName);
                using (FileStream fs = new FileStream(pageFileName, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fs, System.Text.UTF8Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                        reader.Close();
                    }
                    fs.Close();
                }
                HttpRuntime.Cache.Insert(pageFileName, result, depe);
            }
            result = HttpRuntime.Cache.Get(pageFileName).ToString();
            return result;
        }
        /// <summary>
        /// 更新积分
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateUserScore()
        {
            int uid = Convert.ToInt32(Request["uid"]);
            int score = Convert.ToInt32(Request["score"]);
            int resultScore = clientUserBusiness.UpdateClientUserScore(uid, score);
            return Json(new ClientMessage("OK", resultScore.ToString()), Client_Content_Type, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Action测试
        /// </summary>
        public void Testor()
        {
            //string sqlText = "";    //"ALTER TABLE AppuserLogin RENAME TO ClientUserLogin;";
            //Disappearwind.PortalSolution.PortalWeb.Models.DataAccess da = new DataAccess();
            //da.ExcuteSQLText(sqlText);
            //BatchAdd();
        }
        private void BatchAdd()
        {
            Album a = new Album();
            a.Name = "测试";
            a.ImageUrl = "/resource/docimages/1.png";
            a.Url = "/resource/docimages/1/";
            a.Creator = 1;
            for (int i = 0; i < 1000; i++)
            {
                a.Name = "测试" + (i + 1).ToString();
                albumBusiness.AddAlbum(a);
            }
        }
        /// <summary>
        /// 模拟客户端的行为
        /// </summary>
        /// <returns></returns>
        public ActionResult SimulateClient()
        {
            if (Request.Form.Count > 0)
            {
                string url = Request.Url.AbsoluteUri.Replace("SimulateClient", Request.Form["ddlAction"]);
                string str = string.Empty;
                str = WebAccessUtility.Request(url, Request.Form["txtData"], Client_Content_Type);
                ViewData["ServiceRequest"] = Request.Form["txtData"];
                ViewData["ServiceResponse"] = str;
            }
            return View();
        }
    }
}
