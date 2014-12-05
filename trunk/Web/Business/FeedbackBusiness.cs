using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Business
{
    /// <summary>
    /// Business for Feedback
    /// </summary>
    public class FeedbackBusiness : BaseBusiness
    {
        /// <summary>
        /// 新增反馈
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        public int AddFeedback(Feedback feedback)
        {
            var c = DBContext.Feedback.OrderByDescending(p => p.ID).FirstOrDefault();
            if (c == null)
            {
                feedback.ID = 1;
            }
            else
            {
                feedback.ID = c.ID + 1;
            }
            string sqlText = string.Empty;
            sqlText = string.Format(@"insert into Feedback ( ID,UserID,Content,CreateDate) 
                                                  values ( {0} , '{1}' , '{2}' , '{3}')",
                feedback.ID, feedback.UserID, feedback.Content, DateTime.Now.ToString("s"));
            ExecuteSQLiteSql(sqlText);
            return feedback.ID;
        }
        /// <summary>
        /// 获取所有的用户反馈
        /// </summary>
        /// <returns></returns>
        public List<Feedback> GetFeedbackList()
        {
            var c = from p in DBContext.Feedback
                    orderby p.ID descending
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.ToList();
            }
            else
            {
                return new List<Feedback>();
            }
        }
    }
}
