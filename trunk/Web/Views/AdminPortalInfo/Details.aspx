<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master" Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.PortalInfo>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>PortalInfo</legend>
        <p>
            Id:
            <%= Html.Encode(Model.Id) %>
        </p>
        <p>
            Name:
            <%= Html.Encode(Model.Name) %>
        </p>
        <p>
            DisplayName:
            <%= Html.Encode(Model.DisplayName) %>
        </p>
        <p>
            Keyword:
            <%= Html.Encode(Model.Keyword) %>
        </p>
        <p>
            Url:
            <%= Html.Encode(Model.Url) %>
        </p>
        <p>
            ImageUrl:
            <%= Html.Encode(Model.ImageUrl) %>
        </p>
        <p>
            Description:
            <%= Html.Encode(Model.Description) %>
        </p>
    </fieldset>
    <p class="command">

        <%=Html.ActionLink("Edit", "Edit", new { id=Model.Id }) %> |
        <%=Html.ActionLink("回到列表", "Index") %>
    </p>

</asp:Content>

