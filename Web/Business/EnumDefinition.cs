using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Disappearwind.PortalSolution.PortalWeb.Business
{
    /// <summary>
    /// Menu's type only DocumentMenu can have document
    /// and only NodeMenu can have child menu
    /// </summary>
    public enum MenuType
    {
        NodeMenu = 0,
        DocumentMenu = 1
    }
    /// <summary>
    /// State of object like PortalInfo, Document and Menu
    /// </summary>
    public enum ObjectState
    {
        Offline = 0,
        Online = 1
    }
}
