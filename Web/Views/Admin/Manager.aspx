<%@ Page Title="Manager" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    The Manager
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Manage database</h2>
    <form action="/Admin/Manager" method="post">
    <label>
        SqlText:</label><br />
    <textarea id="txtSql" name="txtSql" cols="100" rows="20"></textarea><br />
    <input type="submit" value="Submit" /><br />
    <textarea cols="100" rows="5">
    <%=ViewData[PortalSessionKey.Message.ToString()] %></textarea>
    </form>
</asp:Content>
