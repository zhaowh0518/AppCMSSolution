<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<Disappearwind.PortalSolution.PortalWeb.Models.PortalDocument>>" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <% using (Html.BeginForm())
           {%>
        请选择要查看的菜单:&nbsp;&nbsp;
        <%= Html.DropDownList("Id", ViewData[PortalSessionKey.SelectDocumentPortalMenu.ToString()] as SelectList,
            new { onchange = "this.form.submit();"})%>
        &nbsp;&nbsp;&nbsp;&nbsp;已选择的菜单：<%=CommonUtility.GetMenuName(Session[PortalSessionKey.SelectedPortalMenu.ToString()])%>
        <%} %>
    </p>
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
                Sequence
            </th>
            <th>
                State
            </th>
        </tr>
        <% int total = Model.Count();
           int currentPage = (int)ViewData[PortalSessionKey.DocumentListPageNum.ToString()];
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
                <%= Html.Encode(item.Sequence) %>
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
            <li><a href="/AdminPortalDocument/Index/PageNum/<%=i %>">
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
