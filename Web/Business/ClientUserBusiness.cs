using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Business
{
    /// <summary>
    /// 客户端 用户相关的逻辑
    /// </summary>
    public class ClientUserBusiness : BaseBusiness
    {
        /// <summary>
        /// Get all client user 
        /// </summary>
        public List<ClientUser> GetClientUserList(int pageNum, int pageSize)
        {
            var c = from p in DBContext.ClientUser
                    orderby p.UserID descending
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return new List<ClientUser>();
            }
        }
        /// <summary>
        /// Get User by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ClientUser GetClientUser(string username)
        {
            var c = from p in DBContext.ClientUser
                    where p.UserName == username
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.FirstOrDefault();
            }
            else
            {
                return new ClientUser();
            }
        }
        /// <summary>
        /// Get User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientUser GetClientUser(int id)
        {
            var c = from p in DBContext.ClientUser
                    where p.UserID == id
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.FirstOrDefault();
            }
            else
            {
                return new ClientUser();
            }
        }
        /// <summary>
        /// Add a new ClientUser to database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int AddClientUser(ClientUser user)
        {
            var c = DBContext.ClientUser.OrderByDescending(p => p.UserID).FirstOrDefault();
            if (c == null)
            {
                user.UserID = 1;
            }
            else
            {
                user.UserID = c.UserID + 1;
            }
            user.Score = 100;
            user.CreateDate = DateTime.Now.ToString();
            string sqlText = string.Empty;
            sqlText = string.Format(@"insert into ClientUser ( UserID , UserName , Pwd ,NickName, Phone,DeviceNum,Score,CreateDate) 
                                                  values ( {0} , '{1}' , '{2}' , '{3}' , '{4}' ,'{5}','{6}','{7}')",
               user.UserID, user.UserName, user.Pwd, user.NickName, user.Phone, user.DeviceNum, user.Score, user.CreateDate);
            ExecuteSQLiteSql(sqlText);
            return user.UserID;
        }
        /// <summary>
        /// Add a new ClientUser Login info to database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddClientUserLogin(ClientUserLogin userLogin)
        {
            userLogin.CreateDate = DateTime.Now.ToString();
            string sqlText = string.Empty;
            sqlText = string.Format(@"insert into ClientUserLogin ( UserID , CreateDate , IP ,DeviceNum) 
                                                  values ( {0} , '{1}' , '{2}' , '{3}')",
               userLogin.UserID, userLogin.CreateDate, userLogin.IP, userLogin.DeviceNum);
            ExecuteSQLiteSql(sqlText);
            return true;
        }
        /// <summary>
        /// 更新用户的积分，并返回更新后的积分
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public int UpdateClientUserScore(int userid, int score)
        {
            ClientUser userInfo = GetClientUser(userid);
            userInfo.Score += score;
            string sqlText = string.Empty;
            sqlText = string.Format(@"update ClientUser set Score={0} where UserId={1}",
               userInfo.Score, userid);
            ExecuteSQLiteSql(sqlText);
            return (int)userInfo.Score;
        }
    }
}
