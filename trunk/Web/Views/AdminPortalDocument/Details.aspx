<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master" Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.PortalDocument>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Document</legend>
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
            Url:
            <%= Html.Encode(Model.Url) %>
        </p>
        <p>
            Description:
            <%= Html.Encode(Model.Description) %>
        </p>
        <p>
            ImageUrl:
            <%= Html.Encode(Model.ImageUrl) %>
        </p>
        <p>
            Sequence:
            <%= Html.Encode(Model.Sequence) %>
        </p>
        <p>
            State:
            <%= Html.Encode(Model.State) %>
        </p>
        <p>
            CreateDate:
            <%= Html.Encode(String.Format("{0:g}", Model.CreateDate)) %>
        </p>
    </fieldset>
    <p class="command">

        <%=Html.ActionLink("Edit", "Edit", new { id=Model.Id }) %> |
        <%=Html.ActionLink("回到列表", "Index") %>
    </p>

</asp:Content>

