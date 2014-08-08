<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form action="/AdminImage/Index/" method="post" enctype="multipart/form-data">
    <div>
        Please select a file to upload:&nbsp;&nbsp;
        <input type="file" id="fileImage" name="fileImage" />&nbsp;&nbsp;
        <input type="submit" value="Upload" />
    </div>
    <div>
        <div id="imgFolderList">
            <% foreach (var item in ViewData[PortalSessionKey.ImageFolderList.ToString()] as List<string>)
               { %>
            <div class="imgListItem">
                <a href="\AdminImage\Index\SubPath\<%=item %>">
                    <img width="50" height="50" alt="<%=item %>" src="\resource\cssimages\folder.jpg"
                        title="<%=item %>" />
                </a>
            </div>
            <% } %>
        </div>
        <div id="foderSeperator">
            <hr />
        </div>
        <script type="text/javascript">
            function OnClickImage(img) {
                alert("图片地址已复制到粘贴板上！");
                window.clipboardData.setData("Text", img.title);
            }
        </script>
        <div id="imgList">
            <%  var imagesList = ViewData[PortalSessionKey.ImageFileList.ToString()] as Dictionary<string, string>;
                foreach (var item in imagesList.Keys)
                { %>
            <div class="imgListItem">
                <img width="50" height="50" alt="<%=item%>" src="<%=imagesList[item] %>" title="<%=imagesList[item] %>"
                    onclick="OnClickImage(this)" />
            </div>
            <% } %>
        </div>
    </div>
    <div style="clear: left;">
        <hr />
        p.s. when you click mouse on a image, the image's url will be coped in the clipboard.</div>
    </form>
</asp:Content>
