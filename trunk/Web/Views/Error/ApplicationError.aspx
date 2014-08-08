<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    亲苹果--程序异常
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function ShowError() {
            var divMsg = document.getElementById("msg");
            divMsg.style.display = "inline";
        }
    </script>

    <div id="page">
        <div id="primary">
            <div class="module" id="listProduct">
                <div class="module_tops">
                    &nbsp;</div>
                <div class="modulecontent">
                    <div>
                        &nbsp;&nbsp;您的请求发生了异常,单击&nbsp;&nbsp;<a onclick="ShowError();"><b><i>这里</i></b></a>&nbsp;&nbsp;查看详细信息
                    </div>
                    <div id="msg" style="display:none;padding-left:10px;">
                        <%=Request.QueryString[0] %>
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
