using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Disappearwind.PortalSolution.PortalWeb.Controllers
{
    public class ErrorController : BaseController
    {
        //
        // GET: /Error/

        public ActionResult DefaultError()
        {
            return View();
        }

        public ActionResult PageNotFoundError()
        {
            return View();
        }

        public ActionResult ApplicationError()
        {
            return View();
        }
    }
}
