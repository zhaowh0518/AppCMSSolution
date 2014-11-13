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
    public class AdminPurchaseController : AdminController
    {
        private PurchaseBusiness _purchaseBusiness = new PurchaseBusiness();

        public AdminPurchaseController()
        {
            ViewData[PortalSessionKey.PurchasePageNum.ToString()] = 1;
        }
        //
        // GET: /AdminPurchase/

        public ActionResult Index(int? pageNum)
        {
            if (pageNum == null || pageNum < 1)
            {
                pageNum = 1;
            }
            var c = _purchaseBusiness.GetProductList((int)pageNum, PageSize);
            if (c == null || c.Count == 0)
            {
                pageNum = 0;
            }
            ViewData[PortalSessionKey.PurchasePageNum.ToString()] = pageNum;
            return View(c);
        }

        //
        // GET: /AdminPurchase/Details/5

        public ActionResult Details(int id)
        {
            var c = _purchaseBusiness.GetProduct(id);
            return View(c);
        }

        //
        // GET: /AdminPurchase/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AdminPurchase/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(PurchaseProduct product)
        {
            try
            {
                if (_purchaseBusiness.AddProduct(product) > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminPurchaseController.Create", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return View(product);
            }
        }

        //
        // GET: /AdminPurchase/Edit/5

        public ActionResult Edit(int id)
        {
            var c = _purchaseBusiness.GetProduct(id);
            return View(c);
        }

        //
        // POST: /AdminPurchase/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(PurchaseProduct product)
        {
            try
            {
                if (_purchaseBusiness.UpdateProduct(product))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminPurchaseController.Edit", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return View(product);
            }
        }

        //
        // GET: /AdminPurchase/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                _purchaseBusiness.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminPurchaseController.Delete", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /AdminPurchase/Delete/5

        [HttpPost]
        [Authorize]
        public ActionResult UpdateProductState(string idList, bool state)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
