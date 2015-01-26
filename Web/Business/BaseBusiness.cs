using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Business
{
    /// <summary>
    /// Base business, definetion some properties used by other business class
    /// </summary>
    public abstract class BaseBusiness
    {
        private static Entities _db = new Entities();

        /// <summary>
        /// Database context
        /// </summary>
        public static Entities DBContext
        {
            get
            {
                return _db;
            }
        }
        /// <summary>
        /// ModelStateDictionary 
        /// </summary>
        public ModelStateDictionary ModelStateDic { get; set; }
        /// <summary>
        /// Clear all error message in ModelStateDic
        /// </summary>
        protected void CleanErrorDic()
        {
            foreach (var item in ModelStateDic.Values)
            {
                item.Errors.Clear();
            }
        }
        /// <summary>
        /// Excute sql text, for quick update, insert and delete
        /// </summary>
        /// <param name="connStr">connection string</param>
        /// <param name="sqlText">sql text to be executed</param>
        /// <returns></returns>
        public bool ExecuteSQLServerSql(string connStr, string sqlText)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = connStr;
                SqlCommand cmd = new SqlCommand(sqlText, conn);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ModelState ms = new ModelState();
                ms.Errors.Add(ex);
                ModelStateDic.Add("ex", ms);
                return false;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        public bool ExecuteSQLiteSql(string sqlText)
        {
            sqlText = EscapeSQLiteText(sqlText);
            DataAccess da = new DataAccess();
            da.ExcuteSQLText(sqlText);
            return true;
        }
        /// <summary>
        /// 处理SQL语句中的转义字符
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        private string EscapeSQLiteText(string sqlText)
        {
            return sqlText
                // .Replace("/", "//")
                 .Replace("//", "/")
                .Replace("[", "/[")
                .Replace("]", "/]")
                .Replace("%", "/%")
                .Replace("&", "/&")
                .Replace("_", "/_");
        }
    }
}
