<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Disappearwind.PortalSolution.PortalWeb.Models.UserInfo>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	用户管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>用户管理</h2>

    <table>
        <tr>
            <th>
                用户名
            </th>
            <th>
                密码
            </th>
            <th>
                真实姓名
            </th>
            <th>
                手机
            </th>
            <th>
                Email
            </th>
            <th>
                MSN
            </th>
            <th>
                QQ
            </th>
            <th>
                注册日期
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.Encode(item.UserName) %>
            </td>
            <td>
                <%= Html.Encode(item.Pwd) %>
            </td>
            <td>
                <%= Html.Encode(item.DisplayName) %>
            </td>
            <td>
                <%= Html.Encode(item.Phone) %>
            </td>
            <td>
                <%= Html.Encode(item.Email) %>
            </td>
            <td>
                <%= Html.Encode(item.MSN) %>
            </td>
            <td>
                <%= Html.Encode(item.QQ) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.CreateDate)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

</asp:Content>

