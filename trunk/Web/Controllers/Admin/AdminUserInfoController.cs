using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Disappearwind.PortalSolution.PortalWeb.Business;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Controllers.Admin
{
    public class AdminUserInfoController : AdminController
    {
        //
        // GET: /AdminUserInfo/

        public ActionResult Index()
        {
            var c = BusinessUserInfo.GetUser();
            return View(c);
        }
    }
}
