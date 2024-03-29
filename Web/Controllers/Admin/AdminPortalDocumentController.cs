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
    public class AdminPortalDocumentController : AdminController
    {
        public AdminPortalDocumentController()
        {
            ViewData[PortalSessionKey.FeatureSelected.ToString()] = "Document";
            //Session[PortalSessionKey.SelectedPortalMenu.ToString()] = 0;
            ViewData[PortalSessionKey.DocumentListPageNum.ToString()] = 1;
        }
        //
        // GET: /AdminDocument/

        public ActionResult Index(int? pageNum)
        {
            //List<PortalDocument> emptyList = new List<PortalDocument>();
            //return View(emptyList);
            int menuId = 0;
            if (Session[PortalSessionKey.SelectedPortalMenu.ToString()] != null)
            {
                menuId = Convert.ToInt32(Session[PortalSessionKey.SelectedPortalMenu.ToString()]);
                Session[PortalSessionKey.SelectedPortalMenu.ToString()] = menuId;
            }
            var c = BusinessPortalDocument.GetPortalDocumentList(menuId);
            if (pageNum == null)
            {
                pageNum = 1;
            }
            ViewData[PortalSessionKey.DocumentListPageNum.ToString()] = pageNum;
            return View(c);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(FormCollection collection, int? pageNum)
        {
            int menuId = 0;
            if (!string.IsNullOrEmpty(collection[0]))
            {
                menuId = Convert.ToInt32(collection[0]);
                Session[PortalSessionKey.SelectedPortalMenu.ToString()] = menuId;
            }
            else if (Session[PortalSessionKey.SelectedPortalMenu.ToString()] != null)
            {
                menuId = Convert.ToInt32(Session[PortalSessionKey.SelectedPortalMenu.ToString()]);
            }
            var c = BusinessPortalDocument.GetPortalDocumentList(menuId);
            if (pageNum == null)
            {
                pageNum = 1;
            }
            ViewData[PortalSessionKey.DocumentListPageNum.ToString()] = pageNum;
            return View(c);
        }

        //
        // GET: /AdminDocument/Details/5

        public ActionResult Details(int id)
        {
            var c = BusinessPortalDocument.GetPortalDocument(id);
            return View(c);
        }

        //
        // GET: /AdminDocument/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AdminDocument/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateTemp(PortalDocument portalDocumentToCreate)
        {
            try
            {
                portalDocumentToCreate.CreateDate = DateTime.Now;
                PortalMenu menu = new PortalMenu();
                int menuId = 0;
                if (TempData[PortalSessionKey.SelectedPortalMenu.ToString()] != null)
                {
                    menuId = Convert.ToInt32(TempData[PortalSessionKey.SelectedPortalMenu.ToString()]);
                    menu = BusinessPortalMenu.GetPortalMenu(menuId);
                }
                portalDocumentToCreate.MenuId = menuId;
                if (BusinessPortalDocument.AddPortalDocument(portalDocumentToCreate))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(portalDocumentToCreate);
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminPortalDocumentController.Create.", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return View(portalDocumentToCreate);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult Create(FormCollection collection)
        {
            PortalDocument portalDocumentToCreate = new PortalDocument();
            try
            {
                portalDocumentToCreate.CreateDate = DateTime.Now;
                portalDocumentToCreate.Description = collection["Description"];
                portalDocumentToCreate.DisplayName = collection["DisplayName"];
                portalDocumentToCreate.ImageUrl = collection["ImageUrl"];
                portalDocumentToCreate.Name = collection["Name"];
                portalDocumentToCreate.Sequence = Convert.ToInt32(collection["Sequence"]);
                portalDocumentToCreate.State = Convert.ToBoolean(collection["State"].Split(',')[0]);
                portalDocumentToCreate.Url = collection["Url"];
                portalDocumentToCreate.Extend1 = collection["Extend1"];
                portalDocumentToCreate.Extend2 = collection["Extend2"];
                portalDocumentToCreate.Extend3 = collection["Extend3"];
                PortalMenu menu = new PortalMenu();
                int menuId = Convert.ToInt32(collection["MenuId"]);
                //menu = BusinessPortalMenu.GetPortalMenu(menuId);
                portalDocumentToCreate.MenuId = menuId;
                if (BusinessPortalDocument.AddPortalDocument(portalDocumentToCreate))
                {
                    TempData[PortalSessionKey.SelectedPortalMenu.ToString()] = menuId;
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(portalDocumentToCreate);
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminPortalDocumentController.Create.", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return View(portalDocumentToCreate);
            }
        }

        //
        // GET: /AdminDocument/Edit/5

        public ActionResult Edit(int id)
        {
            var c = BusinessPortalDocument.GetPortalDocument(id);
            return View(c);
        }

        //
        // POST: /AdminDocument/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit(PortalDocument portalDocumentToEdit)
        {
            try
            {
                portalDocumentToEdit.CreateDate = DateTime.Now;
                if (BusinessPortalDocument.UpdatePortalDocument(portalDocumentToEdit))
                {
                    var c = BusinessPortalDocument.GetPortalDocument(portalDocumentToEdit.Id);
                    TempData[PortalSessionKey.SelectedPortalMenu.ToString()] = c.MenuId;
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(portalDocumentToEdit);
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminPortalDocumentController.Edit.", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return View(portalDocumentToEdit);
            }
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                BusinessPortalDocument.DeletePortalDocument(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminPortalDocumentController.Delete", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
