<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    用户注册
</asp:Content>
<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        用户注册</h3>
    <p>
        请至少输入
        <%=Html.Encode(ViewData["PasswordLength"])%>
        位密码.
    </p>
    <%= Html.ValidationSummary("Account creation was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       { %>
    <div>
        <fieldset>
            <legend>用户信息</legend>
            <p>
                <label for="username" class="lbTitle">
                    用户名:</label>
                <%= Html.TextBox("username") %>
                <%= Html.ValidationMessage("username") %>
            </p>
            <p>
                <label for="email" class="lbTitle">
                    Email:</label>
                <%= Html.TextBox("email") %>
                <%= Html.ValidationMessage("email") %>
            </p>
            <p>
                <label for="password" class="lbTitle">
                    密码:</label>
                <%= Html.Password("password") %>
                <%= Html.ValidationMessage("password") %>
            </p>
            <p>
                <label for="confirmPassword" class="lbTitle">
                    确认密码:</label>
                <%= Html.Password("confirmPassword") %>
                <%= Html.ValidationMessage("confirmPassword") %>
            </p>
            <p>
                <label for="displayName" class="lbTitle">
                    真实姓名:</label>
                <%= Html.TextBox("displayName")%>
                <%= Html.ValidationMessage("displayName")%>
            </p>
            <p>
                <label for="phone" class="lbTitle">
                    联系电话:</label>
                <%= Html.TextBox("phone")%>
                <%= Html.ValidationMessage("phone")%>
            </p>
            <p>
                <label for="msn" class="lbTitle">
                    MSN:</label>
                <%= Html.TextBox("msn")%>
                <%= Html.ValidationMessage("msn")%>
            </p>
            <p>
                <label for="QQ" class="lbTitle">
                    QQ:</label>
                <%= Html.TextBox("qq")%>
                <%= Html.ValidationMessage("qq")%>
            </p>
            <p>
                *
            </p>
            <p>
                <input type="submit" value="注册" />
            </p>
        </fieldset>
    </div>
    <% } %>
</asp:Content>
