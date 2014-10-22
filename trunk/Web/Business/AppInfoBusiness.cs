using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Business
{
    /// <summary>
    /// Business for AppInfo
    /// </summary>
    public class AppInfoBusiness : BaseBusiness
    {
        /// <summary>
        /// 获取所有的AppInfo
        /// </summary>
        /// <returns></returns>
        public List<AppInfo> GetAppInfoList()
        {
            var c = from p in DBContext.AppInfo
                    orderby p.ID descending
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.ToList();
            }
            else
            {
                return new List<AppInfo>();
            }
        }
        /// <summary>
        /// Get AppInfo by id,if not exist return a empty AppInfo object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AppInfo GetAppInfo(int id)
        {
            var c = from p in DBContext.AppInfo
                    where p.ID == id
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.FirstOrDefault();
            }
            else
            {
                return new AppInfo();
            }
        }
        /// <summary>
        /// Add a new AppInfo to database
        /// </summary>
        /// <param name="appInfo"></param>
        /// <returns></returns>
        public int AddAppInfo(AppInfo appInfo)
        {
            var c = DBContext.AppInfo.OrderByDescending(p => p.ID).FirstOrDefault();
            if (c == null)
            {
                appInfo.ID = 1;
            }
            else
            {
                appInfo.ID = c.ID + 1;
            }
            string sqlText = string.Empty;
            sqlText = string.Format(@"insert into AppInfo ( Id , Name , Keyword ,Version, VersionUpgrade,Description) 
                                                  values ( {0} , '{1}' , '{2}' , '{3}' , '{4}' ,'{5}')",
                appInfo.ID, appInfo.Name, appInfo.Keyword, appInfo.Version, appInfo.VersionUpgrade, appInfo.Description);
            ExecuteSQLiteSql(sqlText);
            return appInfo.ID;
        }
        /// <summary>
        /// Update a AppInfo
        /// </summary>
        /// <param name="appInfo"></param>
        /// <returns></returns>
        public bool UpdateAppInfo(AppInfo appInfo)
        {
            string sqlText = string.Empty;
            sqlText = string.Format(@"Update AppInfo set Name='{1}' , Description='{2}',  Version='{3}',VersionUpgrade='{4}'  where Id={0}",
                appInfo.ID, appInfo.Name, appInfo.Description, appInfo.Version, appInfo.VersionUpgrade);
            ExecuteSQLiteSql(sqlText);
            return true;
        }
        /// <summary>
        /// Delete a AppInfo
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAppInfo(int id)
        {
            string sqlText = string.Format(@"delete from AppInfo where ID={0}", id);
            ExecuteSQLiteSql(sqlText);
        }
    }
}
