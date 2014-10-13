using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Disappearwind.PortalSolution.PortalWeb.Models;

namespace Disappearwind.PortalSolution.PortalWeb.Business
{
    /// <summary>
    /// Business for Album
    /// </summary>
    public class AlbumBusiness : BaseBusiness
    {
        public static readonly string Resource_Dir = "Resource/DocImages";
        /// <summary>
        /// 获取一系列的专辑
        /// </summary>
        /// <returns></returns>
        public List<Album> GetAlbumList()
        {
            var c = from p in DBContext.Album
                    orderby p.Id descending
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.Take(200).ToList();
            }
            else
            {
                return new List<Album>();
            }
        }
        /// <summary>
        /// 获取一系列的专辑（分页）
        /// </summary>
        /// <returns></returns>
        public List<Album> GetAlbumList(int pageNum, int pageSize)
        {
            var c = from p in DBContext.Album
                    orderby p.Id descending
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return new List<Album>();
            }
        }
        /// <summary>
        /// 取某个用户的专辑
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<Album> GetUserAlbumList(int userID, int? state)
        {
            var c = from p in DBContext.Album
                    where p.Creator == userID && (p.State == state || state == null)
                    orderby p.Id descending
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.ToList();
            }
            else
            {
                return new List<Album>();
            }
        }
        /// <summary>
        /// 获取专辑下的图片列表
        /// </summary>
        /// <param name="albumID"></param>
        /// <returns></returns>
        public List<string> GetAlbumContentList(int albumID)
        {
            List<string> list = new List<string>();
            string albumPath = string.Format("{0}/{1}/{2}", AppDomain.CurrentDomain.BaseDirectory, Resource_Dir, albumID);
            DirectoryInfo dirInfo = new DirectoryInfo(albumPath);
            FileInfo[] fileList = dirInfo.GetFiles();
            for (int i = 0; i < fileList.Length; i++)
            {
                if (fileList[i].Extension.ToLower().Contains("jpg") || fileList[i].Extension.ToLower().Contains("png"))
                {
                    list.Add(string.Format("/{0}/{1}", Resource_Dir, fileList[i].Name));
                }
            }
            return list;
        }
        /// <summary>
        /// Get Album by id,if not exist return a empty Album object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Album GetAlbum(int id)
        {
            var c = from p in DBContext.Album
                    where p.Id == id
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.FirstOrDefault();
            }
            else
            {
                return new Album();
            }
        }
        /// <summary>
        /// Add a new Album to database
        /// </summary>
        /// <param name="Album"></param>
        /// <returns></returns>
        public int AddAlbum(Album album)
        {
            var c = DBContext.Album.OrderByDescending(p => p.Id).FirstOrDefault();
            if (c == null)
            {
                album.Id = 1;
            }
            else
            {
                album.Id = c.Id + 1;
            }
            //album.State = true;
            if (album.Creator == null)
            {
                album.Creator = 1;
            }
            string sqlText = string.Empty;
            sqlText = string.Format(@"insert into Album ( Id , Name , ImageUrl ,Url, Description,Creator) 
                                                  values ( {0} , '{1}' , '{2}' , '{3}' , '{4}' ,'{5}')",
                album.Id, album.Name, album.ImageUrl, album.Url, album.Description, album.Creator);
            ExecuteSQLiteSql(sqlText);
            string albumPath = string.Format("{0}/{1}/{2}", AppDomain.CurrentDomain.BaseDirectory, Resource_Dir, album.Id);
            if (!Directory.Exists(albumPath))
            {
                Directory.CreateDirectory(albumPath);
            }
            return album.Id;
        }
        /// <summary>
        /// Update a Album
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        public bool UpdateAlbum(Album album)
        {
            string sqlText = string.Empty;
            sqlText = string.Format(@"Update Album set Name='{1}' , Description='{2}',  ImageUrl='{3}',Url='{4}'  where Id={0}",
                album.Id, album.Name, album.Description, album.ImageUrl, album.Url);
            ExecuteSQLiteSql(sqlText);
            return true;
        }
        /// <summary>
        /// Delete a Album
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAlbum(int id)
        {
            string sqlText = string.Format(@"delete from Album where ID={0}", id);
            ExecuteSQLiteSql(sqlText);
        }
    }
}
