using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Disappearwind.PortalSolution.PortalWeb.Business;
using Disappearwind.PortalSolution.PortalWeb.Models;
using Disappearwind.PortalSolution.PortalWeb.Utility;

namespace Disappearwind.PortalSolution.PortalWeb.Controllers.Admin
{
    public class AdminAppInfoController : AdminController
    {
        private AppInfoBusiness _appInfoBusiness = new AppInfoBusiness();
        //
        // GET: /AdminAlbum/

        public AdminAppInfoController()
        {

        }

        public ActionResult Index(int? pageNum)
        {
            var c = _appInfoBusiness.GetAppInfoList();
            return View(c);
        }

        //
        // GET: /AdminAlbum/Details/5

        public ActionResult Details(int id)
        {
            var c = _appInfoBusiness.GetAppInfo(id);
            return View(c);
        }

        //
        // GET: /AdminAlbum/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AdminAlbum/Create

        [HttpPost]
        public ActionResult Create(AppInfo appInfo)
        {
            try
            {
                if (_appInfoBusiness.AddAppInfo(appInfo) > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(appInfo);
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminAppInfoController.Create", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return View(appInfo);
            }
        }

        //
        // GET: /AdminAlbum/Edit/5

        public ActionResult Edit(int id)
        {
            var c = _appInfoBusiness.GetAppInfo(id);
            return View(c);
        }

        //
        // POST: /AdminAlbum/Edit/5

        [HttpPost]
        public ActionResult Edit(AppInfo appInfo)
        {
            try
            {
                if (_appInfoBusiness.UpdateAppInfo(appInfo))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(appInfo);
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminAppInfoController.Edit", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return View(appInfo);
            }
        }

        //
        // GET: /AdminAlbum/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                _appInfoBusiness.DeleteAppInfo(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminAppInfoController.Delete", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
