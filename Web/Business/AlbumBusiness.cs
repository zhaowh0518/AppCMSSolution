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
                List<Album> albumList = c.Take(200).ToList();
                foreach (Album album in albumList)
                {
                    album.PicList = GetAlbumContentList(album.Id);
                }
                return albumList;
            }
            else
            {
                return new List<Album>();
            }
        }
        /// <summary>
        /// 获取一系列的专辑（分页）,管理后台用
        /// </summary>
        /// <returns></returns>
        public List<Album> GetAlbumList(int pageNum, int pageSize)
        {
            var c = from p in DBContext.Album
                    orderby p.Id descending
                    select p;
            if (c != null && c.Count() > 0)
            {
                List<Album> albumList = c.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
                return albumList;
            }
            else
            {
                return new List<Album>();
            }
        }
        /// <summary>
        /// 获取一系列的专辑（分页），接口用
        /// </summary>
        /// <returns></returns>
        public List<Album> GetAlbumList(int pageNum, int pageSize, int userid)
        {
            var c = from p in DBContext.Album
                    where p.State == 1 //只取审核通过的专辑
                    orderby p.Id descending
                    select p;
            if (c != null && c.Count() > 0)
            {
                List<Album> albumList = c.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
                List<AlbumView> albumViewList = new List<AlbumView>();
                var v = DBContext.AlbumView.Where(p => p.UserID == userid);
                if (v != null && v.Count() > 0)
                {
                    albumViewList = v.ToList();
                }
                foreach (Album album in albumList)
                {
                    album.PicList = GetAlbumContentList(album.Id);
                    if (albumViewList.Where(p => p.AlbumID == album.Id).Count() > 0) //用户看过专辑后Gold就为0
                    {
                        album.Gold = 0;
                    }
                    else
                    {
                        album.Gold = GetAlbumGold(album.PicList.Count);
                    }
                }
                return albumList;
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
        /// <param name="state">-1：审核未通过；0：未审核；1：审核通过；2：审核未通过+未审核</param>
        /// <returns></returns>
        public List<Album> GetUserAlbumList(int userID, int? state)
        {
            var c = from p in DBContext.Album
                    where p.Creator == userID && (p.State == state || state == null) || (state == 2 && p.State < 1)
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
            if (dirInfo != null && dirInfo.Exists)
            {
                FileInfo[] fileList = dirInfo.GetFiles();
                if (fileList != null & fileList.Length > 0)
                {
                    for (int i = 0; i < fileList.Length; i++)
                    {
                        if (fileList[i].Extension.ToLower().Contains("jpg") || fileList[i].Extension.ToLower().Contains("png"))
                        {
                            list.Add(string.Format("/{0}/{1}/{2}", Resource_Dir, albumID, fileList[i].Name));
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 根据图片的个数获取金币数
        /// 3张以下1个金币6张以下2个金币10张以下3个金币
        /// </summary>
        /// <param name="picCount"></param>
        /// <returns></returns>
        public int GetAlbumGold(int picCount)
        {
            int gold = 1;
            if (picCount > 3 && picCount <= 6)
            {
                gold = 2;
            }
            else if (picCount > 6)
            {
                gold = 3;
            }
            return gold;
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
            album.State = 0;//默认未审核
            if (album.Creator == null)
            {
                album.Creator = 1;
            }
            string sqlText = string.Empty;
            sqlText = string.Format(@"insert into Album ( Id , Name , ImageUrl ,Url, Description,State,Creator) 
                                                  values ( {0} , '{1}' , '{2}' , '{3}' , '{4}' ,'{5}','{6}')",
                album.Id, album.Name, album.ImageUrl, album.Url, album.Description, album.State, album.Creator);
            ExecuteSQLiteSql(sqlText);
            string albumPath = string.Format("{0}/{1}/{2}", AppDomain.CurrentDomain.BaseDirectory, Resource_Dir, album.Id);
            if (!Directory.Exists(albumPath))
            {
                Directory.CreateDirectory(albumPath);
            }
            return album.Id;
        }
        public int AddAlbumView(AlbumView view)
        {
            var c = DBContext.AlbumView.OrderByDescending(p => p.ID).FirstOrDefault();
            if (c == null)
            {
                view.ID = 1;
            }
            else
            {
                view.ID = view.ID + 1;
            }
            string sqlText = string.Empty;
            sqlText = string.Format(@"insert into AlbumView ( ID , AlbumID , UserID ,Gold, CreateDate) 
                                                  values ( {0} , '{1}' , '{2}' , '{3}' , '{4}')",
               view.ID, view.AlbumID, view.UserID, view.Gold, DateTime.Now.ToShortDateString());
            ExecuteSQLiteSql(sqlText);
            return view.ID;
        }
        /// <summary>
        /// Update a Album
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        public bool UpdateAlbum(Album album)
        {
            string sqlText = string.Empty;
            sqlText = string.Format(@"Update Album set Name='{1}' , Description='{2}',  ImageUrl='{3}',Url='{4}',State={5} where Id={0}",
                album.Id, album.Name, album.Description, album.ImageUrl, album.Url, album.State);
            ExecuteSQLiteSql(sqlText);
            DBContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, DBContext.Album);
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
