using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Disappearwind.PortalSolution.PortalWeb.Models
{
    /// <summary>
    /// 一些用到的全局数字，统一管理
    /// </summary>
    public static class MagicNum
    {
        /// <summary>
        /// 金币个数
        /// </summary>
        public enum GOLDNUM
        {
            REGISTER = 8,   //注册送的金币数 2014-12-17
            LOGIN = 2       //每天登录的金币数
        }
    }
}