<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.AppInfo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    修改App
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        修改App</h2>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>App信息</legend>
        <div class="editor-label">
            名称：
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Name) %>
            <%: Html.ValidationMessageFor(model => model.Name) %>
        </div>
        <div class="editor-label">
            关键字：
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Keyword) %>
            <%: Html.ValidationMessageFor(model => model.Keyword) %>
        </div>
        <div class="editor-label">
            版本号：
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Version) %>
            <%: Html.ValidationMessageFor(model => model.Version) %>
        </div>
        <div class="editor-label">
            强制升级：
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.VersionUpgrade) %>
            <%: Html.ValidationMessageFor(model => model.VersionUpgrade) %>
        </div>
        <div class="editor-label">
            描述：
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
            <%: Html.ValidationMessageFor(model => model.State) %>&nbsp;默认为空，填0为审核中
        </div>
        <p>
            <input type="submit" value="保存" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%: Html.ActionLink("回到列表页", "Index") %>
    </div>
</asp:Content>
