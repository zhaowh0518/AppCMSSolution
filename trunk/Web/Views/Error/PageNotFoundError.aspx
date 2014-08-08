<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    亲苹果--页面未找到
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page">
        <div id="primary">
            <div class="module" id="listProduct">
                <div class="module_tops">
                    &nbsp;</div>
                <div class="modulecontent">
                    <div>
                        &nbsp;&nbsp;您请求的页面(&nbsp;&nbsp;<%=Request.QueryString[0] %>&nbsp;&nbsp;)不存在,请单击&nbsp;&nbsp;<a href="/"><i>这里</i></a>&nbsp;&nbsp;回到首页
                    </div>
                </div>
                <div class="module_btms">
                    &nbsp;</div>
            </div>
        </div>
        <div>
            <%Html.RenderPartial("LeftNavUserControl"); %>
        </div>
    </div>
</asp:Content>
