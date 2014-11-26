<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<Disappearwind.PortalSolution.PortalWeb.Models.PurchaseProduct>>" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    订购管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        订购管理</h2>
    <table>
        <tr>
            <th>
                操作
            </th>
            <th>
                状态
            </th>
            <th>
                ID
            </th>
            <th>
                产品ID
            </th>
            <th>
                产品名称
            </th>
            <th>
                金币数量
            </th>
            <th>
                产品描述
            </th>
            <th>
                订单数量
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%: Html.ActionLink("编辑", "Edit", new { id = item.ID })%>
                |
                <%: Html.ActionLink("删除", "Delete", new { id=item.ID })%>
            </td>
            <td>
                <%:item.State %>
            </td>
            <td>
                <%: item.ID %>
            </td>
            <td>
                <%: item.ProductID %>
            </td>
            <td>
                <%: item.ProductName %>
            </td>
            <td>
                <%: item.Gold %>
            </td>
            <td>
                <%: item.Description %>
            </td>
            <td>
                <%: item.OrderCount %>
            </td>
        </tr>
        <% } %>
        <tr>
            <td colspan="8">
                <div class="divPager">
                    <%
                        int pageNum = (int)ViewData[PortalSessionKey.PurchasePageNum.ToString()]; %>
                    <a href="/AdminPurchase/Index/PageNum/<%=pageNum-1 %>">上一页</a>&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="/AdminPurchase/Index/PageNum/<%=pageNum+1 %>">下一页</a>
                </div>
            </td>
        </tr>
    </table>
    <p>
        <%: Html.ActionLink("新增", "Create")%>
    </p>
</asp:Content>
