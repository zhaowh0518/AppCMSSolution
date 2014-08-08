<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    用户登录
</asp:Content>
<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        <br />用户登录</h3>
    <p>
        请输入用户名和密码. 如果您还没有帐号，请<%= Html.ActionLink("注册", "Register") %>.
    </p>
    <%= Html.ValidationSummary("Login was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       { %>
    <div>
        <fieldset>
            <legend>登录信息</legend>
            <p>
                <label for="username" class="lbTitle">
                    用户名:</label>
                <%= Html.TextBox("username") %>
                <%= Html.ValidationMessage("username") %>
            </p>
            <p>
                <label for="password" class="lbTitle">
                    密&nbsp;&nbsp;&nbsp;&nbsp;码:</label>
                <%= Html.Password("password") %>
                <%= Html.ValidationMessage("password") %>
            </p>
            <p>
                <%= Html.CheckBox("rememberMe") %>
                <label class="inline" for="rememberMe">
                    Remember me?</label>
            </p>
            <p>
                <input type="submit" value="登录" />
            </p>
            <p>
                *注册为本站会员可享受折扣优惠</p>
        </fieldset>
    </div>
    <% } %>
</asp:Content>
