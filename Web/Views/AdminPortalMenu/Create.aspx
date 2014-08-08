<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.PortalMenu>" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary() %>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Menu</legend>
        <p>
            <label for="Name">
                Name:</label>
            <%= Html.TextBox("Name") %>
            <%= Html.ValidationMessage("Name", "*") %>
        </p>
        <p class="hidden">
            <label for="DisplayName">
                DisplayName:</label>
            <%= Html.TextBox("DisplayName") %>
            <%= Html.ValidationMessage("DisplayName", "*") %>
        </p>
        <p>
            <label for="Keyword">
                Keyword:</label>
            <%= Html.TextBox("Keyword") %>
            <%= Html.ValidationMessage("Keyword", "*") %>
        </p>
        <p>
            <label for="Description">
                Description:</label>
            <%= Html.TextArea("Description") %>
            <%= Html.ValidationMessage("Description", "*") %>
        </p>
        <p>
            <label for="Type">
                Type:</label>
            <%= Html.DropDownList("Type",PortalHtmlHelper.PortalMenuTypeList())%>
        </p>
        <p>
            <label for="ParentMenu">
                ParentMenu:</label>
            <%= Html.DropDownList("ParentMenu",ViewData[PortalSessionKey.SelectNodePortalMenu.ToString()] as SelectList) %>
            <%= Html.ValidationMessage("ParentMenu", "*") %>
        </p>
        <p>
            <label for="Url">
                Url:</label>
            <%= Html.TextBox("Url") %>
            <%= Html.ValidationMessage("Url", "*") %>
        </p>
        <p>
            <label for="ImageUrl">
                ImageUrl:</label>
            <%= Html.TextBox("ImageUrl") %>
            <%= Html.ValidationMessage("ImageUrl", "*") %>
        </p>
        <p>
            <label for="Sequence">
                Sequence:</label>
            <%= Html.TextBox("Sequence","100") %>
            <%= Html.ValidationMessage("Sequence", "*") %>
        </p>
        <p>
            <label for="State">
                State:</label>
            <%= Html.CheckBox("State",true) %>
        </p>
        <p class="submit">
            <input type="submit" value="Create" />
        </p>
    </fieldset>
    <% } %>
    <div class="command">
        <%=Html.ActionLink("回到列表", "Index") %>
    </div>
</asp:Content>
