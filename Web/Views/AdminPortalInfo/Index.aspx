<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<Disappearwind.PortalSolution.PortalWeb.Models.PortalInfo>>" %>
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
        </tr>
        <% foreach (var item in Model)
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
        </tr>
        <% } %>
    </table>
    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>
</asp:Content>
