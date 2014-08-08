using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Business
{
    public class PortalInfoBusiness : BaseBusiness
    {
        /// <summary>
        /// Get all PortalInfo in db
        /// </summary>
        /// <returns></returns>
        public List<PortalInfo> GetPortalInfo()
        {
            List<PortalInfo> listPortalInfo = new List<PortalInfo>();
            var c = from p in DBContext.PortalInfo
                    select p;
            if (c != null && c.Count() > 0)
            {
                listPortalInfo = c.ToList();
            }
            return listPortalInfo;
        }
        /// <summary>
        /// Get PortalInfo by id, if not find, return a empty PortalInfo object
        /// </summary>
        /// <param name="portalId"></param>
        /// <returns></returns>
        public PortalInfo GetPortalInfo(int portalId)
        {
            List<PortalInfo> listPortalInfo = GetPortalInfo();
            listPortalInfo = listPortalInfo.Where(p => p.Id == portalId).ToList();
            if (listPortalInfo.Count > 0)
            {
                return listPortalInfo.SingleOrDefault();
            }
            else
            {
                return new PortalInfo();
            }
        }
        /// <summary>
        /// Add a new PortalInfo
        /// </summary>
        /// <param name="portalInfo"></param>
        /// <returns></returns>
        public bool AddPortalInfo(PortalInfo portalInfo)
        {
            if (ValidatePortalInfo(portalInfo))
            {
                var c = DBContext.PortalInfo.OrderByDescending(p => p.Id).FirstOrDefault();
                if (c == null)
                {
                    portalInfo.Id = 1;
                }
                else
                {
                    portalInfo.Id = c.Id + 1;
                }
                //DBContext.AddToPortalInfo(portalInfo);
                //DBContext.SaveChanges();
                string sqlText = string.Empty;
                sqlText = string.Format("insert into PortalInfo(Id,Name,DisplayName,Keyword,Url,ImageUrl,Description) values ({0},'{1}','{2}','{3}','{4}','{5}','{6}')",
                    portalInfo.Id, portalInfo.Name, portalInfo.DisplayName, portalInfo.Keyword, portalInfo.Url, portalInfo.ImageUrl, portalInfo.Description);
                ExecuteSQLiteSql(sqlText);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Update portalInfo
        /// </summary>
        /// <param name="portalInfo"></param>
        /// <returns></returns>
        public bool UpdatePortalInfo(PortalInfo portalInfo)
        {
            if (ValidatePortalInfo(portalInfo))
            {
                PortalInfo originalPortalInfo = GetPortalInfo(portalInfo.Id);
                //DBContext.ApplyPropertyChanges(originalPortalInfo.EntityKey.EntitySetName, portalInfo);
                //DBContext.SaveChanges();
                string sqlText = string.Empty;
                sqlText = string.Format("update PortalInfo set Name='{1}',DisplayName='{1}',Keyword='{2}',Url='{3}',ImageUrl='{4}',Description='{5}' where Id={0}",
                   portalInfo.Id, portalInfo.Name, portalInfo.DisplayName, portalInfo.Keyword, portalInfo.Url, portalInfo.ImageUrl, portalInfo.Description);
                ExecuteSQLiteSql(sqlText);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Delete a ProtalInfo
        /// </summary>
        /// <param name="portalInfoId"></param>
        public void DeletePortalInfo(int portalInfoId)
        {
            PortalInfo portalInfo = GetPortalInfo(portalInfoId);
            //DBContext.DeleteObject(portalInfo);
            //DBContext.SaveChanges();
            string sqlText = string.Empty;
            sqlText = string.Format("delete from PortalInfo where Id={0}", portalInfo.Id);
            ExecuteSQLiteSql(sqlText);
        }
        /// <summary>
        /// Validate the portalInfo
        /// </summary>
        /// <param name="portalInfo"></param>
        /// <returns></returns>
        public bool ValidatePortalInfo(PortalInfo portalInfo)
        {
            CleanErrorDic();
            if (string.IsNullOrEmpty(portalInfo.Name))
            {
                ModelStateDic.AddModelError("Name", "Name should not be empty.");
            }
            if (string.IsNullOrEmpty(portalInfo.Keyword))
            {
                ModelStateDic.AddModelError("Keyword", "Keyword should not be empty.");
            }
            List<PortalInfo> allPortalInfo = GetPortalInfo();
            //validate update portalinfo
            if (portalInfo.Id != 0)
            {
                var c = allPortalInfo.Where(p => p.Id != portalInfo.Id && p.Keyword == portalInfo.Keyword);
                if (c != null && c.Count() > 0)
                {
                    ModelStateDic.AddModelError("Keyword", "The Keyword has already exist.");
                }
            }
            else
            {
                var c = allPortalInfo.Where(p => p.Keyword == portalInfo.Keyword);
                if (c != null && c.Count() > 0)
                {
                    ModelStateDic.AddModelError("Keyword", "The Keyword has already exist.");
                }

            }
            return ModelStateDic.IsValid;
        }
    }
}
