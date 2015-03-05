using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Disappearwind.PortalSolution.PortalWeb.Models
{
    /// <summary>
    /// 专辑的非持久化属性
    /// </summary>
    public partial class Album
    {
        /// <summary>
        /// 专辑中的图片
        /// </summary>
        public List<string> PicList { get; set; }
    }
}