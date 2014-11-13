using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Disappearwind.PortalSolution.PortalWeb.Models;
using Disappearwind.PortalSolution.PortalWeb.Utility;

namespace Disappearwind.PortalSolution.PortalWeb.Controllers
{
    public class AdminPortalMenuController : AdminController
    {
        public AdminPortalMenuController()
        {
            ViewData[PortalSessionKey.FeatureSelected.ToString()] = "Menu";
            ViewData[PortalSessionKey.MenuListPageNum.ToString()] = 1;
        }
        //
        // GET: /AdminMenu/

        public ActionResult Index(int? pageNum)
        {
            var c = BusinessPortalMenu.GetPortalMenuList(PortalId);
            if (c == null || c.Count == 0)
            {
                c = new List<PortalMenu>();
            }
            if (pageNum == null)
            {
                pageNum = 1;
            }
            ViewData[PortalSessionKey.MenuListPageNum.ToString()] = pageNum;
            return View(c);
        }

        //
        // GET: /AdminMenu/Details/5

        public ActionResult Details(int id)
        {
            var c = BusinessPortalMenu.GetPortalMenu(id);
            return View(c);
        }

        //
        // GET: /AdminMenu/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AdminMenu/Create

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult Create(PortalMenu portalMenuToCreate)
        {
            try
            {
                portalMenuToCreate.CreateDate = DateTime.Now;
                portalMenuToCreate.PortalId = PortalId;
                if (BusinessPortalMenu.AddPortalMenu(portalMenuToCreate))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(portalMenuToCreate);
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminPortalMenuController.Create", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return View(portalMenuToCreate);
            }
        }

        //
        // GET: /AdminMenu/Edit/5

        public ActionResult Edit(int id)
        {
            var c = BusinessPortalMenu.GetPortalMenu(id);
            return View(c);
        }

        //
        // POST: /AdminMenu/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult Edit(PortalMenu portalMenuToEdit)
        {
            try
            {
                portalMenuToEdit.CreateDate = DateTime.Now;
                portalMenuToEdit.PortalId = PortalId;
                if (BusinessPortalMenu.UpdatePortalMenu(portalMenuToEdit))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(portalMenuToEdit);
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminPortalMenuController.Edit", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return View(portalMenuToEdit);
            }
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                BusinessPortalMenu.DeletePortalMenu(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminPortalMenuController.Delete", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
