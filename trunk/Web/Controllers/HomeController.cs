using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Disappearwind.PortalSolution.PortalWeb.Utility;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        public HomeController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int? id, int? pageNum)
        {
            PortalMenu menu = new PortalMenu();
            try
            {
                if (pageNum == null)
                {
                    pageNum = 1;
                }
                menu = BusinessPortalMenu.GetPortalMenu((int)id);
                ViewData[PortalSessionKey.PortalListPageNum.ToString()] = pageNum;
            }
            catch (Exception ex)
            {
                return RedirectToAction("../Error/ApplicationError?ex=" + ex.Message);
            }
            return View(menu);
        }

        public ActionResult MoreList(string keyword, int? pageNum)
        {
            var c = CommonUtility.GetDocument(keyword);
            if (pageNum == null)
            {
                pageNum = 1;
            }
            ViewData[PortalSessionKey.PortalListPageNum.ToString()] = pageNum;
            return View(c);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            PortalDocument doc = new PortalDocument();
            try
            {
                doc = BusinessPortalDocument.GetPortalDocument(id);
            }
            catch
            {

            }
            return View(doc);
        }
        public ActionResult SecondPage(int id)
        {
            PortalDocument doc = new PortalDocument();
            try
            {
                doc = BusinessPortalDocument.GetPortalDocument(id);
            }
            catch
            {

            }
            return View(doc);
        }

        public ActionResult Default()
        {
            return View();
        }
    }
}
