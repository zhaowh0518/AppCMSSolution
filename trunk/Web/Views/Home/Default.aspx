<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    首页
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <img src="../../Resource/CSSImages/main.JPG" alt="亲亲苹果家族 卓越产品完美呈现" />
    <%
        foreach (var item in CommonUtility.GetDocument("DA_Default").Take(4))
        {
    %>
    <a href="/Home/Details/<%=item.Id %>" target="_blank">
        <img src="<%=item.ImageUrl%>" alt="" style="width:236px;height:155px; padding-left:7px;" />
    </a>
    <% 
        } %>
</asp:Content>
