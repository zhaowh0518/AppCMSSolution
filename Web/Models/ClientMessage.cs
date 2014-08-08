using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Disappearwind.PortalSolution.PortalWeb.Models
{
    /// <summary>
    /// 返回给服务端的消息
    /// </summary>
    public class ClientMessage
    {
        public string Message { get; set; }
        public string Data { get; set; }

        public ClientMessage(string message, string data)
        {
            Message = message;
            Data = data;
        }
    }
}