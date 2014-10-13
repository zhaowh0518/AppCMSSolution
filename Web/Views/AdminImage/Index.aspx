<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form action="/AdminImage/Index/" method="post" enctype="multipart/form-data">
    <div>
        <div>
            请选择要上传的图片:&nbsp;&nbsp;
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
            <div id="imgList">
                <%  var imagesList = ViewData[PortalSessionKey.ImageFileList.ToString()] as Dictionary<string, string>;
                    foreach (var item in imagesList.Keys)
                    { %>
                <div class="imgListItem">
                    <img width="50" height="50" alt="<%=item%>" src="<%=imagesList[item] %>" title="<%=imagesList[item] %>" />
                </div>
                <% } %>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
