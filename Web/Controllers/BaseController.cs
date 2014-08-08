using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Disappearwind.PortalSolution.PortalWeb.Business;
using Disappearwind.PortalSolution.PortalWeb.Utility;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Controllers
{
    public class BaseController : Controller
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

        /// <summary>
        /// PortalId
        /// </summary>
        public int PortalId { get { return ConfigUtility.PortalId; } }

        public BaseController()
        {
            List<PortalDocument> navMenuList = BusinessPortalDocument.GetPortalDocumentList("DA_NavMenu");
            ViewData[PortalSessionKey.NavMenuList.ToString()] = navMenuList;
        }
    }
}
