<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Disappearwind.PortalSolution.PortalWeb.Models.PortalDocument>" %>

<%@ Import Namespace="Disappearwind.PortalSolution.PortalWeb.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Model.DisplayName %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="primary">
        <div class="product-details">
            <div class="module details-main">
                <div class="module_top">
                </div>
                <div class="modulecontent">
                    <div class="product-info">
                        <h2>
                            <%=Model.DisplayName %></h2>
                        <div class="description">
                            <p>
                                <%=Model.Description %>
                            </p>
                            <a class="learn-more" href="#overview">了解更多</a>
                        </div>
                        <div class="image">
                            <img alt='<%=Model.DisplayName %>' width="162" height="150" src="<%=Model.ImageUrl %>" />
                        </div>
                    </div>
                    <form id="product-details-form">
                    <fieldset class="purchase-info">
                        <legend class="xs"></legend>
                        <p class="price" id="price">
                            <b>
                                <%=Model.Price %>
                            </b>
                        </p>
                        <p class="add-to-cart">
                            <a href="/Home/SecondPage/6">
                                <img alt="" width="101" height="23" src="../../Resource/CSSImages/product-details-add-to-cart.png" />
                            </a>
                        </p>
                    </fieldset>
                    </form>
                </div>
                <div class="module_btm">
                </div>
            </div>
            <div class="module details-scan">
                <div class="module_top">
                </div>
                <div class="modulecontent">
                    <h2 id="overview" class="overview">
                        概览
                    </h2>
                    <div class="overview-full">
                     <%=Model.Extend3 %>
                    </div>
                </div>
                <div class="module_btm">
                </div>
            </div>
        </div>
    </div>
    <div>
        <%Html.RenderPartial("LeftNavUserControl"); %>
    </div>
</asp:Content>
