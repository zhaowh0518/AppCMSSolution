<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.PortalDocument>" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary() %>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Document</legend>
        <p>
            <label for="Name">
                Name:</label>
            <%= Html.TextBox("Name") %>
            <%= Html.ValidationMessage("Name", "*") %>
        </p>
        <p>
            <label for="DisplayName">
                DisplayName:</label>
            <%= Html.TextBox("DisplayName") %>
            <%= Html.ValidationMessage("DisplayName", "*") %>
        </p>
        <p>
            <label for="MenuId">
                Menu:</label>
            <%= Html.DropDownList("MenuId", ViewData[PortalSessionKey.SelectDocumentPortalMenu.ToString()] as SelectList, TempData[PortalSessionKey.SelectedPortalMenu.ToString()])%>
            <%= Html.ValidationMessage("ParentMenu", "*") %>
        </p>
        <p>
            <label for="Url">
                Url:</label>
            <%= Html.TextBox("Url") %>
            <%= Html.ValidationMessage("Url", "*") %>
        </p>
        <p>
            <label for="Description">
                Description:</label>
            <%= Html.TextArea("Description") %>
            <%= Html.ValidationMessage("Description", "*") %>
        </p>
        <p>
            <label for="Extend1">
                普通价格:</label>
            <%= Html.TextBox("Extend1")%>
            <%= Html.ValidationMessage("Extend1", "*")%>
        </p>
        <p>
            <label for="Extend2">
                会员价格:</label>
            <%= Html.TextBox("Extend2")%>
            <%= Html.ValidationMessage("Extend2", "*")%>
        </p>
          <p>
            <label for="Extend3">
                详细介绍:</label>
            <%= Html.TextArea("Extend3")%>
            <%= Html.ValidationMessage("Extend3", "*")%>
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
