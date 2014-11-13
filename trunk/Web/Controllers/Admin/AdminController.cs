using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Disappearwind.PortalSolution.PortalWeb.Business;
using Disappearwind.PortalSolution.PortalWeb.Models;
using Disappearwind.PortalSolution.PortalWeb.Utility;

namespace Disappearwind.PortalSolution.PortalWeb.Controllers
{
    public class AdminController : Controller
    {
        private PortalInfoBusiness _portalInfoBusiness = new PortalInfoBusiness();
        /// <summary>
        /// PortalInfoBusiness
        /// </summary>
        public PortalInfoBusiness BusinessPortalInfo { get { return _portalInfoBusiness; } }

        private PortalMenuBusiness _portalMenuBusiness = new PortalMenuBusiness();
        /// <summary>
        /// PortalMenuBusiness
        /// </summary>
        public PortalMenuBusiness BusinessPortalMenu { get { return _portalMenuBusiness; } }

        private PortalDocumentBusiness _portalDocumentBusiness = new PortalDocumentBusiness();
        /// <summary>
        /// PortalDocumentBusiness
        /// </summary>
        public PortalDocumentBusiness BusinessPortalDocument { get { return _portalDocumentBusiness; } }
        private UserInfoBusiness _userInfoBusiness = new UserInfoBusiness();
        /// <summary>
        /// UserInfo Business
        /// </summary>
        public UserInfoBusiness BusinessUserInfo { get { return _userInfoBusiness; } }

        private ClientUserBusiness _clientUserBusiness = new ClientUserBusiness();
        public ClientUserBusiness BusinessClientUser { get { return _clientUserBusiness; } }

        /// <summary>
        /// PortalId
        /// </summary>
        public int PortalId { get { return ConfigUtility.PortalId; } }
        /// <summary>
        /// 页面大小，用于分页
        /// </summary>
        public int PageSize { get { return 12; } }
        /// <summary>
        /// 页码，用于分页
        /// </summary>
        public int PageNum { get; set; }

        public AdminController()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("/Home/Index");
                Redirect("/Home/Index");
                return;
            }
            //PortalInfo
            ViewData["PortalInfo"] = ConfigUtility.PortalInfo;
            //Menu in current portal
            var portalMenuList = BusinessPortalMenu.GetPortalMenuList(PortalId);
            PortalMenu emptyPortalMenu = new PortalMenu();
            emptyPortalMenu.Id = 0;
            emptyPortalMenu.Name = "Root";
            emptyPortalMenu.DisplayName = "--请选择--";
            emptyPortalMenu.Type = 0;
            if (portalMenuList == null)
            {
                portalMenuList = new List<PortalMenu>();
            }
            portalMenuList.Add(emptyPortalMenu);
            var portalNodeMenuList = portalMenuList.Where(p => p.Type == (int)MenuType.NodeMenu).ToList();
            if (portalNodeMenuList == null)
            {
                portalNodeMenuList = new List<PortalMenu>();
            }
            List<PortalMenu> portalDocMenuList = new List<PortalMenu>();
            portalDocMenuList.Add(emptyPortalMenu);
            portalDocMenuList.AddRange(portalMenuList.Where(p => p.Type == (int)MenuType.DocumentMenu).ToList());
            ViewData[PortalSessionKey.SelectNodePortalMenu.ToString()] = new SelectList(portalNodeMenuList, "Id", "DisplayName");
            ViewData[PortalSessionKey.SelectDocumentPortalMenu.ToString()] = new SelectList(portalDocMenuList, "Id", "DisplayName");
            ViewData[PortalSessionKey.FeatureSelected.ToString()] = string.Empty; //It must be assign value in child control
            //Init ModeSate to every business
            _portalDocumentBusiness.ModelStateDic = ModelState;
            _portalInfoBusiness.ModelStateDic = ModelState;
            _portalMenuBusiness.ModelStateDic = ModelState;

            ViewData[PortalSessionKey.ClientUserPageNum.ToString()] = 1;
        }

        [Authorize]
        public ActionResult Manager()
        {
            ViewData[PortalSessionKey.Message.ToString()] = string.Empty;
            return View();
        }
        [Authorize]
        public ActionResult ClientUser(int? pageNum)
        {
            if (pageNum == null || pageNum < 1)
            {
                pageNum = 1;
            }
            var c = BusinessClientUser.GetClientUserList((int)pageNum, PageSize);
            if (c == null || c.Count == 0)
            {
                pageNum = 0;
            }
            ViewData[PortalSessionKey.ClientUserPageNum.ToString()] = (int)pageNum;
            return View(c);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Manager(FormCollection collection)
        {
            string sqlText = collection["txtSql"];
            if (!BusinessPortalInfo.ExecuteSQLiteSql(sqlText))
            {
                if (BusinessPortalInfo.ModelStateDic.Count > 0)
                {
                    ViewData[PortalSessionKey.Message.ToString()] = BusinessPortalInfo.ModelStateDic["ex"].Errors[0].Exception.Message;
                }
                else
                {
                    ViewData[PortalSessionKey.Message.ToString()] = "No record effected!";
                }
            }
            else
            {
                ViewData[PortalSessionKey.Message.ToString()] = "Execute successfully!";
            }
            return View();
        }
    }
}
