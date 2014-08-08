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
    public class AdminPortalInfoController : AdminController
    {
        public AdminPortalInfoController()
        {
            ViewData[PortalSessionKey.FeatureSelected.ToString()] = "PortalInfo";
        }
        //
        // GET: /AdminPortalInfo/

        public ActionResult Index()
        {
            var c = BusinessPortalInfo.GetPortalInfo();
            return View(c);
        }

        //
        // GET: /AdminPortalInfo/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /AdminPortalInfo/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(PortalInfo portalInfoToCreate)
        {
            try
            {
                if (BusinessPortalInfo.AddPortalInfo(portalInfoToCreate))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(portalInfoToCreate);
                }
            }
            catch(Exception ex)
            {
                LogUtility.WriteLog("AdminPortalInfoController.Create", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return View();
            }
        }

        //
        // GET: /AdminPortalInfo/Edit/5
 
        public ActionResult Edit(int id)
        {
            PortalInfo portalInfo = BusinessPortalInfo.GetPortalInfo(id);
            return View(portalInfo);
        }

        //
        // POST: /AdminPortalInfo/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(PortalInfo portalInfoToEdit)
        {
            try
            {
                BusinessPortalInfo.UpdatePortalInfo(portalInfoToEdit);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                LogUtility.WriteLog("AdminPortalInfoController.Edit", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            PortalInfo portalInfo = BusinessPortalInfo.GetPortalInfo(id);
            return View(portalInfo);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                BusinessPortalInfo.DeletePortalInfo(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminPortalInfoController.Delete", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
