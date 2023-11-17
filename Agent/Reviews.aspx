<%@ Page Language="C#" MasterPageFile="~/Agent/Agent.Master" AutoEventWireup="true" CodeBehind="Reviews.aspx.cs" Inherits="Jewar_API.Reviews" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row align-items-center pb40">
        <div class="col-lg-12">
            <div class="dashboard_title_area">
                <h2>Reviews</h2>
                <p class="text">We are glad to see you again!</p>
            </div>
        </div>
    </div>



    <div class="row">
        <div class="col-xl-12">
            <div class="ps-widget bgc-white bdrs12 default-box-shadow2 p30 mb30 overflow-hidden position-relative">
                <div class="product_single_content">
                    <div class="mbp_pagination_comments">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="total_review d-block d-sm-flex align-items-center justify-content-between mb20">
                                    <h6 class="fz17 mb15"><i class="fas fa-star fz12 pe-2"></i>5.0 · 3 reviews</h6>
                                    <div class="page_control_shorting d-flex align-items-center justify-content-start justify-content-sm-end">
                                        <div class="pcs_dropdown mb15">
                                            <span>Sort by</span>
                                            <select class="selectpicker show-tick">
                                                <option>Newest</option>
                                                <option>Best Seller</option>
                                                <option>Best Match</option>
                                                <option>Price Low</option>
                                                <option>Price High</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Repeater ID="rptReviews" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-12">
                                        <div class="mbp_first position-relative d-block d-sm-flex align-items-center justify-content-start mb30-sm">
                                            <img src="images/blog/comments-2.png" class="mr-3 mb15-xs" alt="comments-2.png">
                                            <div class="ml20 ml0-xs">
                                                <h6 class="mt-0 mb-0"><%#Eval("CustomerName") %></h6>
                                                <div>
                                                    <span class="fz14"><%# Convert.ToDateTime(Eval("CustomerName")).ToString("dd MMM yyyy") %></span>
                                                    <div class="blog-single-review">
                                                        <ul class="mb0 ps-0">
                                                            <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                            <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                            <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                            <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                            <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <p class="text mt20 mb20"><%#Eval("Review") %></p>
                                        <%--<ul class="mb20 ps-0">
                                            <li class="list-inline-item mb5-sm">
                                                <img class="bdrs6" src="images/blog/blog-single-3.jpg" alt="review-img"></li>
                                            <li class="list-inline-item mb5-sm">
                                                <img class="bdrs6" src="images/blog/blog-single-4.jpg" alt="review-img"></li>
                                            <li class="list-inline-item mb5-sm">
                                                <img class="bdrs6" src="images/blog/blog-single-5.jpg" alt="review-img"></li>
                                            <li class="list-inline-item mb5-sm">
                                                <img class="bdrs6" src="images/blog/blog-single-6.jpg" alt="review-img"></li>
                                        </ul>--%>
                                        <div class="review_cansel_btns d-flex bdrb1 pb30">
                                            <a class="dark-color" href="#"><i class="fas fa-reply"></i>Reply</a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>


                          <%--  <div class="col-md-12">
                                <div class="mbp_first position-relative d-block d-sm-flex align-items-center justify-content-start mt30 mb30-sm">
                                    <img src="images/blog/comments-2.png" class="mr-3 mb15-xs" alt="comments-2.png">
                                    <div class="ml20 ml0-xs">
                                        <h6 class="mt-0 mb-0">Darrell Steward</h6>
                                        <div>
                                            <span class="fz14">12 March 2022</span>
                                            <div class="blog-single-review">
                                                <ul class="mb0 ps-0">
                                                    <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                    <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                    <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                    <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                    <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <p class="text mt20 mb20">The second bedroom is a corner room with double windows. The kitchen has fabulous space, new appliances, and a laundry area. Other features include rich herringbone floors. Fully furnished. Elegantly appointed condominium unit situated on premier location. PS6. The wide entry hall leads to a large living room with dining area.</p>
                                <div class="review_cansel_btns d-flex bdrb1 pb30">
                                    <a class="dark-color" href="#"><i class="fas fa-reply"></i>Reply</a>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="mbp_first position-relative d-block d-sm-flex align-items-center justify-content-start mt30 mb30-sm">
                                    <img src="images/blog/comments-2.png" class="mr-3 mb15-xs" alt="comments-2.png">
                                    <div class="ml20 ml0-xs">
                                        <h6 class="mt-0 mb-0">Darrell Steward</h6>
                                        <div>
                                            <span class="fz14">12 March 2022</span>
                                            <div class="blog-single-review">
                                                <ul class="mb0 ps-0">
                                                    <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                    <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                    <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                    <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                    <li class="list-inline-item me-0"><a href="#"><i class="fas fa-star review-color2 fz10"></i></a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <p class="text mt20 mb20">The second bedroom is a corner room with double windows. The kitchen has fabulous space, new appliances, and a laundry area. Other features include rich herringbone floors. Fully furnished. Elegantly appointed condominium unit situated on premier location. PS6. The wide entry hall leads to a large living room with dining area.</p>
                                <div class="review_cansel_btns d-flex">
                                    <a class="dark-color" href="#"><i class="fas fa-reply"></i>Reply</a>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
