<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Disappearwind.PortalSolution.PortalWeb.Models.Album>>" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>专辑管理</title>
    <script src="../../Scripts/Web.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="../../Content/Web.css" />
</head>
<body>
    <div>
        <h1>
            管理我的专辑</h1>
        <form action="/Web/Index/" method="post" enctype="multipart/form-data">
        <div id="divAction">
            <input type="file" name="fileAlbum" id="fileAlbum" multiple="multiple" />&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="submit" value="添加封面" name="btnAddCover" id="btnAddCover" onclick="return doAction('UploadCover')" />&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="submit" value="添加图片" name="btnAddImage" id="btnAddImage" onclick="return doAction('UploadImage')" />&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="submit" value="查看图片" name="btnViewImage" id="btnViewImage" onclick="return doAction('ViewImage')" />&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="submit" value="删除图片" name="btnDeleteImage" id="btnDeleteImage" onclick="return doAction('DeleteImage')" />&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="hidden"  name="aid" id="aid" value='<%=Session["AID"] %>' />
            <input type="hidden" value="" name="cmd" id="cmd" />
            <input type="hidden" value="" name="selectedImage" id="selectedImage" />
            <p class="message" id="message">
                <%=Session["INDEX_MESSAGE"]%></p>
        </div>
        <div id="divAlbumList">
            <div>
                <table>
                    <tr>
                        <th width="50px">
                            选择
                        </th>
                        <th width="120px">
                            状态
                        </th>
                        <th width="100px">
                            封面
                        </th>
                        <th width="110px">
                            专辑名
                        </th>
                    </tr>
                </table>
            </div>
            <div id="divAlbumContent">
                <table>
                    <%
                        string isChecked = string.Empty;
                        foreach (var item in Model)
                        {
                            if (Session["AID"] != null && Session["AID"].Equals(item.Id))
                            {
                                isChecked = "checked";
                            }
                            else
                            {
                                isChecked = string.Empty;
                            }
                    %>
                    <tr>
                        <td width="50px">
                            <input type="checkbox" class="chbList" <%=isChecked %> onclick="doClickCheck(this,'aid',<%: item.Id %>)" /><%: item.Id %>
                        </td>
                        <td width="120px">
                            <%: CommonUtility.ShowAlbumState(item.State)%>
                        </td>
                        <td width="100px">
                            <img alt="" src='<%=Request.Url.GetLeftPart(UriPartial.Authority) %><%: item.ImageUrl.Replace("//","/") %>' />
                        </td>
                        <td width="110px">
                            <%: item.Name %>
                        </td>
                    </tr>
                    <% } %>
                </table>
            </div>
        </div>
        <div id="divPicList">
            <%  var imagesList = Session["IMAGE_LIST"] as Dictionary<string, string>;
                if (imagesList != null && imagesList.Keys != null)
                {
                    foreach (var item in imagesList.Keys)
                    { %>
            <div class="imgItem">
                <input type="checkbox" title="<%=imagesList[item] %>" name="chkImage" onclick="selectImage()" />&nbsp;
                <img alt="<%=item%>" src='<%=Request.Url.GetLeftPart(UriPartial.Authority) %><%: imagesList[item].Replace("//","/") %>'
                    title="<%=imagesList[item] %>" />
            </div>
            <% }
                }%>
        </div>
        </form>
    </div>
</body>
</html>
