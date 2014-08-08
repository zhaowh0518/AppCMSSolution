using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Business
{
    /// <summary>
    /// Business for PortalMenu
    /// </summary>
    public class PortalMenuBusiness : BaseBusiness
    {
        /// <summary>
        /// Get PortalMenu by keywor 
        /// </summary>
        /// <param name="keyWord">keyword</param>
        /// <returns></returns>
        public PortalMenu GetPortalMenu(string keyWord)
        {
            var c = from p in DBContext.PortalMenu
                    where p.Keyword == keyWord
                        & p.State == true
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.FirstOrDefault();
            }
            else
            {
                return new PortalMenu();
            }
        }
        /// <summary>
        /// Get all children of current menu
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public List<PortalMenu> GetChildrenPortalMenu(PortalMenu menu)
        {
            var c = from p in DBContext.PortalMenu
                    where p.ParentMenu == menu.Id
                        & p.State == true
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.ToList();
            }
            else
            {
                return new List<PortalMenu>();
            }
        }
        /// <summary>
        /// Get PortalMenu by portalId, will return all PortalMenu in the portal
        /// </summary>
        /// <param name="portalId"></param>
        /// <returns></returns>
        public List<PortalMenu> GetPortalMenuList(int portalId)
        {
            var c = from p in DBContext.PortalMenu
                    where p.PortalId == portalId
                    select p;
            if (c == null || c.Count() == 0)
            {
                return new List<PortalMenu>();
            }
            else
            {
                return c.ToList();
            }
        }
        /// <summary>
        /// Get PortalMenu by menuId,if not exist return a empty PortalMenu object
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public PortalMenu GetPortalMenu(int menuId)
        {
            var c = from p in DBContext.PortalMenu
                    where p.Id == menuId
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.FirstOrDefault();
            }
            else
            {
                return new PortalMenu();
            }
        }
        /// <summary>
        /// Add a new PortalMenu to database
        /// </summary>
        /// <param name="portalMenu"></param>
        /// <returns></returns>
        public bool AddPortalMenu(PortalMenu portalMenu)
        {
            if (ValidatePortalMenu(portalMenu))
            {
                var c = DBContext.PortalMenu.OrderByDescending(p => p.Id).FirstOrDefault();
                if (c == null)
                {
                    portalMenu.Id = 1;
                }
                else
                {
                    portalMenu.Id = c.Id + 1;
                }
                portalMenu.State = true;
                portalMenu.Sequence = 100;
                portalMenu.CreateDate = DateTime.Now.Date;
                //DBContext.AddToPortalMenu(portalMenu);
                //DBContext.SaveChanges();
                string sqlText = string.Empty;
                sqlText = string.Format(@"insert into PortalMenu ( Id , Name , DisplayName , Keyword , Description, Type , ParentMenu , PortalId , Url , ImageUrl , Sequence , State ) 
                                                  values ( {0} , '{1}' , '{2}' , '{3}' , '{4}' ,{5} , {6} , {7} , '{8}' , '{9}' , {10} , '{11}')",
                    portalMenu.Id, portalMenu.Name, portalMenu.DisplayName, portalMenu.Keyword, portalMenu.Description, portalMenu.Type, portalMenu.ParentMenu, portalMenu.PortalId, portalMenu.Url,
                    portalMenu.ImageUrl, portalMenu.Sequence, 1);
                ExecuteSQLiteSql(sqlText);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Update a PotalMenu
        /// </summary>
        /// <param name="portalMenu"></param>
        /// <returns></returns>
        public bool UpdatePortalMenu(PortalMenu portalMenu)
        {
            if (ValidatePortalMenu(portalMenu))
            {
                PortalMenu originalPortalMenu = GetPortalMenu(portalMenu.Id);
                //DBContext.ApplyPropertyChanges(originalPortalMenu.EntityKey.EntitySetName, portalMenu);
                //DBContext.SaveChanges();
                string sqlText = string.Empty;
                sqlText = string.Format(@"Update PortalMenu set Name='{1}' , DisplayName='{2}' , Keyword='{3}' , Description='{4}', Type={5} , ParentMenu={6} , PortalId={7} , Url='{8}' , ImageUrl='{9}' , Sequence={10} , State='{11}' where Id={0}",
                    portalMenu.Id, portalMenu.Name, portalMenu.DisplayName, portalMenu.Keyword, portalMenu.Description, portalMenu.Type, portalMenu.ParentMenu, portalMenu.PortalId, portalMenu.Url,
                    portalMenu.ImageUrl, portalMenu.Sequence, Convert.ToInt32(portalMenu.State));
                ExecuteSQLiteSql(sqlText);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Delete a PortalMenu
        /// </summary>
        /// <param name="menuId"></param>
        public void DeletePortalMenu(int menuId)
        {
            PortalMenu originalPortalMenu = GetPortalMenu(menuId);
            //DBContext.DeleteObject(originalPortalMenu);
            //DBContext.SaveChanges();
            string sqlText = string.Empty;
            sqlText = string.Format(@"delete from PortalMenu where ID={0}", menuId);
            ExecuteSQLiteSql(sqlText);
        }
        /// <summary>
        /// Validate the PortalMenu
        /// </summary>
        /// <param name="portalMenu"></param>
        /// <returns></returns>
        public bool ValidatePortalMenu(PortalMenu portalMenu)
        {
            CleanErrorDic();
            return ModelStateDic.IsValid;
        }
    }
}
