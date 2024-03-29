﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Disappearwind.PortalSolution.PortalWeb.Business;
using Disappearwind.PortalSolution.PortalWeb.Models;
using Disappearwind.PortalSolution.PortalWeb.Utility;
using Newtonsoft.Json;

namespace Disappearwind.PortalSolution.PortalWeb.Controllers
{
    /// <summary>
    /// App 接口
    /// </summary>
    public class ServiceController : Controller
    {
        public const string Client_Content_Type = "text/json;charset=UTF-8";
        /// <summary>
        /// ID，用于标识App
        /// </summary>
        public const int APP_ID = 1;

        AlbumBusiness albumBusiness = new AlbumBusiness();
        ClientUserBusiness clientUserBusiness = new ClientUserBusiness();
        PurchaseBusiness purchaseBusiness = new PurchaseBusiness();
        AppInfoBusiness appInfoBusiness = new AppInfoBusiness();
        FeedbackBusiness feedbackBusiness = new FeedbackBusiness();

        #region Private
        /// <summary>
        /// 从Request中取客户端传过来的图片并把图片保存到指定的位置
        /// <param name="width">如果宽度不为0，图片按宽度裁减</param>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool GetImageInRequest(string path, int width = 0)
        {
            if (Request.Files != null && Request.Files.Count > 0 && Request.Files[0].InputStream != null)
            {
                try
                {
                    System.Drawing.Image img = System.Drawing.Bitmap.FromStream(Request.Files[0].InputStream);
                    Bitmap bmp = new Bitmap(img);
                    MemoryStream bmpStream = new MemoryStream();
                    if (width > 0)
                    {
                        Graphics g = Graphics.FromImage(bmp);
                        Rectangle rect = new Rectangle(0, (int)bmp.Height / 4, width, (int)bmp.Height / 2);
                        g.DrawImage(bmp, rect);
                    }
                    bmp.Save(bmpStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    FileStream fs = new FileStream(path, FileMode.Create);
                    bmpStream.WriteTo(fs);
                    bmpStream.Close();
                    fs.Close();
                    bmpStream.Dispose();
                    fs.Dispose();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// 验证订单是否合法
        /// 判断加密的信息中是否包含com.1tuanwang
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool ValidatePurchase(string data)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(data))
            {
                str = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(data));
                str = str.Replace("=", ":").Replace(";", ",");
                Dictionary<string, string> list = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
                str = list["purchase-info"];
                str = str.Replace(":", "=");
                str = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(str));
                str = str.Replace("=", ":").Replace(";", ",");
                list = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
                str = list["product-id"];
            }
            if (str.Contains("com.1tuanwang"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 封装返回的数据为Json数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private JsonResult ReturnJson<T>(ServiceReturnData<T> data)
        {
            if (string.IsNullOrEmpty(data.Message))
            {
                data.Message = string.Empty;
            }
            if (string.IsNullOrEmpty(data.StrData))
            {
                data.StrData = string.Empty;
            }
            return Json(data, Client_Content_Type, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public JsonResult Index()
        {
            ServiceReturnData<Album> data = new ServiceReturnData<Album>();
            try
            {
                //获取首页照片墙
                int pageNum = 1;
                if (Request["pagenum"] != null)
                {
                    pageNum = Convert.ToInt32(Request["pagenum"]);
                }
                int pageSize = 30;
                if (Request["pagesize"] != null)
                {
                    pageSize = Convert.ToInt32(Request["pagesize"]);
                }
                int uid = 0;
                if (Request["uid"] != null)
                {
                    uid = Convert.ToInt32(Request["uid"]);
                }
                //如果当前版本在审核中，则需要加载审核专用的专辑
                List<Album> albumList = new List<Album>();
                AppInfo appInfo = appInfoBusiness.GetAppInfo(APP_ID);
                if (!string.IsNullOrEmpty(appInfo.Version) && appInfo.Version == Request["version"] && appInfo.State == 0)
                {
                    albumList = albumBusiness.GetAlbumList(pageNum, pageSize, uid, 1);
                }
                else
                {
                    albumList = albumBusiness.GetAlbumList(pageNum, pageSize, uid);
                }
                data.Code = 1;
                if (albumList.Count > 0)
                {
                    data.Message = "加载成功！";
                    data.ListData = albumList;
                }
                else
                {
                    data.Message = "无数据！";
                }
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<Album>(data);
        }
        /// <summary>
        /// 获取用户自己的专辑
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserAlbumList()
        {
            ServiceReturnData<Album> data = new ServiceReturnData<Album>();
            try
            {
                //获取首页照片墙
                int userid = 0;
                if (Request["uid"] == null)
                {
                    data.Code = 0;
                    data.Message = "uid不能为空";
                }
                else
                {
                    int? state = null;
                    if (Request["state"] != null)
                    {
                        state = Convert.ToInt32(Request["state"]);
                    }
                    userid = Convert.ToInt32(Request["uid"]);
                    List<Album> albumList = albumBusiness.GetUserAlbumList(userid, state);
                    if (albumList.Count > 0)
                    {
                        data.Code = 1;
                        data.ListData = albumList;
                    }
                }
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<Album>(data);
        }
        /// <summary>
        /// 专辑下的所有图片
        /// [{"uid":1,"aid":1}]
        /// </summary>
        /// <returns></returns>
        public JsonResult List()
        {
            ServiceReturnData<string> data = new ServiceReturnData<string>();
            try
            {
                string albumID = Request["aid"];
                List<string> list = new List<string>();
                if (!string.IsNullOrEmpty(albumID))
                {
                    list = albumBusiness.GetAlbumContentList(Convert.ToInt32(albumID));
                    if (list.Count > 0)
                    {
                        data.Code = 1;
                        data.ListData = list;
                    }
                }
                else
                {
                    data.Code = -1;
                    data.Message = "参数aid为空";
                }
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<string>(data);
        }
        /// <summary>
        /// 添加专辑
        /// </summary>
        /// <returns></returns>
        public JsonResult AddAlbum()
        {
            ServiceReturnData<string> data = new ServiceReturnData<string>();
            try
            {
                Album album = new Album();
                if (!string.IsNullOrEmpty(Request["uid"]))
                {
                    album.Creator = Convert.ToInt32(Request["uid"]);
                }
                album.Name = Request["aname"];
                string imagePath = string.Format("{0}/{1}/{2}.jpg",
                    AppDomain.CurrentDomain.BaseDirectory, AlbumBusiness.Resource_Dir, Guid.NewGuid());
                int imgWidth = 0;
                if (!string.IsNullOrEmpty(Request["width"]))
                {
                    imgWidth = Convert.ToInt32(Request["width"]);
                }
                if (GetImageInRequest(imagePath, imgWidth))
                {
                    album.ImageUrl = imagePath.Replace(AppDomain.CurrentDomain.BaseDirectory, string.Empty).Replace("\\", "/");
                    album.ImageUrl = album.ImageUrl.Replace("//", "/"); //处理地址中的//
                }
                else
                {
                    data.Code = 0;
                    data.Message = "专辑图片保存失败！";
                }
                int aid = albumBusiness.AddAlbum(album);
                data.StrData = string.Format("aid={0}", aid);
                data.Message += "添加成功！";
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<string>(data);
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadImage()
        {
            ServiceReturnData<string> data = new ServiceReturnData<string>();
            try
            {
                string uid = Request["uid"];
                string aid = Request["aid"];
                string imagePath = string.Format("{0}/{1}/{2}/{3}.jpg",
                   AppDomain.CurrentDomain.BaseDirectory, AlbumBusiness.Resource_Dir, aid, Guid.NewGuid());
                if (GetImageInRequest(imagePath))
                {
                    data.Message = "上传成功";
                    data.Code = 1;
                }
                else
                {
                    data.Message = "上传失败";
                    data.Code = 0;
                }
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<string>(data);
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
        /// 添加用户反馈
        /// </summary>
        /// <returns></returns>
        public JsonResult AddFeedback()
        {
            ServiceReturnData<string> data = new ServiceReturnData<string>();
            try
            {
                if (string.IsNullOrEmpty(Request["uid"]) || string.IsNullOrEmpty(Request["content"]))
                {
                    data.Code = 0;
                    data.Message = "uid和content都不能为空";
                }
                else
                {
                    Feedback feedback = new Feedback();
                    feedback.UserID = Convert.ToInt32(Request["uid"]);
                    feedback.Content = System.Text.UTF8Encoding.UTF8.GetString(System.Text.UTF8Encoding.UTF8.GetBytes(Request["content"]));
                    feedbackBusiness.AddFeedback(feedback);
                    data.Code = 1;
                    data.Message = "添加成功！";
                }
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<string>(data);
        }

        #region User

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public JsonResult Register()
        {
            ServiceReturnData<string> data = new ServiceReturnData<string>();
            try
            {
                if (string.IsNullOrEmpty(Request["username"]) || string.IsNullOrEmpty(Request["pwd"]))
                {
                    data.Code = 0;
                    data.Message = "用户名或者密码不能为空";
                }
                else
                {
                    //先判断用户是否存在
                    ClientUser userInfo = clientUserBusiness.GetClientUser(Request["username"]);
                    if (userInfo != null && userInfo.UserID != 0)
                    {
                        data.Code = 0;
                        data.Message = "用户名重复";
                    }
                    else
                    {
                        userInfo.DeviceNum = Request["devicenum"];
                        userInfo.NickName = Request["nickname"];
                        userInfo.Phone = Request["phone"];
                        userInfo.Pwd = Request["pwd"];
                        userInfo.UserName = Request["username"];
                        int uid = clientUserBusiness.AddClientUser(userInfo);
                        if (uid > 0)
                        {
                            data.Code = 1;
                            clientUserBusiness.UserLogin(userInfo.UserName, userInfo.Pwd);
                            data.DictData.Add("uid", uid.ToString());
                            AppInfo appInfo = appInfoBusiness.GetAppInfo(APP_ID);
                            if (appInfo != null && appInfo.ID != 0)
                            {
                                data.DictData.Add("version", appInfo.Version);
                                data.DictData.Add("upgrade", appInfo.VersionUpgrade.ToString());
                            }
                            data.DictData.Add("gold", Convert.ToInt32(MagicNum.GOLDNUM.REGISTER).ToString()); //注册成功送的金币
                            data.Message = "注册成功";
                        }
                        else
                        {
                            data.Code = -1;
                            data.Message = "注册失败";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<string>(data);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public JsonResult Login()
        {
            ServiceReturnData<string> data = new ServiceReturnData<string>();
            try
            {
                string username = Request["username"];
                string pwd = Request["pwd"];
                string uid = "0";
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(pwd))
                {
                    uid = clientUserBusiness.UserLogin(username, pwd).ToString();
                }
                else if (!string.IsNullOrEmpty(Request["uid"]))
                {
                    uid = Request["uid"];
                }
                if (!string.IsNullOrEmpty(uid) && uid != "0" && uid != "-1")
                {
                    ClientUserLogin loginInfo = new ClientUserLogin();
                    loginInfo.DeviceNum = Request["devicenum"];
                    loginInfo.UserID = Convert.ToInt32(uid);
                    clientUserBusiness.AddClientUserLogin(loginInfo);
                    //返回用户的金币数
                    ClientUser userInfo = clientUserBusiness.GetClientUser(Convert.ToInt32(uid));
                    data.DictData.Add("gold", userInfo.Score == null ? "0" : userInfo.Score.ToString());
                    data.Code = 1;
                    data.Message = "登录成功";
                }
                else
                {
                    data.Code = 0;
                    data.Message = "用户不存在";
                }

                data.DictData.Add("uid", uid.ToString());
                AppInfo appInfo = appInfoBusiness.GetAppInfo(APP_ID);
                if (appInfo != null && appInfo.ID != 0)
                {
                    data.DictData.Add("version", appInfo.Version);
                    data.DictData.Add("upgrade", appInfo.VersionUpgrade.ToString());
                }
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<string>(data);
        }
        /// <summary>
        /// 获得用户的信息：头像、金币、昵称
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserInfo()
        {
            ServiceReturnData<string> data = new ServiceReturnData<string>();
            try
            {
                if (string.IsNullOrEmpty(Request["uid"]))
                {
                    data.Code = 0;
                    data.Message = "uid不能为空";
                }
                else
                {
                    string uid = Request["uid"];
                    ClientUser userInfo = clientUserBusiness.GetClientUser(Convert.ToInt32(uid));
                    if (userInfo != null)
                    {
                        string headurl = string.Format("{0}/UserHead/{1}.jpg", AlbumBusiness.Resource_Dir, uid);
                        if (!System.IO.File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, headurl)))
                        {
                            //头像不存在就不返回
                            headurl = string.Empty;
                        }
                        data.DictData.Add("headurl", headurl);
                        data.DictData.Add("nickname", string.IsNullOrEmpty(userInfo.NickName) ? userInfo.UserName : userInfo.NickName);
                        data.DictData.Add("gold", userInfo.Score == null ? "0" : userInfo.Score.ToString());
                        data.Code = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<string>(data);
        }
        /// <summary>
        /// 上传用户头像
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadUserHead()
        {
            ServiceReturnData<string> data = new ServiceReturnData<string>();
            try
            {
                if (string.IsNullOrEmpty(Request["uid"]))
                {
                    data.Code = 0;
                    data.Message = "uid不能为空";
                }
                else
                {
                    string uid = Request["uid"];
                    string imgHeadPath = string.Format("{0}/{1}/UserHead/{2}.jpg",
                        AppDomain.CurrentDomain.BaseDirectory, AlbumBusiness.Resource_Dir, uid);
                    if (GetImageInRequest(imgHeadPath))
                    {
                        data.Code = 1;
                        data.Message = "上传成功！";
                    }
                    else
                    {
                        data.Code = 0;
                        data.Message = "上传失败！";
                    }
                }
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<string>(data);
        }
        /// <summary>
        /// 更新积分
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateUserScore()
        {
            ServiceReturnData<string> data = new ServiceReturnData<string>();
            try
            {
                if (string.IsNullOrEmpty(Request["uid"]) || string.IsNullOrEmpty(Request["gold"]))
                {
                    data.Code = 0;
                    data.Message = "uid和gold都不能为空";
                }
                else
                {
                    int uid = Convert.ToInt32(Request["uid"]);
                    int score = Convert.ToInt32(Request["gold"]);
                    int aid = Convert.ToInt32(Request["aid"]);
                    int resultScore = clientUserBusiness.UpdateScoreForViewAlbum(aid, uid, score);
                    data.StrData = resultScore.ToString();
                    data.Code = 1;
                    data.Message = "更新成功！";
                }
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<string>(data);
        }
        /// <summary>
        /// 更新用户昵称
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateUserNickName()
        {
            ServiceReturnData<string> data = new ServiceReturnData<string>();
            try
            {
                if (string.IsNullOrEmpty(Request["uid"]) || string.IsNullOrEmpty(Request["nickname"]))
                {
                    data.Code = 0;
                    data.Message = "uid和nickname都不能为空";
                }
                else
                {
                    int uid = Convert.ToInt32(Request["uid"]);
                    string nickname = Request["nickname"];
                    nickname = System.Text.UTF8Encoding.UTF8.GetString(System.Text.UTF8Encoding.UTF8.GetBytes(nickname));
                    clientUserBusiness.UpdateNickName(uid, nickname);
                    data.Code = 1;
                    data.Message = "更新成功！";
                }
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<string>(data);
        }
        #endregion

        #region Purchase
        /// <summary>
        /// 获取订购产品的列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPurchaseProductList()
        {
            ServiceReturnData<PurchaseProduct> data = new ServiceReturnData<PurchaseProduct>();
            try
            {
                List<PurchaseProduct> productList = purchaseBusiness.GetProductList();
                if (productList.Count > 0)
                {
                    data.Code = 1;
                    data.Message = "获取成功！";
                    data.ListData = productList;
                }
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<PurchaseProduct>(data);
        }
        /// <summary>
        /// 同步客户端的订购产生的订单
        /// </summary>
        /// <returns></returns>
        public JsonResult AddPurchaseOrder()
        {
            ServiceReturnData<string> data = new ServiceReturnData<string>();
            try
            {
                if (string.IsNullOrEmpty(Request["productid"]) || string.IsNullOrEmpty(Request["uid"]))
                {
                    data.Code = 0;
                    data.Message = "产品ID和用户ID不能为空";
                }
                else
                {
                    string receipt = Request["receipt"];
                    if (ValidatePurchase(receipt))
                    {
                        PurchaseOrder order = new PurchaseOrder();
                        order.ProductID = Request["productid"];
                        order.UserID = Convert.ToInt32(Request["uid"]);
                        order.TransactionID = Request["transactionid"];

                        if (purchaseBusiness.AddOrder(order))
                        {
                            string uid = Request["uid"];
                            ClientUser userInfo = clientUserBusiness.GetClientUser(Convert.ToInt32(uid));
                            data.DictData.Add("gold", userInfo.Score == null ? "0" : userInfo.Score.ToString());
                            data.Code = 1;
                            data.Message = "订单添加成功！";
                        }
                    }
                    else
                    {
                        data.Code = 0;
                        data.Message = "订单不合法！";
                    }
                }
            }
            catch (Exception ex)
            {
                data.Code = 0;
                data.Message = ex.Message;
            }
            return ReturnJson<string>(data);
        }
        #endregion

        #region Testor
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
                string url = Request.Url.AbsoluteUri.ToLower().Replace("simulateclient", Request.Form["ddlAction"]);
                url = string.Format("{0}?{1}", url, Request.Form["txtData"]);
                string fileData = string.Empty;
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    byte[] data = new byte[Request.Files[0].InputStream.Length];
                    Request.Files[0].InputStream.Read(data, 0, data.Length);
                    fileData = System.Text.Encoding.UTF8.GetString(data);
                }
                string str = string.Empty;
                str = WebAccessUtility.Request(url, fileData, Client_Content_Type);
                ViewData["ServiceRequest"] = Request.Form["txtData"];
                ViewData["ServiceAction"] = Request.Form["ddlAction"];
                ViewData["ServiceResponse"] = str;
            }
            return View();
        }
        /// <summary>
        /// 输出测试
        /// </summary>
        /// <returns></returns>
        public JsonResult Test()
        {
            ServiceReturnData<string> data = new ServiceReturnData<string>();
            try
            {
                string str = string.Empty;
                //str = string.Format("DateTime.Now={0} & Date.Now.ToShortDateString()={1}", DateTime.Now, DateTime.Now.ToShortDateString());
                //str = Convert.ToInt32(MagicNum.GOLDNUM.REGISTER).ToString();
                str = @"ewoJInNpZ25hdHVyZSIgPSAiQWlwRk84TFQ3a1NCa25JMVBmSWZGRzM4YlY0enJOMVRzQUdjQUhX
S1JWRDNIeUIyL1p3ZTB3WFR4bUJ0UEhwaWVOZ3BDSUhybUZWYzIvN214WnBFK29zUzFiMnhacWkz
Z1d4TzVTVHB5dnlvWWxpNE5TNGxoL0poY0JPbHVUWDkxNnRWVFpwNGNvTks0OGhYMGJLa0hMM3hZ
SjR6VGltSk1sK2thdS9VWER3cUFBQURWekNDQTFNd2dnSTdvQU1DQVFJQ0NCdXA0K1BBaG0vTE1B
MEdDU3FHU0liM0RRRUJCUVVBTUg4eEN6QUpCZ05WQkFZVEFsVlRNUk13RVFZRFZRUUtEQXBCY0hC
c1pTQkpibU11TVNZd0pBWURWUVFMREIxQmNIQnNaU0JEWlhKMGFXWnBZMkYwYVc5dUlFRjFkR2h2
Y21sMGVURXpNREVHQTFVRUF3d3FRWEJ3YkdVZ2FWUjFibVZ6SUZOMGIzSmxJRU5sY25ScFptbGpZ
WFJwYjI0Z1FYVjBhRzl5YVhSNU1CNFhEVEUwTURZd056QXdNREl5TVZvWERURTJNRFV4T0RFNE16
RXpNRm93WkRFak1DRUdBMVVFQXd3YVVIVnlZMmhoYzJWU1pXTmxhWEIwUTJWeWRHbG1hV05oZEdV
eEd6QVpCZ05WQkFzTUVrRndjR3hsSUdsVWRXNWxjeUJUZEc5eVpURVRNQkVHQTFVRUNnd0tRWEJ3
YkdVZ1NXNWpMakVMTUFrR0ExVUVCaE1DVlZNd2daOHdEUVlKS29aSWh2Y05BUUVCQlFBRGdZMEFN
SUdKQW9HQkFNbVRFdUxnamltTHdSSnh5MW9FZjBlc1VORFZFSWU2d0Rzbm5hbDE0aE5CdDF2MTk1
WDZuOTNZTzdnaTNvclBTdXg5RDU1NFNrTXArU2F5Zzg0bFRjMzYyVXRtWUxwV25iMzRucXlHeDlL
QlZUeTVPR1Y0bGpFMU93QytvVG5STStRTFJDbWVOeE1iUFpoUzQ3VCtlWnRERWhWQjl1c2szK0pN
MkNvZ2Z3bzdBZ01CQUFHamNqQndNQjBHQTFVZERnUVdCQlNKYUVlTnVxOURmNlpmTjY4RmUrSTJ1
MjJzc0RBTUJnTlZIUk1CQWY4RUFqQUFNQjhHQTFVZEl3UVlNQmFBRkRZZDZPS2RndElCR0xVeWF3
N1hRd3VSV0VNNk1BNEdBMVVkRHdFQi93UUVBd0lIZ0RBUUJnb3Foa2lHOTJOa0JnVUJCQUlGQURB
TkJna3Foa2lHOXcwQkFRVUZBQU9DQVFFQWVhSlYyVTUxcnhmY3FBQWU1QzIvZkVXOEtVbDRpTzRs
TXV0YTdONlh6UDFwWkl6MU5ra0N0SUl3ZXlOajVVUllISytIalJLU1U5UkxndU5sMG5rZnhxT2Jp
TWNrd1J1ZEtTcTY5Tkluclp5Q0Q2NlI0Szc3bmI5bE1UQUJTU1lsc0t0OG9OdGxoZ1IvMWtqU1NS
UWNIa3RzRGNTaVFHS01ka1NscDRBeVhmN3ZuSFBCZTR5Q3dZVjJQcFNOMDRrYm9pSjNwQmx4c0d3
Vi9abEwyNk0ydWVZSEtZQ3VYaGRxRnd4VmdtNTJoM29lSk9PdC92WTRFY1FxN2VxSG02bTAzWjli
N1BSellNMktHWEhEbU9Nazd2RHBlTVZsTERQU0dZejErVTNzRHhKemViU3BiYUptVDdpbXpVS2Zn
Z0VZN3h4ZjRjemZIMHlqNXdOelNHVE92UT09IjsKCSJwdXJjaGFzZS1pbmZvIiA9ICJld29KSW05
eWFXZHBibUZzTFhCMWNtTm9ZWE5sTFdSaGRHVXRjSE4wSWlBOUlDSXlNREUxTFRBeExUSTFJREU0
T2pRNU9qTTRJRUZ0WlhKcFkyRXZURzl6WDBGdVoyVnNaWE1pT3dvSkluVnVhWEYxWlMxcFpHVnVk
R2xtYVdWeUlpQTlJQ0kzTkRnek1XRmxZV1JsWldGaE1XVmpOVEUyWTJaaVltUTBZV05oTURZek1q
ZGhNRGhsTjJaa0lqc0tDU0p2Y21sbmFXNWhiQzEwY21GdWMyRmpkR2x2YmkxcFpDSWdQU0FpTVRB
d01EQXdNREUwTURNeE1qWXdOaUk3Q2draVluWnljeUlnUFNBaU1TNHdMakFpT3dvSkluUnlZVzV6
WVdOMGFXOXVMV2xrSWlBOUlDSXhNREF3TURBd01UUXdNekV5TmpBMklqc0tDU0p4ZFdGdWRHbDBl
U0lnUFNBaU1TSTdDZ2tpYjNKcFoybHVZV3d0Y0hWeVkyaGhjMlV0WkdGMFpTMXRjeUlnUFNBaU1U
UXlNakkwTURVM09ERTVOQ0k3Q2draWRXNXBjWFZsTFhabGJtUnZjaTFwWkdWdWRHbG1hV1Z5SWlB
OUlDSTVOVGcyUVRjNFFpMDFOa1ZDTFRSRk9Ea3RPVU5DUmkxRlJFUTJNa0k1TkRJMVJVWWlPd29K
SW5CeWIyUjFZM1F0YVdRaUlEMGdJbU52YlM0eGRIVmhibmRoYm1jdU1UaERUbGxmTWpBd1IwSWlP
d29KSW1sMFpXMHRhV1FpSUQwZ0lqa3pNekV6TURZeE55STdDZ2tpWW1sa0lpQTlJQ0pqYjIwdU1Y
UjFZVzUzWVc1bkxtMWxhWFIxYTNWaGFXSnZJanNLQ1NKd2RYSmphR0Z6WlMxa1lYUmxMVzF6SWlB
OUlDSXhOREl5TWpRd05UYzRNVGswSWpzS0NTSndkWEpqYUdGelpTMWtZWFJsSWlBOUlDSXlNREUx
TFRBeExUSTJJREF5T2pRNU9qTTRJRVYwWXk5SFRWUWlPd29KSW5CMWNtTm9ZWE5sTFdSaGRHVXRj
SE4wSWlBOUlDSXlNREUxTFRBeExUSTFJREU0T2pRNU9qTTRJRUZ0WlhKcFkyRXZURzl6WDBGdVoy
VnNaWE1pT3dvSkltOXlhV2RwYm1Gc0xYQjFjbU5vWVhObExXUmhkR1VpSUQwZ0lqSXdNVFV0TURF
dE1qWWdNREk2TkRrNk16Z2dSWFJqTDBkTlZDSTdDbjA9IjsKCSJlbnZpcm9ubWVudCIgPSAiU2Fu
ZGJveCI7CgkicG9kIiA9ICIxMDAiOwoJInNpZ25pbmctc3RhdHVzIiA9ICIwIjsKfQ==";
                /*
                str = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(str));
                str = str.Replace("=", ":").Replace(";", ",");
                Dictionary<string, string> list = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
                str = list["purchase-info"];
                str = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(str));
                str = str.Replace("=", ":").Replace(";", ",");
                list = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
                str = list["product-id"];
                 */
                data.Message = ValidatePurchase(str).ToString();

            }
            catch (Exception ex)
            {
                data.Message = ex.Message;
            }
            return ReturnJson<string>(data);
        }
        #endregion
    }
}
