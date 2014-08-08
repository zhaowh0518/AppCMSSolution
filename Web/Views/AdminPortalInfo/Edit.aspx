<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.PortalInfo>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>PortalInfo</legend>
        <p>
            <label for="Name">
                Name:</label>
            <%= Html.TextBox("Name", Model.Name) %>
            <%= Html.ValidationMessage("Name", "*") %>
        </p>
        <p>
            <label for="DisplayName">
                DisplayName:</label>
            <%= Html.TextBox("DisplayName", Model.DisplayName) %>
            <%= Html.ValidationMessage("DisplayName", "*") %>
        </p>
        <p>
            <label for="Keyword">
                Keyword:</label>
            <%= Html.TextBox("Keyword", Model.Keyword) %>
            <%= Html.ValidationMessage("Keyword", "*") %>
        </p>
        <p>
            <label for="Url">
                Url:</label>
            <%= Html.TextBox("Url", Model.Url) %>
            <%= Html.ValidationMessage("Url", "*") %>
        </p>
        <p>
            <label for="ImageUrl">
                ImageUrl:</label>
            <%= Html.TextBox("ImageUrl", Model.ImageUrl) %>
            <%= Html.ValidationMessage("ImageUrl", "*") %>
        </p>
        <p>
            <label for="Description">
                Description:</label>
            <%= Html.TextBox("Description", Model.Description) %>
            <%= Html.ValidationMessage("Description", "*") %>
        </p>
        <p class="submit">
            <input type="submit" value="Save" />
        </p>
    </fieldset>
    <% } %>
    <div class="command">
        <%=Html.ActionLink("回到列表", "Index") %>
    </div>
</asp:Content>
