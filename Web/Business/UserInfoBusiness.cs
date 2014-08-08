using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Business
{
    /// <summary>
    /// User info manager
    /// </summary>
    public class UserInfoBusiness : BaseBusiness
    {
        /// <summary>
        /// Return all user
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetUser()
        {
            var c = from p in DBContext.UserInfo
                    orderby p.CreateDate descending
                    select p;
            if (c != null)
            {
                return c.ToList();
            }
            else
            {
                return new List<UserInfo>();
            }
        }
        /// <summary>
        /// Add a user to db
        /// </summary>
        /// <param name="userInfo"></param>
        public void AddUser(UserInfo userInfo)
        {
            var c = DBContext.UserInfo.OrderByDescending(p => p.UserId).FirstOrDefault();
            if (c != null)
            {
                userInfo.UserId = c.UserId + 1;
            }
            else
            {
                userInfo.UserId = 1;
            }
            userInfo.CreateDate = DateTime.Now;
            //DBContext.AddToUserInfo(userInfo);
            //DBContext.SaveChanges();
            string sqlText = string.Empty;
            sqlText = string.Format(@"insert into UserInfo(UserId,UserName,Pwd,DisplayName,Phone,Email,MSN,QQ,CreateDate) 
                                        values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                        userInfo.UserId,userInfo.UserName,userInfo.Pwd,userInfo.DisplayName,userInfo.Phone,userInfo.Email,userInfo.MSN,userInfo.QQ ,DateTime.Now.ToString("yyyy-MM-dd"));
            ExecuteSQLiteSql(sqlText);
        }

        /// <summary>
        /// Judge if the userName has been used
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsExist(string userName)
        {
            var c = from p in DBContext.UserInfo
                    where p.UserName == userName
                    select p;
            if (c == null || c.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Login(string userName, string password)
        {
            var c = from p in DBContext.UserInfo
                    where p.UserName == userName && p.Pwd == password
                    select p;
            if (c == null || c.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
