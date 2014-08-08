<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.PortalMenu>" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("") %>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Menu</legend>
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
            <label for="Description">
                Description:</label>
            <%= Html.TextArea("Description", Model.Description) %>
            <%= Html.ValidationMessage("Description", "*") %>
        </p>
        <p>
            <label for="Type">
                Type:</label>
            <%= Html.DropDownList("Type",PortalHtmlHelper.PortalMenuTypeList(),Model.Type)%>
        </p>
        <p>
            <label for="ParentMenu">
                ParentMenu:</label>
            <%= Html.DropDownList("ParentMenu", ViewData[PortalSessionKey.SelectNodePortalMenu.ToString()] as SelectList, Model.ParentMenu)%>
            <%= Html.ValidationMessage("ParentMenu", "*") %>
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
            <label for="Sequence">
                Sequence:</label>
            <%= Html.TextBox("Sequence", Model.Sequence) %>
            <%= Html.ValidationMessage("Sequence", "*") %>
        </p>
        <p>
            <label for="State">
                State:</label>
            <%= Html.CheckBox("State", Model.State)%>
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
