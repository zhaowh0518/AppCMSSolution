<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<Disappearwind.PortalSolution.PortalWeb.Models.ClientUser>>" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    用户管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        用户管理</h2>
    <table>
        <tr>
            <th>
                ID
            </th>
            <th>
                用户名
            </th>
            <th>
                密码
            </th>
            <th>
                昵称
            </th>
            <th>
                手机号
            </th>
            <th>
                设备号
            </th>
            <th>
                积分
            </th>
            <th>
                创建日期
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%: item.UserID %>
            </td>
            <td>
                <%: item.UserName %>
            </td>
            <td>
                <%: item.Pwd %>
            </td>
            <td>
                <%: item.NickName %>
            </td>
            <td>
                <%: item.Phone %>
            </td>
            <td>
                <%: item.DeviceNum %>
            </td>
            <td>
                <%: item.Score %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.CreateDate) %>
            </td>
        </tr>
        <% } %>
        <tr>
            <td colspan="8">
                <div class="divPager">
                    <%
                        int pageNum = (int)ViewData[PortalSessionKey.ClientUserPageNum.ToString()]; %>
                    <a href="/Admin/ClientUser/PageNum/<%=pageNum-1 %>">上一页</a>&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="/Admin/ClientUser/PageNum/<%=pageNum+1 %>">下一页</a>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
