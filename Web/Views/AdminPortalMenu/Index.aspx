<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<Disappearwind.PortalSolution.PortalWeb.Models.PortalMenu>>" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <th>
                Command
            </th>
            <th>
                Id
            </th>
            <th>
                Name
            </th>
            <th>
                DisplayName
            </th>
            <th>
                Keyword
            </th>
            <th>
                Type
            </th>
            <th>
                State
            </th>
        </tr>
        <%  
            int total = Model.Count();
            int currentPage = (int)ViewData[PortalSessionKey.MenuListPageNum.ToString()];
            if (currentPage > CommonUtility.PageSize || currentPage < 0)
            {
                currentPage = 1;
            }
            var c = Model.Skip(currentPage * CommonUtility.PageSize - CommonUtility.PageSize).Take(CommonUtility.PageSize).ToList();
            foreach (var item in c)
            { %>
        <tr>
            <td>
                <%= Html.ActionLink("Details", "Details", new { id=item.Id })%>
                |
                <%= Html.ActionLink("Edit", "Edit", new { id=item.Id }) %>
                |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.Id }) %>
            </td>
            <td>
                <%= Html.Encode(item.Id) %>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.DisplayName) %>
            </td>
            <td>
                <%= Html.Encode(item.Keyword) %>
            </td>
            <td>
                <%= Html.Encode(item.Type) %>
            </td>
            <td>
                <%= Html.Encode(item.State) %>
            </td>
        </tr>
        <% } %>
    </table>
    <div>
        <ul class="pagger">
            <% if (total > CommonUtility.PageSize)
               {
                   int pageCount = (int)System.Math.Ceiling((double)total / CommonUtility.PageSize);
                   for (int i = 1; i <= pageCount; i++)
                   {                                               
            %>
            <li><a href="/AdminPortalMenu/Index/PageNum/<%=i %>">
                <%=i %>
            </a></li>
            <%      }
               } %>
        </ul>
    </div>
    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>
</asp:Content>
