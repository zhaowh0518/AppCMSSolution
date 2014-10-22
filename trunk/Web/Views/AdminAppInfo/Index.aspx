<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<Disappearwind.PortalSolution.PortalWeb.Models.AppInfo>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    App管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        App管理</h2>
    <table>
        <tr>
            <th>
            </th>
            <th>
                ID
            </th>
            <th>
                名称
            </th>
            <th>
                关键字
            </th>
            <th>
                版本号
            </th>
            <th>
                是否强制升级
            </th>
            <th>
                描述
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%: Html.ActionLink("编辑", "Edit", new { id = item.ID })%>
                |
                <%: Html.ActionLink("删除", "Delete", new { id = item.ID })%>
            </td>
            <td>
                <%: item.ID %>
            </td>
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: item.Keyword %>
            </td>
            <td>
                <%: item.Version %>
            </td>
            <td>
                <%: item.VersionUpgrade %>
            </td>
            <td>
                <%: item.Description %>
            </td>
        </tr>
        <% } %>
    </table>
    <p>
        <%: Html.ActionLink("新增", "Create") %>
    </p>
</asp:Content>
