<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录</title>
</head>
<body>
    <div>
        <form action="/Web/Login" method="post">
        <p>
            <p>
                <label for="username" class="lbTitle">
                    用户名:</label>
                <%= Html.TextBox("username") %>
            </p>
            <p>
                <label for="password" class="lbTitle">
                    密&nbsp;&nbsp;&nbsp;&nbsp;码:</label>
                <%= Html.Password("password") %>
            </p>
            <p>
                <%= Html.CheckBox("rememberMe") %>
                <label class="inline" for="rememberMe">
                   记住密码？</label>
            </p>
            <p>
                <input type="submit" value="登录" />
            </p>
        </p>
        </form>
    </div>
</body>
</html>
