using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Disappearwind.PortalSolution.PortalWeb.Models;
using Disappearwind.PortalSolution.PortalWeb.Business;

namespace Disappearwind.PortalSolution.PortalWeb.Utility
{
    public static class ConfigUtility
    {
        /// <summary>
        /// Get PortalId from web.config
        /// </summary>
        public static int PortalId
        {
            get
            {
                try
                {
                    return Convert.ToInt32(ConfigurationManager.AppSettings["PortalId"]);
                }
                catch (Exception ex)
                {
                    LogUtility.WriteLog("ConfigUtility.PortalId", ex.Message);
                    return -1;
                } 
            }
        }
        /// <summary>
        /// Current portal information
        /// If no portal matched on the PortalId, then return a empty PortalInfo object
        /// </summary>
        public static PortalInfo PortalInfo 
        {
            get
            {
                try
                {
                    PortalInfoBusiness piBusiness = new PortalInfoBusiness();
                    return piBusiness.GetPortalInfo(PortalId);
                }
                catch(Exception ex)
                {
                    LogUtility.WriteLog("ConfigUtility.PortalInfo", ex.Message);
                    return new PortalInfo();
                }
            }
        }
        /// <summary>
        /// The image type supported for the portal from web.config
        /// </summary>
        public static string ImageType
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["ImageType"];
                }
                catch (Exception ex)
                {
                    LogUtility.WriteLog("ConfigUtility.PortalId", ex.Message);
                    return string.Empty;
                }
            }
        }
    }
}
