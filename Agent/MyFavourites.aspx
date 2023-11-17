<%@ Page Language="C#"  MasterPageFile="~/Agent/Agent.Master" AutoEventWireup="true" CodeBehind="MyFavourites.aspx.cs" Inherits="Jewar_API.MyFavourites" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row align-items-center pb40">
    <div class="col-lg-12">
  <div class="dashboard_title_area">
    <h2>My Favorites</h2>
    <p class="text">We are glad to see you again!</p>
  </div>
</div>
        </div>
     


<div class="row">
  <div class="col-xl-12">
    <div class="ps-widget bgc-white bdrs12 default-box-shadow2 p30 p20-xs mb30 overflow-hidden position-relative">
      <div class="row">
           <asp:Repeater ID="rptFavourites" runat="server">
     <ItemTemplate>
        <div class="col-sm-6 col-xl-3">
          <div class="listing-style1 style2">
            <div class="list-thumb">
              <a href="#" class="tag-del" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete Item"><span class="fas fa-trash-can"></span></a>
              <img class="w-100" src="images/listings/g1-1.jpg" alt="">
              <div class="list-tag fz12"><span class="flaticon-electricity me-2"></span>FEATURED</div>
              <div class="list-price">$14,000 / <span>mo</span></div>
            </div>
            <div class="list-content">
              <h6 class="list-title"><a href="page-property-single-v1.html">Equestrian Family Home</a></h6>
              <p class="list-text">California City, CA, USA</p>
              <div class="list-meta d-flex align-items-center">
                <a href="#"><span class="flaticon-bed"></span>3 bed</a>
                <a href="#"><span class="flaticon-shower"></span>4 bath</a>
                <a href="#"><span class="flaticon-expand"></span>1200 sqft</a>
              </div>
              <hr class="mt-2 mb-2">
              <div class="list-meta2 d-flex justify-content-between align-items-center">
                <span class="for-what">For Rent</span>
                <div class="icons d-flex align-items-center">
                  <a href="#"><span class="flaticon-fullscreen"></span></a>
                  <a href="#"><span class="flaticon-new-tab"></span></a>
                  <a href="#"><span class="flaticon-like"></span></a>
                </div>
              </div>
            </div>
          </div>
        </div>
             </ItemTemplate>
</asp:Repeater>
      </div>
      <div class="row">
        <div class="mbp_pagination text-center">
          <ul class="page_navigation">
            <li class="page-item">
              <a class="page-link" href="#"> <span class="fas fa-angle-left"></span></a>
            </li>
            <li class="page-item"><a class="page-link" href="#">1</a></li>
            <li class="page-item active" aria-current="page">
              <a class="page-link" href="#">2 <span class="sr-only">(current)</span></a>
            </li>
            <li class="page-item"><a class="page-link" href="#">3</a></li>
            <li class="page-item"><a class="page-link" href="#">4</a></li>
            <li class="page-item"><a class="page-link" href="#">5</a></li>
            <li class="page-item"><a class="page-link" href="#">...</a></li>
            <li class="page-item"><a class="page-link" href="#">20</a></li>
            <li class="page-item">
              <a class="page-link" href="#"><span class="fas fa-angle-right"></span></a>
            </li>
          </ul>
          <p class="mt10 pagination_page_count text-center">1 – 20 of 300+ property available</p>
        </div>
      </div>
    </div>
  </div>
</div>

    </asp:Content>