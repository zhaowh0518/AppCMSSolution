<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.PortalMenu>" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Model.DisplayName %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page">
        <div id="primary">
            <div class="module" id="promo-bar">
                <div class="module_top">
                    <h2>
                        <%=Model.DisplayName %></h2>
                    <p class="shippingnotice">
                    </p>
                    <p class="mt_btm">
                        <%=Model.Description%></p>
                </div>
                <div class="modulecontent">
                    <ul>
                        <%foreach (var item in CommonUtility.GetDocument(Model.Keyword + "_Offer").Take(5))
                          {
                        %>
                        <li>
                            <p>
                                <%=item.DisplayName %></p>
                            <a href="/Home/Details/<%=item.Id %>">
                                <img src="<%=item.ImageUrl %>" width="110" height="118" />
                            </a></li>
                        <%} %>
                    </ul>
                </div>
                <div class="module_btm" style="margin-bottom: 0px;">
                    &nbsp;</div>
            </div>
            <div class="module" id="listProduct">
                <div class="module_tops">
                    &nbsp;</div>
                <div class="modulecontent">
                    <div id="store-prod-content">
                        <table>
                            <%
                                var listProducts = CommonUtility.GetDocument(Model.Keyword + "_List");
                                int total = listProducts.Count;
                                int pageSize = 7;
                                int currentPage = (int)ViewData[PortalSessionKey.PortalListPageNum.ToString()];
                                if (currentPage > pageSize || currentPage < 0)
                                {
                                    currentPage = 1;
                                }
                                listProducts = listProducts.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();
                                foreach (var item in listProducts)
                                {
                            %>
                            <tr class="product first">
                                <td class="image">
                                    <a href="/Home/Details/<%=item.Id %>">
                                        <img src="<%=item.ImageUrl %>" width="90" height="90" border="0" />
                                    </a>
                                </td>
                                <td class="details">
                                    <h3>
                                        <%=Html.ActionLink(item.DisplayName,"Details",new{id=item.Id}) %>
                                    </h3>
                                    <p>
                                        <%=item.Description %>
                                    </p>
                                </td>
                                <td class="starrating">
                                </td>
                                <td class="purchase-info">
                                    <p class="price">
                                        <%=item.Price %>
                                    </p>
                                </td>
                            </tr>
                            <%} %>
                        </table>
                    </div>
                </div>
                <div class="module_btms">
                    &nbsp;</div>
            </div>
            <div>
                <ul class="pagger">
                    <% if (total > pageSize)
                       {
                           int pageCount = (int)System.Math.Ceiling((double)total / pageSize);
                           for (int i = 1; i <= pageCount; i++)
                           {                                               
                    %>
                    <li><a href="/Home/List/<%=Model.Id %>/pageNum/<%=i %>">
                        <%=i %>
                    </a></li>
                    <%      }
                       } %>
                </ul>
            </div>
        </div>
        <div>
            <%Html.RenderPartial("LeftNavUserControl"); %>
        </div>
    </div>
</asp:Content>
