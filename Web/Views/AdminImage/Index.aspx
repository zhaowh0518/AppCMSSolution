<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AdminSite.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function selectImage() {
            var list = document.getElementsByName("chkImage");
            var selected = "";
            for (var i = 0; i < list.length; i++) {
                if (list[i].checked) {
                    selected += list[i].title + ",";
                }
            }
            document.getElementById("selectedImage").value = selected;
        }
    </script>
    <div>
        <div>
            <form action="/AdminImage/Index/" method="post" enctype="multipart/form-data">
            请选择要上传的图片:&nbsp;&nbsp;
            <input type="file" id="fileList" name="fileList" multiple="multiple" />&nbsp;&nbsp;
            <input type="submit" name="upload" value="上传" onclick="this.form.action='/AdminImage/Index'" />&nbsp;&nbsp;
            <input type="submit" name="delete" value="删除" onclick="this.form.action='/AdminImage/Delete'" />&nbsp;&nbsp;
            <input type="hidden" value='<%=ViewData["SubDirectory"] %>' id="subPath" name="subPath" />
            <input type="hidden" value="" id="selectedImage" name="selectedImage" />
            </form>
        </div>
        <div>
            <div id="imgFolderList">
                <% foreach (var item in ViewData[PortalSessionKey.ImageFolderList.ToString()] as List<string>)
                   { %>
                <div class="imgListItem">
                    <a href="/AdminImage/Index/SubPath/<%=item %>">
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
                    <input type="checkbox" title="<%=imagesList[item] %>" name="chkImage" onclick="selectImage()" />
                    <img width="50" height="50" alt="<%=item%>" src="<%=imagesList[item] %>" title="<%=imagesList[item] %>" />
                </div>
                <% } %>
            </div>
        </div>
    </div>
</asp:Content>
