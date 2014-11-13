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
    public class AdminAlbumController : AdminController
    {
        private AlbumBusiness _albumBusiness = new AlbumBusiness();
        //
        // GET: /AdminAlbum/

        public AdminAlbumController()
        {
            ViewData[PortalSessionKey.AlbumListPageNum.ToString()] = 1;
        }

        public ActionResult Index(int? pageNum)
        {
            if (pageNum == null || pageNum < 1)
            {
                pageNum = 1;
            }
            var c = _albumBusiness.GetAlbumList((int)pageNum, PageSize);
            if (c == null || c.Count == 0)
            {
                pageNum = 0;
            }
            ViewData[PortalSessionKey.AlbumListPageNum.ToString()] = pageNum;
            return View(c);
        }

        //
        // GET: /AdminAlbum/Details/5

        public ActionResult Details(int id)
        {
            var c = _albumBusiness.GetAlbum(id);
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
        [Authorize]
        public ActionResult Create(Album album)
        {
            try
            {
                if (_albumBusiness.AddAlbum(album) > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(album);
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminAlbumController.Create", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return View(album);
            }
        }

        //
        // GET: /AdminAlbum/Edit/5

        public ActionResult Edit(int id)
        {
            var c = _albumBusiness.GetAlbum(id);
            return View(c);
        }

        //
        // POST: /AdminAlbum/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Album album)
        {
            try
            {
                if (_albumBusiness.UpdateAlbum(album))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(album);
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminAlbumController.Edit", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return View(album);
            }
        }

        //
        // GET: /AdminAlbum/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                _albumBusiness.DeleteAlbum(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog("AdminAlbumController.Delete", ex.Message);
                ModelState.AddModelError("Exception", ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
