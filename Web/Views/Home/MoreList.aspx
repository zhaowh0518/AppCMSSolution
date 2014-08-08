<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Disappearwind.PortalSolution.PortalWeb.Models.PortalDocument>>" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    更多内容
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page">
        <div id="primary">
            <div id="listProduct" class="module">
                <div class="module_tops">
                </div>
                <div class="modulecontent">
                    <ul>
                        <%var listProducts = Model.ToList();
                          int total = listProducts.Count;
                          int pageSize = 20;
                          int currentPage = (int)ViewData[PortalSessionKey.PortalListPageNum.ToString()];
                          if (currentPage > pageSize || currentPage < 0)
                          {
                              currentPage = 1;
                          }
                          listProducts = listProducts.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();
                          foreach (var item in listProducts)
                          {
                        %>
                        <li>
                            <%=Html.ActionLink(item.DisplayName,"Details",new{id=item.Id}) %>
                        </li>
                        <% 
                            } %>
                    </ul>
                </div>
                <div class="module_btms">
                </div>
            </div>
            <div>
                <ul class="pagger">
                    <% if (total > pageSize)
                       {
                           int pageCount = (int)System.Math.Ceiling((double)total / pageSize);
                           for (int i = 1; i <= pageCount; i++)
                           {                                               
                    %>
                    <li><a href='/Home/MoreList/<%=Request.QueryString["keyword"]%>/PageNum/<%=i %>'>
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
