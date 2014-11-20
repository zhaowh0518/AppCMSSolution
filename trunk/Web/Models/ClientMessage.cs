using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Disappearwind.PortalSolution.PortalWeb.Models
{
    /// <summary>
    /// 返回给客户端的数据
    /// </summary>
    public class ServiceReturnData<T>
    {
        public ServiceReturnData()
        {
            if (DictData == null)
            {
                DictData = new Dictionary<string, string>();
            }
        }
        /// <summary>
        /// 返回数据编号：-1：出错；0：无数据；1：成功返回数据
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 返回的信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回的字符数据
        /// </summary>
        public string StrData { get; set; }
        /// <summary>
        /// 返回列表数据
        /// </summary>
        public List<T> ListData { get; set; }
        /// <summary>
        /// Key Value数据，最后以JSON格式返回
        /// </summary>
        public Dictionary<string, string> DictData { get; set; }
    }
}