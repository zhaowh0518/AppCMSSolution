<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.Album>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新增专辑
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        新增专辑</h2>
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
            是否收费：
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Gold) %>
            <%: Html.ValidationMessageFor(model => model.Gold) %>
        </div>
        <div class="editor-label">
            审核专用：
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Type) %>
            <%: Html.ValidationMessageFor(model => model.Type) %>
        </div>
        <p>
            <input type="submit" value="提交" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%: Html.ActionLink("回到列表", "Index") %>
    </div>
</asp:Content>
