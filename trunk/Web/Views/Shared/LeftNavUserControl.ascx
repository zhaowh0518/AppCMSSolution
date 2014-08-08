<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<div id="index_Left">
    <div id="index_LeftAD">
        <%=Html.DocumentImage("DA_Index_LeftAD",168,107) %></div>
    <div id="index_LeftNav" class="module">
        <div class="module_top">
        </div>
        <div class="modulecontent">
            <ul>
                <%foreach (var item in CommonUtility.GetDocument("DA_Index_LeftNav"))
                  {
                %>
                <li>
                    <%=Html.ActionLink(item.DisplayName, "List", "Home", new { id=item.Url},null)%>
                </li>
                <% 
                    } %>
            </ul>
        </div>
        <div class="module_btm">
        </div>
    </div>
    <div id="index_LeftList" class="module">
        <h2>
            <span>
                <%=CommonUtility.GetMenuName("DA_Index_LeftList")%>
            </span>
        </h2>
        <div class="modulecontent">
            <ul>
                 <!--Keywork changed temp-->
                <%foreach (var item in CommonUtility.GetDocument("DA_List_IPodPart_List").Take(15))
                  {
                %>
                <li>
                    <%=Html.ActionLink(item.DisplayName,"Details",new{id=item.Id}) %>
                </li>
                <% 
                    } %>
            </ul>
            <div class="more">
                <%=Html.ActionLink("查看更多>>", "MoreList", "Home", new { keyword = "DA_List_IPodPart_List" }, null)%>
            </div>
        </div>
        <div class="module_btm">
        </div>
    </div>
</div>
