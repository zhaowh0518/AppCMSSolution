<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.PortalDocument>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Model.DisplayName %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page">
        <div class="pageContent">
            <fieldset>
                <legend>
                    <%=Model.DisplayName %></legend>
                <%=Model.Description %>
            </fieldset>
        </div>
        <div>
            <%Html.RenderPartial("LeftNavUserControl"); %>
        </div>
    </div>
</asp:Content>
