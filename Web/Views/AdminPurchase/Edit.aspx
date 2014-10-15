<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.PurchaseProduct>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    编辑订购产品
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        编辑订购产品</h2>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>产品信息</legend>
        <div class="editor-label">
            产品名称：
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.ProductName) %>
            <%: Html.ValidationMessageFor(model => model.ProductName) %>
        </div>
        <div class="editor-label">
            产品描述：
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Description) %>
            <%: Html.ValidationMessageFor(model => model.Description) %>
        </div>
        <div class="editor-label">
            状态：
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.State) %>
            <%: Html.ValidationMessageFor(model => model.State) %>
        </div>
        <p>
            <input type="submit" value="保存" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%: Html.ActionLink("回到列表", "Index")%>
    </div>
</asp:Content>
