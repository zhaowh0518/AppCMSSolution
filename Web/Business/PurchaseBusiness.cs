using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Business
{
    /// <summary>
    /// Business for PurchaseProduct and PurchaseOrder
    /// </summary>
    public class PurchaseBusiness : BaseBusiness
    {
        /// <summary>
        /// 获取所有上线的产品，用于返回给客户端
        /// </summary>
        /// <returns></returns>
        public List<PurchaseProduct> GetProductList()
        {
            var c = from p in DBContext.PurchaseProduct
                    where p.State == 1
                    orderby p.ID descending
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.ToList();
            }
            else
            {
                return new List<PurchaseProduct>();
            }
        }
        /// <summary>
        /// 获取所有的产品（分页）,用于后台管理
        /// </summary>
        /// <returns></returns>
        public List<PurchaseProduct> GetProductList(int pageNum, int pageSize)
        {
            var c = from p in DBContext.PurchaseProduct
                    orderby p.ID descending
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return new List<PurchaseProduct>();
            }
        }
        /// <summary>
        /// Get PurchaseProduct by id,if not exist return a empty PurchaseProduct object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PurchaseProduct GetProduct(int id)
        {
            var c = from p in DBContext.PurchaseProduct
                    where p.ID == id
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.FirstOrDefault();
            }
            else
            {
                return new PurchaseProduct();
            }
        }
        /// <summary>
        /// Get PurchaseProduct by proiduct id,if not exist return a empty PurchaseProduct object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PurchaseProduct GetProduct(string productid)
        {
            var c = from p in DBContext.PurchaseProduct
                    where p.ProductID == productid
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.FirstOrDefault();
            }
            else
            {
                return new PurchaseProduct();
            }
        }
        /// <summary>
        /// Add a new PurchaseProduct to database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int AddProduct(PurchaseProduct product)
        {
            var c = DBContext.PurchaseProduct.OrderByDescending(p => p.ID).FirstOrDefault();
            if (c == null)
            {
                product.ID = 1;
            }
            else
            {
                product.ID = c.ID + 1;
            }
            product.AppID = 1;
            product.State = 0; //默认状态是0
            product.OrderCount = 0; //新增加的产品订单数为0
            if (product.Gold == null) //金币数默认为0
            {
                product.Gold = 0;
            }
            string sqlText = string.Empty;
            sqlText = string.Format(@"insert into PurchaseProduct ( ID , ProductID,ProductName,State,Description,AppID,OrderCount,Gold) 
                                                  values ( {0} , '{1}' , '{2}' , '{3}' , '{4}' ,'{5}','{6}','{7}')",
                product.ID, product.ProductID, product.ProductName, product.State, product.Description, product.AppID, product.OrderCount, product.Gold);
            ExecuteSQLiteSql(sqlText);
            return product.ID;
        }
        /// <summary>
        /// Update a Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool UpdateProduct(PurchaseProduct product)
        {
            string sqlText = string.Empty;
            sqlText = string.Format(@"Update PurchaseProduct set ProductName='{1}' , Description='{2}',  State='{3}',Gold='{4}'  where Id={0}",
                product.ID, product.ProductName, product.Description, product.State, product.Gold);
            ExecuteSQLiteSql(sqlText);
            return true;
        }
        /// <summary>
        /// Update product state
        /// </summary>
        /// <param name="idList">ID List</param>
        /// <param name="state">State</param>
        /// <returns></returns>
        public bool UpdateProductState(string idList, bool state)
        {
            string sqlText = string.Empty;
            sqlText = string.Format(@"Update PurchaseProduct set State='{1}'  where Id in '{0}'",
                state, idList);
            ExecuteSQLiteSql(sqlText);
            return true;
        }
        /// <summary>
        /// Update product order count
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public bool UpdateProductOrder(string productID)
        {
            var c = DBContext.PurchaseProduct.Where(p => p.ProductID == productID).OrderByDescending(p => p.ID).FirstOrDefault();
            if (c != null)
            {
                int orderCount = c.OrderCount + 1; //用户每产生一次订单，订单数增加1
                string sqlText = string.Empty;
                sqlText = string.Format(@"Update PurchaseProduct set OrderCount='{1}' where ProductID = '{0}'",
                    productID, orderCount);
                ExecuteSQLiteSql(sqlText);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Delete a Product
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            string sqlText = string.Format(@"delete from PurchaseProduct where ID={0}", id);
            ExecuteSQLiteSql(sqlText);
        }
        /// <summary>
        /// Add a purchase order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool AddOrder(PurchaseOrder order)
        {
            string sqlText = string.Empty;
            order.CreateDate = DateTime.Now;
            sqlText = string.Format(@"insert into PurchaseOrder (ProductID,UserID,CreateDate,TransactionID) 
                                                  values ( {0} , '{1}' , '{2}' , '{3}')",
                order.ProductID, order.UserID, order.CreateDate, order.TransactionID);
            ExecuteSQLiteSql(sqlText);
            UpdateProductOrder(order.ProductID);
            new ClientUserBusiness().UpdateScoreForProduct(order.UserID, order.ProductID);
            return true;
        }
    }
}
