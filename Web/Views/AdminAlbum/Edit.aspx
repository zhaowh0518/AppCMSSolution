<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.Album>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit</h2>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>专辑信息</legend>
        <div class="editor-label">
            专题标题：
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Name) %>
            <%: Html.ValidationMessageFor(model => model.Name) %>
        </div>
        <div class="editor-label">
            图片地址：
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.ImageUrl) %>
            <%: Html.ValidationMessageFor(model => model.ImageUrl) %>
        </div>
        <div class="editor-label">
            目录地址：
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Url) %>
            <%: Html.ValidationMessageFor(model => model.Url) %>
        </div>
        <div class="editor-label">
            专辑描述：
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Description) %>
            <%: Html.ValidationMessageFor(model => model.Description) %>
        </div>
        <div class="editor-label">
            专辑状态：
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
        <%: Html.ActionLink("回到列表", "Index") %>
    </div>
</asp:Content>
