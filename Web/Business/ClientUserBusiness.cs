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
            UpdateScoreForLogin(userLogin.UserID);
            userLogin.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string sqlText = string.Empty;
            sqlText = string.Format(@"insert into ClientUserLogin ( UserID , CreateDate , IP ,DeviceNum) 
                                                  values ( {0} , '{1}' , '{2}' , '{3}')",
               userLogin.UserID, userLogin.CreateDate, userLogin.IP, userLogin.DeviceNum);
            ExecuteSQLiteSql(sqlText);
            return true;
        }
        /// <summary>
        /// 根据用户名和密码登录，返回用户ID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int UserLogin(string username, string pwd)
        {
            var c = from p in DBContext.ClientUser
                    where p.UserName == username && p.Pwd == pwd
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.FirstOrDefault().UserID;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 浏览专辑更新用户的积分，并返回更新后的积分
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public int UpdateScoreForViewAlbum(int albumid, int userid, int score)
        {
            ClientUser userInfo = GetClientUser(userid);
            if (userInfo.Score == null)
            {
                userInfo.Score = 0;
            }
            userInfo.Score -= score;
            UpdateScore(userid, (int)userInfo.Score);
            //记录用户消耗gold的历史
            AlbumView view = new AlbumView();
            view.Gold = score;
            view.UserID = userid;
            view.AlbumID = albumid;
            new AlbumBusiness().AddAlbumView(view);
            return (int)userInfo.Score;
        }
        /// <summary>
        /// 购买订单后记录用户的金币量
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="productid"></param>
        /// <returns></returns>
        public void UpdateScoreForProduct(int userid, string productid)
        {
            ClientUser userInfo = GetClientUser(userid);
            if (userInfo.Score == null)
            {
                userInfo.Score = 0;
            }
            userInfo.Score += new PurchaseBusiness().GetProduct(productid).Gold;
            UpdateScore(userid, (int)userInfo.Score);
        }
        /// <summary>
        /// 每天第一次登录，奖励2个金币
        /// </summary>
        /// <param name="userid"></param>
        public void UpdateScoreForLogin(int userid)
        {
            ClientUser userInfo = GetClientUser(userid);
            if (userInfo.Score == null)
            {
                userInfo.Score = 0;
            }
            if (!IsLoginToday(userid))
            {
                userInfo.Score += 2;
                UpdateScore(userid, (int)userInfo.Score);
            }
        }
        /// <summary>
        ///更新用户的金币到制定的数目
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="score">金币数目</param>
        public void UpdateScore(int userid, int score)
        {
            string sqlText = string.Empty;
            sqlText = string.Format(@"update ClientUser set Score={0} where UserId={1}",
               score, userid);
            ExecuteSQLiteSql(sqlText);
        }
        /// <summary>
        /// 更新用户的昵称
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public void UpdateNickName(int userid, string nickname)
        {

            string sqlText = string.Empty;
            sqlText = string.Format(@"update ClientUser set NickName='{0}' where UserId={1}",
              nickname, userid);
            ExecuteSQLiteSql(sqlText);
        }
        /// <summary>
        /// 判断用户是否当天登录
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        private bool IsLoginToday(int userid)
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            var c = DBContext.ClientUserLogin.Where(p => p.CreateDate.Contains(currentDate) && p.UserID == userid);
            if (c != null && c.Count() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
