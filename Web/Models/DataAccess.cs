using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SQLite;

namespace Disappearwind.PortalSolution.PortalWeb.Models
{
    /// <summary>
    /// Access database use base sql connection
    /// </summary>
    public class DataAccess
    {
        /// <summary>
        /// Execute sql text to db,for insert,update and delete
        /// </summary>
        /// <param name="sqlText"></param>
        public void ExcuteSQLText(string sqlText)
        {
            string datasource = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\portaldb.db3";
            //string datasource = System.Configuration.ConfigurationManager.ConnectionStrings["Entities"].ConnectionString;
            SQLiteConnection conn = new SQLiteConnection();
            SQLiteConnectionStringBuilder connStr = new SQLiteConnectionStringBuilder();
            connStr.DataSource = datasource;
            connStr.Password = "!QAZXSW@";
            conn.ConnectionString = connStr.ToString();
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = sqlText;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Clone();
        }
    }
}
