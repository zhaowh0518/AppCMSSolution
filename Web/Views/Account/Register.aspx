<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.UserInfo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    用户注册
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        用户注册</h2>
    <%= Html.ValidationSummary("创建用户失败，详细信息如下：") %>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>用户信息</legend>
        <p>
            <label for="UserName" class="lbTitle">
                用户名:</label>
            <%= Html.TextBox("UserName") %>
            <%= Html.ValidationMessage("UserName", "*") %>
        </p>
        <p>
            <label for="Pwd" class="lbTitle">
                密码:</label>
            <%= Html.TextBox("Pwd", null, new { Type="password"})%>
            <%= Html.ValidationMessage("Pwd", "*") %>
        </p>
        <p>
            <label for="ConfirmPassword" class="lbTitle">
                确认密码:</label>
            <%= Html.TextBox("ConfirmPassword", null, new { Type = "password" })%>
            <%= Html.ValidationMessage("ConfirmPassword", "*")%>
        </p>
        <p>
            <label for="DisplayName" class="lbTitle">
                真实姓名:</label>
            <%= Html.TextBox("DisplayName") %>
            <%= Html.ValidationMessage("DisplayName", "*") %>
        </p>
        <p>
            <label for="Phone" class="lbTitle">
                手机:</label>
            <%= Html.TextBox("Phone") %>
            <%= Html.ValidationMessage("Phone", "*") %>
        </p>
        <p>
            <label for="Email" class="lbTitle">
                Email:</label>
            <%= Html.TextBox("Email") %>
            <%= Html.ValidationMessage("Email", "*") %>
        </p>
        <p>
            <label for="QQ" class="lbTitle">
                QQ:</label>
            <%= Html.TextBox("QQ") %>
            <%= Html.ValidationMessage("QQ", "*") %>
        </p>
        <p>
            <label for="MSN" class="lbTitle">
                MSN:</label>
            <%= Html.TextBox("MSN")%>
            <%= Html.ValidationMessage("MSN", "*")%>
        </p>
        <p>
            <input type="submit" value="注册" onclick="ValidateInput();" />
        </p>
    </fieldset>
    <% } %>
</asp:Content>
