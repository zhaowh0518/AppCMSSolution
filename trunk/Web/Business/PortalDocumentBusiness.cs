using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Business
{
    /// <summary>
    /// Business for PortalDocument
    /// </summary>
    public class PortalDocumentBusiness : BaseBusiness
    { /// <summary>
        /// Get PortalDocument by menuId, will return all PortalDocuments in the PortalMenu
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public List<PortalDocument> GetPortalDocumentList(int menuId)
        {
            var c = from p in DBContext.PortalDocument
                    where p.MenuId == menuId
                    select p;
            if (c == null || c.Count() == 0)
            {
                return new List<PortalDocument>();
            }
            else
            {
                return c.ToList();
            }
        }
        /// <summary>
        /// Get all document of current menu by keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<PortalDocument> GetPortalDocumentList(string keyword)
        {
            var c = from p in DBContext.PortalMenu
                    where p.Keyword == keyword
                    select p;
            if (c != null && c.Count() > 0)
            {
                var r = from p in DBContext.PortalDocument
                        where p.MenuId == c.FirstOrDefault().Id && p.State
                        select p;
                return r.ToList();
            }
            else
            {
                return new List<PortalDocument>();
            }
        }
        /// <summary>
        /// Get PortalDocument by documentId,if not exist return a empty PortalDocument object
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public PortalDocument GetPortalDocument(int documentId)
        {
            var c = from p in DBContext.PortalDocument
                    where p.Id == documentId
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.FirstOrDefault();
            }
            else
            {
                return new PortalDocument();
            }
        }
        /// <summary>
        /// Add a new PortalDocument to database
        /// </summary>
        /// <param name="portalDocument"></param>
        /// <returns></returns>
        public bool AddPortalDocument(PortalDocument portalDocument)
        {
            if (ValidatePortalDocument(portalDocument))
            {
                var c = DBContext.PortalDocument.OrderByDescending(p => p.Id).FirstOrDefault();
                if (c == null)
                {
                    portalDocument.Id = 1;
                }
                else
                {
                    portalDocument.Id = c.Id + 1;
                }
                portalDocument.State = true;
                portalDocument.Sequence = 100;
                portalDocument.CreateDate = DateTime.Now;
                //DBContext.AddToPortalDocument(portalDocument);
                //DBContext.SaveChanges();
                string sqlText = string.Empty;
                sqlText = string.Format(@"insert into PortalDocument ( Id , Name, MenuId , DisplayName ,Url , Description,  ImageUrl , Sequence , State ,
                                                Extend1,Extend2,Extend3,Extend4,Extend5,Extend6,Extend7) 
                                                values ( {0} , '{1}' , {2} , '{3}' , '{4}' ,'{5}' , '{6}' , {7} , '{8}' , '{9}' , '{10}','{11}' , '{12}', '{13}', '{14}', '{15}')",
                         portalDocument.Id, portalDocument.Name,portalDocument.MenuId, portalDocument.DisplayName, portalDocument.Url, portalDocument.Description, portalDocument.ImageUrl, portalDocument.Sequence, 1,
                         portalDocument.Extend1, portalDocument.Extend2, portalDocument.Extend3, portalDocument.Extend4, portalDocument.Extend5, portalDocument.Extend6, portalDocument.Extend7);
                ExecuteSQLiteSql(sqlText);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Update a PortalDocument
        /// </summary>
        /// <param name="portalDocument"></param>
        /// <returns></returns>
        public bool UpdatePortalDocument(PortalDocument portalDocument)
        {
            portalDocument.CreateDate = Convert.ToDateTime(portalDocument.CreateDate);
            if (ValidatePortalDocument(portalDocument))
            {
                //PortalDocument originalPortalDocument = GetPortalDocument(portalDocument.Id);
                //DBContext.ApplyPropertyChanges(originalPortalDocument.EntityKey.EntitySetName, portalDocument);
                //DBContext.SaveChanges();
                string sqlText = string.Empty;
                sqlText = string.Format(@"Update PortalDocument set Name='{1}', MenuId={2} , DisplayName='{3}' ,Url='{4}' , Description='{5}',  ImageUrl='{6}' , Sequence={7} , State='{8}' ,
                                                Extend1='{9}',Extend2='{10}',Extend3='{11}',Extend4='{12}',Extend5='{13}',Extend6='{14}',Extend7='{15}' where Id={0}",
                         portalDocument.Id, portalDocument.Name, portalDocument.MenuId, portalDocument.DisplayName, portalDocument.Url, portalDocument.Description, portalDocument.ImageUrl, portalDocument.Sequence, Convert.ToInt32(portalDocument.State),
                         portalDocument.Extend1, portalDocument.Extend2, portalDocument.Extend3, portalDocument.Extend4, portalDocument.Extend5, portalDocument.Extend6, portalDocument.Extend7);
                ExecuteSQLiteSql(sqlText);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Delete a PortalDocument
        /// </summary>
        /// <param name="documentId"></param>
        public void DeletePortalDocument(int documentId)
        {
            PortalDocument originalPortalDocument = GetPortalDocument(documentId);
            //DBContext.DeleteObject(originalPortalDocument);
            //DBContext.SaveChanges();
            string sqlText = string.Empty;
            sqlText = string.Format(@"Delete from PortalDocument where Id={0}", documentId);
            ExecuteSQLiteSql(sqlText);
        }
        /// <summary>
        /// Validate the PortalDocument
        /// </summary>
        /// <param name="portalDocument"></param>
        /// <returns></returns>
        public bool ValidatePortalDocument(PortalDocument portalDocument)
        {
            CleanErrorDic();
            if (string.IsNullOrEmpty(portalDocument.Name))
            {
                ModelStateDic.AddModelError("Name", "Name should not be empty.");
            }
            return ModelStateDic.IsValid;
        }
    }
}
