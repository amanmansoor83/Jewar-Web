<%@ Page Language="C#"  MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Jewar_API.index" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   

 <div class="body_content">
  <!-- Home Banner Style V1 -->
  <section class="home-banner-style4 p0 bgc-white">
    <div class="home-style4 maxw1600 bdrs24 position-relative mx-auto mx20-lg">
      <div class="container">
        <div class="row">
          <div class="col-xl-9">
            <div class="inner-banner-style4">
              <h2 class="hero-title animate-up-1">Easy Way to Find a <br class="d-none d-md-block"> Perfect Property</h2>
              <p class="hero-text fz15 animate-up-2">From as low as $10 per day with limited time offer discounts.</p>
              <div class="home4-floatin-img">
                <img class="img-1 spin-left d-none d-xl-block" src="images/about/element-10.png" alt="">
                <img class="img-2 bounce-y d-none d-xl-block" src="images/about/element-9.png" alt="">
                <a class="popup-iframe popup-youtube bounce-y d-flex align-items-center justify-content-start justify-content-xl-center fz14 fw600 ff-heading" href="https://www.youtube.com/watch?v=7EHnQ0VM4KY">Watch Video <span class="video-icon flaticon-play fz12 ml20"></span></a>
              </div>
              <div class="advance-search-tab mt60 mt30-lg mx-auto animate-up-3">
                <ul class="nav nav-tabs p-0 m-0" id="myTab" role="tablist">
                  <li class="nav-item" role="presentation">
                    <button class="nav-link active" id="btnBuySearch" data-bs-toggle="tab" data-bs-target="#home" type="button" role="tab" aria-controls="home" aria-selected="true">Buy</button>
                  </li>
                  <li class="nav-item" role="presentation">
                    <button class="nav-link" id="btnRentSearch" data-bs-toggle="tab" data-bs-target="#profile" type="button" role="tab" aria-controls="profile" aria-selected="false">Rent</button>
                  </li>
                  <li class="nav-item" role="presentation">
                    <button class="nav-link" id="btnSoldSearch" data-bs-toggle="tab" data-bs-target="#contact" type="button" role="tab" aria-controls="contact" aria-selected="false">Sold</button>
                  </li>
                </ul>
                <div class="tab-content" id="myTabContent">
                  <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                    <div class="advance-content-style1">
                      <div class="row">
                        <div class="col-md-8 col-lg-9">
                          <div class="advance-search-field position-relative text-start">
                            <form action="#" method="get" class="form-search position-relative" accept-charset="utf-8">
                              <div class="box-search">
                                <span class="icon flaticon-home-1"></span>
                                <input class="form-control bgc-f7 bdrs12" type="text" name="search" id="txtSearchBuyText" placeholder="Search products…">
                              </div>
                            </form>
                          </div>
                        </div>
                        <div class="col-md-4 col-lg-3">
                          <div class="d-flex align-items-center justify-content-start justify-content-md-center mt-3 mt-md-0">
                            <button class="advance-search-btn" type="button" data-bs-toggle="modal" data-bs-target="#exampleModal"><span class="flaticon-settings"></span> Advanced</button>
                            <button class="advance-search-icon ud-btn btn-dark ms-4" type="button" id="btnHomeBuySearch"><span class="flaticon-search"></span></button>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">
                    <div class="advance-content-style1">
                      <div class="row">
                        <div class="col-md-8 col-lg-9">
                          <div class="advance-search-field position-relative text-start">
                            <form action="#" method="get" class="form-search position-relative" accept-charset="utf-8">
                              <div class="box-search">
                                <span class="icon flaticon-home-1"></span>
                                <input class="form-control bgc-f7 bdrs12" type="text" name="search" id="txtSearchRentText" placeholder="Search products…">
                              </div>
                            </form>
                          </div>
                        </div>
                        <div class="col-md-4 col-lg-3">
                          <div class="d-flex align-items-center justify-content-start justify-content-md-center mt-3 mt-md-0">
                            <button class="advance-search-btn" type="button" data-bs-toggle="modal" data-bs-target="#exampleModal"><span class="flaticon-settings"></span> Advanced</button>
                            <button class="advance-search-icon ud-btn btn-dark ms-4" type="button" id="btnHomeRentSearch"><span class="flaticon-search"></span></button>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="tab-pane fade" id="contact" role="tabpanel" aria-labelledby="contact-tab">
                    <div class="advance-content-style1">
                      <div class="row">
                        <div class="col-md-8 col-lg-9">
                          <div class="advance-search-field position-relative text-start">
                            <form action="#" method="get" class="form-search position-relative" accept-charset="utf-8">
                              <div class="box-search">
                                <span class="icon flaticon-home-1"></span>
                                <input class="form-control bgc-f7 bdrs12" type="text" name="search" id="txtSearchSoldText" placeholder="Search products…">
                              </div>
                            </form>
                          </div>
                        </div>
                        <div class="col-md-4 col-lg-3">
                          <div class="d-flex align-items-center justify-content-start justify-content-md-center mt-3 mt-md-0">
                            <button class="advance-search-btn" type="button" data-bs-toggle="modal" data-bs-target="#exampleModal"><span class="flaticon-settings"></span> Advanced</button>
                            <button class="advance-search-icon ud-btn btn-dark ms-4" type="button" id="btnHomeSoldSearch" ><span class="flaticon-search"></span></button>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>              
              </div>
              <div class="home4-icon-style mt30 d-none d-sm-flex animate-up-4">
                <a href="#" class="d-flex align-items-center dark-color ff-heading me-4"><i class="icon mr10 flaticon-home-1"></i> Houses</a>
                <a href="#" class="d-flex align-items-center dark-color ff-heading me-4"><i class="icon mr10 flaticon-corporation"></i> Apartments</a>
                <a href="#" class="d-flex align-items-center dark-color ff-heading me-4"><i class="icon mr10 flaticon-network"></i> Office</a>
                <a href="#" class="d-flex align-items-center dark-color ff-heading"><i class="icon mr10 flaticon-garden"></i> Villa</a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Property Cities -->
  <section class="pb80 bgc-white">
    <div class="container">
      <div class="row align-items-center wow fadeInUp" data-wow-delay="100ms">
        <div class="col-lg-9">
          <div class="main-title2">
            <h2 class="title">Properties by Cities</h2>
            <p class="paragraph">Aliquam lacinia diam quis lacus euismod</p>
          </div>
        </div>
        <div class="col-lg-3">
          <div class="text-start text-lg-end mb-3">
            <a class="ud-btn2" href="page-property-single-v1.html">See All Cities<i class="fal fa-arrow-right-long"></i></a>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-lg-12 wow fadeInUp" data-wow-delay="300ms">
          <div class="property-city-slider style2 dots_none slider-dib-sm slider-6-grid vam_nav_style owl-theme owl-carousel">
                <asp:Repeater ID="rptPropertiesbyCities" runat="server">
      <ItemTemplate>


            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="feature-style3 mb30 text-center">
                  <div class="feature-img rounded-circle"><img class="w-100" src="images/listings/cp-m-1.png" alt=""></div>
                  <div class="feature-content pt25">
                    <h6 class="title mb-1"><%#Eval("City") %></h6>
                    <p class="fz15 fw400 dark-color mb-0"><%#Eval("TotalProperties") %> Properties</p>
                  </div>
                </div>
              </a>
            </div>
               </ItemTemplate>
 </asp:Repeater>

          <%--  <div class="item">
              <a href="page-property-single-v1.html">
                <div class="feature-style3 mb30 text-center">
                  <div class="feature-img rounded-circle"><img class="w-100" src="images/listings/cp-m-2.png" alt=""></div>
                  <div class="feature-content pt25">
                    <h6 class="title mb-1">Chicago</h6>
                    <p class="fz15 fw400 dark-color mb-0">12 Properties</p>
                  </div>
                </div>
              </a>
            </div>
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="feature-style3 mb30 text-center">
                  <div class="feature-img rounded-circle"><img class="w-100" src="images/listings/cp-m-3.png" alt=""></div>
                  <div class="feature-content pt25">
                    <h6 class="title mb-1">Manhattan</h6>
                    <p class="fz15 fw400 dark-color mb-0">12 Properties</p>
                  </div>
                </div>
              </a>
            </div>
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="feature-style3 mb30 text-center">
                  <div class="feature-img rounded-circle"><img class="w-100" src="images/listings/cp-m-4.png" alt=""></div>
                  <div class="feature-content pt25">
                    <h6 class="title mb-1">San Francisco</h6>
                    <p class="fz15 fw400 dark-color mb-0">12 Properties</p>
                  </div>
                </div>
              </a>
            </div>
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="feature-style3 mb30 text-center">
                  <div class="feature-img rounded-circle"><img class="w-100" src="images/listings/cp-m-5.png" alt=""></div>
                  <div class="feature-content pt25">
                    <h6 class="title mb-1">Los Angeles</h6>
                    <p class="fz15 fw400 dark-color mb-0">12 Properties</p>
                  </div>
                </div>
              </a>
            </div>
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="feature-style3 mb30 text-center">
                  <div class="feature-img rounded-circle"><img class="w-100" src="images/listings/cp-m-6.png" alt=""></div>
                  <div class="feature-content pt25">
                    <h6 class="title mb-1">California</h6>
                    <p class="fz15 fw400 dark-color mb-0">12 Properties</p>
                  </div>
                </div>
              </a>
            </div>--%>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Popular Property -->
  <section class="pt-0 pb60">
    <div class="container">
      <div class="row wow fadeInUp" data-wow-delay="100ms">
        <div class="col-lg-6">
          <div class="main-title2">
            <h2 class="title">Discover Popular Properties</h2>
            <p class="paragraph">Aliquam lacinia diam quis lacus euismod</p>
          </div>
        </div>
        <div class="col-lg-6">
          <div class="dark-light-navtab style2 text-start text-lg-end mt-0 mt-lg-4 mb-4">
            <ul class="nav nav-pills justify-content-start justify-content-lg-end" id="pills-tab" role="tablist">
              <li class="nav-item" role="presentation">
                <button class="nav-link mb10-sm active" id="pills-house-tab" data-bs-toggle="pill" data-bs-target="#pills-house" type="button" role="tab" aria-controls="pills-house" aria-selected="true">House</button>
              </li>
              <li class="nav-item" role="presentation">
                <button class="nav-link mb10-sm" id="pills-villa-tab" data-bs-toggle="pill" data-bs-target="#pills-villa" type="button" role="tab" aria-controls="pills-villa" aria-selected="false">Villa</button>
              </li>
              <li class="nav-item" role="presentation">
                <button class="nav-link mb10-sm" id="pills-office-tab" data-bs-toggle="pill" data-bs-target="#pills-office" type="button" role="tab" aria-controls="pills-office" aria-selected="false">Office</button>
              </li>
              <li class="nav-item" role="presentation">
                <button class="nav-link mb10-sm me-0" id="pills-apartments-tab" data-bs-toggle="pill" data-bs-target="#pills-apartments" type="button" role="tab" aria-controls="pills-apartments" aria-selected="false">Apartments</button>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-lg-12 wow fadeInUp" data-wow-delay="300ms">
          <div class="tab-content" id="pills-tabContent">
            <div class="tab-pane fade show active" id="pills-house" role="tabpanel" aria-labelledby="pills-house-tab">
              <div class="row">
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-1.jpg" alt="">
                      <div class="list-tag fz12"><span class="flaticon-electricity me-2"></span>FEATURED</div>
                      <div class="list-tag2 fz12">FOR SALE</div>
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Equestrian Family Home</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-2.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$82,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Luxury villa in Rego Park</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-3.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$63,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Villa on Hollywood Boulevard</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-4.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Northwest Office Space</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-5.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Affordable Green Villa House</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-6.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Sky Pool Villa House</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="tab-pane fade" id="pills-villa" role="tabpanel" aria-labelledby="pills-villa-tab">
              <div class="row">
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-1.jpg" alt="">
                      <div class="list-tag fz12"><span class="flaticon-electricity me-2"></span>FEATURED</div>
                      <div class="list-tag2 fz12">FOR SALE</div>
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Equestrian Family Home</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-2.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$82,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Luxury villa in Rego Park</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-3.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$63,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Villa on Hollywood Boulevard</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-4.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Northwest Office Space</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-5.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Affordable Green Villa House</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-6.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Sky Pool Villa House</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="tab-pane fade" id="pills-office" role="tabpanel" aria-labelledby="pills-office-tab">
              <div class="row">
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-1.jpg" alt="">
                      <div class="list-tag fz12"><span class="flaticon-electricity me-2"></span>FEATURED</div>
                      <div class="list-tag2 fz12">FOR SALE</div>
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Equestrian Family Home</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-2.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$82,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Luxury villa in Rego Park</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-3.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$63,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Villa on Hollywood Boulevard</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-4.jpg" alt="">
                      <div class="list-tag fz12"><span class="flaticon-electricity me-2"></span>FEATURED</div>
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Northwest Office Space</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-5.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Affordable Green Villa House</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-6.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Sky Pool Villa House</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="tab-pane fade" id="pills-apartments" role="tabpanel" aria-labelledby="pills-apartments-tab">
              <div class="row">
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-1.jpg" alt="">
                      <div class="list-tag fz12"><span class="flaticon-electricity me-2"></span>FEATURED</div>
                      <div class="list-tag2 fz12">FOR SALE</div>
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Equestrian Family Home</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-2.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$82,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Luxury villa in Rego Park</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-3.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$63,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Villa on Hollywood Boulevard</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-4.jpg" alt="">
                      <div class="list-tag fz12"><span class="flaticon-electricity me-2"></span>FEATURED</div>
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Northwest Office Space</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-5.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Affordable Green Villa House</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6 col-xl-4">
                  <div class="listing-style6">
                    <div class="list-thumb">
                      <img class="w-100" src="images/listings/md-6.jpg" alt="">
                      <div class="list-meta">
                        <div class="icons">
                          <a href="#"><span class="flaticon-like"></span></a>
                          <a href="#"><span class="flaticon-new-tab"></span></a>
                          <a href="#"><span class="flaticon-fullscreen"></span></a>
                        </div>
                      </div>
                    </div>
                    <div class="list-content">
                      <div class="list-price mb-2">$14,000</div>
                      <h6 class="list-title"><a href="page-property-single-v1.html">Sky Pool Villa House</a></h6>
                      <p class="list-text">California City, CA, USA</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- CTA Banner -->
  <section class="pt30 pb-0">
    <div class="cta-banner3 bgc-thm-light mx-auto maxw1600 pt100 pt60-lg pb90 pb60-lg bdrs24 position-relative overflow-hidden mx20-lg">
      <div class="container">
        <div class="row">
          <div class="col-md-6 col-lg-5 pl30-md pl15-xs wow fadeInRight" data-wow-delay="500ms">
            <div class="mb30">
              <h2 class="title text-capitalize">Let’s find the right <br class="d-none d-md-block"> selling option for you</h2>
            </div>
            <div class="why-chose-list style2">
              <div class="list-one d-flex align-items-start mb30">
                <span class="list-icon flex-shrink-0 flaticon-security"></span>
                <div class="list-content flex-grow-1 ml20">
                  <h6 class="mb-1">Property Management</h6>
                  <p class="text mb-0 fz15">Nullam sollicitudin blandit eros eu pretium. Nullam maximus ultricies auctor.</p>
                </div>
              </div>
              <div class="list-one d-flex align-items-start mb30">
                <span class="list-icon flex-shrink-0 flaticon-keywording"></span>
                <div class="list-content flex-grow-1 ml20">
                  <h6 class="mb-1">Mortgage Services</h6>
                  <p class="text mb-0 fz15">Nullam sollicitudin blandit eros eu pretium. Nullam maximus ultricies auctor.</p>
                </div>
              </div>
              <div class="list-one d-flex align-items-start mb30">
                <span class="list-icon flex-shrink-0 flaticon-investment"></span>
                <div class="list-content flex-grow-1 ml20">
                  <h6 class="mb-1">Currency Services</h6>
                  <p class="text mb-0 fz15">Nullam sollicitudin blandit eros eu pretium. Nullam maximus ultricies auctor.</p>
                </div>
              </div>
            </div>
            <a href="page-property-single-v1.html" class="ud-btn btn-dark">Learn More<i class="fal fa-arrow-right-long"></i></a>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Funfact -->
  <section class="pt45 pb-0">
    <div class="container maxw1600 bdrb1 pb50">
      <div class="row justify-content-center wow fadeInUp" data-wow-delay="300ms">
        <div class="col-sm-6 col-lg-4 col-xl-2">
          <div class="funfact_one text-center">
            <div class="details">
              <ul class="ps-0 mb-0">
                <li><div class="timer">400</div></li>
              </ul>
              <p class="text mb-0">Stores around the world</p>
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-lg-4 col-xl-2">
          <div class="funfact_one text-center">
            <div class="details">
              <ul class="ps-0 mb-0 d-flex justify-content-center">
                <li><div class="timer">200</div></li>
                <li><span>+</span></li>
              </ul>
              <p class="text mb-0">Stores around the world</p>
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-lg-4 col-xl-2">
          <div class="funfact_one text-center">
            <div class="details">
              <ul class="ps-0 mb-0 d-flex justify-content-center">
                <li><div class="timer">1</div></li>
                <li><span>K</span></li>
                <li><span>+</span></li>
              </ul>
              <p class="text mb-0">Stores around the world</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Explore Apartment -->
  <section class="pb80 pb30-md">
    <div class="container">
      <div class="row">
        <div class="col-lg-6">
          <div class="main-title wow fadeInUp" data-wow-delay="100ms">
            <h2 class="title">Explore Apartment Types</h2>
            <p class="paragraph">Get some Inspirations from 1800+ skills</p>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-lg-12">
          <div class="explore-apartment-5col-slider navi_pagi_top_right slider-5-grid owl-carousel owl-theme wow fadeInUp" data-wow-delay="300ms">
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="apartment-style1">
                  <div class="apartment-img"><img class="w-100" src="images/listings/as-1.jpg" alt=""></div>
                  <div class="apartment-content">
                    <h6 class="title mb-0">House</h6>
                    <p class="text mb-0">22 Properties</p>
                  </div>
                </div>
              </a>
            </div>
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="apartment-style1">
                  <div class="apartment-img"><img class="w-100" src="images/listings/as-2.jpg" alt=""></div>
                  <div class="apartment-content">
                    <h6 class="title mb-0">Apartments</h6>
                    <p class="text mb-0">22 Properties</p>
                  </div>
                </div>
              </a>
            </div>
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="apartment-style1">
                  <div class="apartment-img"><img class="w-100" src="images/listings/as-3.jpg" alt=""></div>
                  <div class="apartment-content">
                    <h6 class="title mb-0">Office</h6>
                    <p class="text mb-0">22 Properties</p>
                  </div>
                </div>
              </a>
            </div>
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="apartment-style1">
                  <div class="apartment-img"><img class="w-100" src="images/listings/as-4.jpg" alt=""></div>
                  <div class="apartment-content">
                    <h6 class="title mb-0">Villa</h6>
                    <p class="text mb-0">22 Properties</p>
                  </div>
                </div>
              </a>
            </div>
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="apartment-style1">
                  <div class="apartment-img"><img class="w-100" src="images/listings/as-5.jpg" alt=""></div>
                  <div class="apartment-content">
                    <h6 class="title mb-0">House</h6>
                    <p class="text mb-0">22 Properties</p>
                  </div>
                </div>
              </a>
            </div>
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="apartment-style1">
                  <div class="apartment-img"><img class="w-100" src="images/listings/as-2.jpg" alt=""></div>
                  <div class="apartment-content">
                    <h6 class="title mb-0">Apartments</h6>
                    <p class="text mb-0">22 Properties</p>
                  </div>
                </div>
              </a>
            </div>
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="apartment-style1">
                  <div class="apartment-img"><img class="w-100" src="images/listings/as-3.jpg" alt=""></div>
                  <div class="apartment-content">
                    <h6 class="title mb-0">Office</h6>
                    <p class="text mb-0">22 Properties</p>
                  </div>
                </div>
              </a>
            </div>
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="apartment-style1">
                  <div class="apartment-img"><img class="w-100" src="images/listings/as-4.jpg" alt=""></div>
                  <div class="apartment-content">
                    <h6 class="title mb-0">Villa</h6>
                    <p class="text mb-0">22 Properties</p>
                  </div>
                </div>
              </a>
            </div>
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="apartment-style1">
                  <div class="apartment-img"><img class="w-100" src="images/listings/as-5.jpg" alt=""></div>
                  <div class="apartment-content">
                    <h6 class="title mb-0">Townhome</h6>
                    <p class="text mb-0">22 Properties</p>
                  </div>
                </div>
              </a>
            </div>
            <div class="item">
              <a href="page-property-single-v1.html">
                <div class="apartment-style1">
                  <div class="apartment-img"><img class="w-100" src="images/listings/as-5.jpg" alt=""></div>
                  <div class="apartment-content">
                    <h6 class="title mb-0">Townhome</h6>
                    <p class="text mb-0">22 Properties</p>
                  </div>
                </div>
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- About Us -->
  <section class="pt0 pb40-md">
    <div class="cta-banner bgc-f7 mx-auto maxw1600 pt70 pb140 pb30-md bdrs12 position-relative mx20-lg px20-sm">
      <div class="container">
        <div class="row align-items-start align-items-xl-center">
          <div class="col-md-10 col-lg-7 col-xl-6">
            <div class="position-relative mb35 mb0-sm wow fadeInRight" data-wow-delay="300ms">
              <div class="exclusive-agent-widget mb30-sm">
                <h4 class="title mb20"><span class="text-thm">200+</span> Exclusive Agents</h4>
                <div class="thumb d-flex align-items-center mb20">
                  <div class="flex-shrink-0">
                    <img class="wa" src="images/team/ea-1.png" alt="">
                  </div>
                  <div class="flex-grow-1 ml20">
                    <h6 class="title fz14 mb-0">Marvin McKinney</h6>
                    <p class="fz13 mb-0">Designer</p>
                  </div>
                </div>
                <div class="thumb d-flex align-items-center mb20">
                  <div class="flex-shrink-0">
                    <img class="wa" src="images/team/ea-2.png" alt="">
                  </div>
                  <div class="flex-grow-1 ml20">
                    <h6 class="title fz14 mb-0">Ralph Edwards</h6>
                    <p class="fz13 mb-0">Designer</p>
                  </div>
                </div>
                <div class="thumb d-flex align-items-center mb20">
                  <div class="flex-shrink-0">
                    <img class="wa" src="images/team/ea-3.png" alt="">
                  </div>
                  <div class="flex-grow-1 ml20">
                    <h6 class="title fz14 mb-0">Annette Black</h6>
                    <p class="fz13 mb-0">Designer</p>
                  </div>
                </div>
                <div class="thumb d-flex align-items-center">
                  <div class="flex-shrink-0">
                    <img class="wa" src="images/team/ea-4.png" alt="">
                  </div>
                  <div class="flex-grow-1 ml20">
                    <h6 class="title fz14 mb-0">Jane Cooper</h6>
                    <p class="fz13 mb-0">Designer</p>
                  </div>
                </div>
              </div>
              <div class="exclusive-agent-single mb30-sm">
                <div class="agent-img"><img src="images/team/agent-5.jpg" alt=""></div>
                <div class="agent-content pt20">
                  <h6 class="title mb-0">Marvin McKinney</h6>
                  <p class="fz15 mb-0">Designer</p>
                </div>
              </div>
              <div class="img-box-10 position-relative d-none d-xl-block">
                <img class="img-1 spin-right" src="images/about/element-3.png" alt="">
                <img class="img-2 bounce-x" src="images/about/element-5.png" alt="">
                <img class="img-3 bounce-y" src="images/about/element-6.png" alt="">
                <img class="img-4 bounce-y" src="images/about/element-7.png" alt="">
                <img class="img-5 spin-right" src="images/about/element-8.png" alt="">
              </div>
            </div>
          </div>
          <div class="col-md-8 col-lg-5 col-xl-5 offset-xl-1">
            <div class="about-box-1 pe-4 mt100 mt0-lg mb30-lg wow fadeInLeft" data-wow-delay="300ms">
              <h2 class="title mb20">Let’s find the right selling option for you</h2>
              <p class="text mb55 mb30-md fz14">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do <br class="d-none d-xl-block"> eiusmod tempor incididunt.</p>
              <div class="list-style1 mb60 mb30-md">
                <ul>
                  <li><i class="far fa-check text-white bgc-dark fz15"></i>Find excellent deals</li>
                  <li><i class="far fa-check text-white bgc-dark fz15"></i>Friendly host & Fast support</li>
                  <li><i class="far fa-check text-white bgc-dark fz15"></i>List your own property</li>
                </ul>
              </div>
              <a href="page-property-single-v1.html" class="ud-btn btn-dark">See More<i class="fal fa-arrow-right-long"></i></a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Testimonials -->
  <section class="pt-0 pb100 pb30-md">
    <div class="container">
      <div class="row">
        <div class="col-lg-6 wow fadeInUp" data-wow-delay="100ms">
          <div class="main-title">
            <h2 class="title">People Love Living with Realton</h2>
            <p class="paragraph">Aliquam lacinia diam quis lacus euismod</p>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-lg-12">
          <div class="testimonial-slider navi_pagi_top_right slider-3-grid owl-carousel owl-theme wow fadeInUp" data-wow-delay="300ms">
            <div class="item">
              <div class="testimonial-style1 position-relative bdr1">
                <div class="testimonial-content">
                  <h5 class="title">Great Work</h5>
                  <span class="icon fas fa-quote-left"></span>
                  <p class="text">“Amazing design, easy to customize and a design quality superlative account on its cloud platform for the optimized performance. And we didn’t on our original designs.”</p>
                  <div class="testimonial-review">
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a href="#"><i class="fas fa-star"></i></a>
                  </div>
                </div>
                <div class="thumb d-flex align-items-center">
                  <div class="flex-shrink-0">
                    <img class="wa" src="images/testimonials/testimonial-1.png" alt="">
                  </div>
                  <div class="flex-grow-1 ms-3">
                    <h6 class="mb-0">Leslie Alexander</h6>
                    <p class="mb-0">Nintendo</p>
                  </div>
                </div>
              </div>
            </div>
            <div class="item">
              <div class="testimonial-style1 position-relative bdr1">
                <div class="testimonial-content">
                  <h5 class="title">Great Work</h5>
                  <span class="icon fas fa-quote-left"></span>
                  <p class="text">“Amazing design, easy to customize and a design quality superlative account on its cloud platform for the optimized performance. And we didn’t on our original designs.”</p>
                  <div class="testimonial-review">
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a href="#"><i class="fas fa-star"></i></a>
                  </div>
                </div>
                <div class="thumb d-flex align-items-center">
                  <div class="flex-shrink-0">
                    <img class="wa" src="images/testimonials/testimonial-2.png" alt="">
                  </div>
                  <div class="flex-grow-1 ms-3">
                    <h6 class="mb-0">Floyd Miles</h6>
                    <p class="mb-0">Bank of America</p>
                  </div>
                </div>
              </div>
            </div>
            <div class="item">
              <div class="testimonial-style1 position-relative bdr1">
                <div class="testimonial-content">
                  <h5 class="title">Great Work</h5>
                  <span class="icon fas fa-quote-left"></span>
                  <p class="text">“Amazing design, easy to customize and a design quality superlative account on its cloud platform for the optimized performance. And we didn’t on our original designs.”</p>
                  <div class="testimonial-review">
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a href="#"><i class="fas fa-star"></i></a>
                  </div>
                </div>
                <div class="thumb d-flex align-items-center">
                  <div class="flex-shrink-0">
                    <img class="wa" src="images/testimonials/testimonial-3.png" alt="">
                  </div>
                  <div class="flex-grow-1 ms-3">
                    <h6 class="mb-0">Dianne Russell</h6>
                    <p class="mb-0">Facebook</p>
                  </div>
                </div>
              </div>
            </div>
            <div class="item">
              <div class="testimonial-style1 position-relative bdr1">
                <div class="testimonial-content">
                  <h5 class="title">Great Work</h5>
                  <span class="icon fas fa-quote-left"></span>
                  <p class="text">“Amazing design, easy to customize and a design quality superlative account on its cloud platform for the optimized performance. And we didn’t on our original designs.”</p>
                  <div class="testimonial-review">
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a href="#"><i class="fas fa-star"></i></a>
                  </div>
                </div>
                <div class="thumb d-flex align-items-center">
                  <div class="flex-shrink-0">
                    <img class="wa" src="images/testimonials/testimonial-3.png" alt="">
                  </div>
                  <div class="flex-grow-1 ms-3">
                    <h6 class="mb-0">Dianne Russell</h6>
                    <p class="mb-0">Facebook</p>
                  </div>
                </div>
              </div>
            </div>
            <div class="item">
              <div class="testimonial-style1 position-relative bdr1">
                <div class="testimonial-content">
                  <h5 class="title">Great Work</h5>
                  <span class="icon fas fa-quote-left"></span>
                  <p class="text">“Amazing design, easy to customize and a design quality superlative account on its cloud platform for the optimized performance. And we didn’t on our original designs.”</p>
                  <div class="testimonial-review">
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a class="me-1" href="#"><i class="fas fa-star"></i></a>
                    <a href="#"><i class="fas fa-star"></i></a>
                  </div>
                </div>
                <div class="thumb d-flex align-items-center">
                  <div class="flex-shrink-0">
                    <img class="wa" src="images/testimonials/testimonial-3.png" alt="">
                  </div>
                  <div class="flex-grow-1 ms-3">
                    <h6 class="mb-0">Dianne Russell</h6>
                    <p class="mb-0">Facebook</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Our Partners --> 
  <section class="our-partners p-0">
    <div class="container">
      <div class="row">
        <div class="col-lg-12 wow fadeInUp" data-wow-delay="100ms">
          <div class="main-title text-center">
            <h6>Trusted by the world’s best</h6>
          </div>
        </div>
        <div class="col-lg-12">
          <div class="dots_none nav_none slider-dib-sm slider-6-grid owl-carousel owl-theme wow fadeInUp" data-wow-delay="300ms">
            <div class="item">
              <div class="partner_item"><img class="wa m-auto" src="images/partners/1.png" alt="1.png"></div>
            </div>
            <div class="item">
              <div class="partner_item"><img class="wa m-auto" src="images/partners/2.png" alt="2.png"></div>
            </div>
            <div class="item">
              <div class="partner_item"><img class="wa m-auto" src="images/partners/3.png" alt="3.png"></div>
            </div>
            <div class="item">
              <div class="partner_item"><img class="wa m-auto" src="images/partners/4.png" alt="4.png"></div>
            </div>
            <div class="item">
              <div class="partner_item"><img class="wa m-auto" src="images/partners/5.png" alt="5.png"></div>
            </div>
            <div class="item">
              <div class="partner_item"><img class="wa m-auto" src="images/partners/6.png" alt="6.png"></div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Our Blog -->
  <section class="mb75 mb0-md pb30-md">
    <div class="container">
      <div class="row">
        <div class="col-lg-6 m-auto wow fadeInUp" data-wow-delay="100ms">
          <div class="main-title text-start text-md-center">
            <h2 class="title">From Our Blog</h2>
            <p class="paragraph">Aliquam lacinia diam quis lacus euismod</p>
          </div>
        </div>
      </div>
      <div class="row wow fadeInUp" data-wow-delay="300ms">
        <div class="col-sm-6 col-lg-4">
          <div class="blog-style1">
            <div class="blog-img"><img class="w-100" src="images/blog/blog-1.jpg" alt=""></div>
            <div class="blog-content">
              <div class="date">
                <span class="month">July</span>
                <span class="day">28</span>
              </div>
              <a class="tag" href="#">Living Room</a>
              <h6 class="title mt-1"><a href="page-blog-single.html">Private Contemporary Home Balancing Openness</a></h6>
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-lg-4">
          <div class="blog-style1">
            <div class="blog-img"><img class="w-100" src="images/blog/blog-2.jpg" alt=""></div>
            <div class="blog-content">
              <div class="date">
                <span class="month">July</span>
                <span class="day">28</span>
              </div>
              <a class="tag" href="#">Living Room</a>
              <h6 class="title mt-1"><a href="page-blog-single.html">Private Contemporary Home Balancing Openness</a></h6>
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-lg-4">
          <div class="blog-style1">
            <div class="blog-img"><img class="w-100" src="images/blog/blog-3.jpg" alt=""></div>
            <div class="blog-content">
              <div class="date">
                <span class="month">July</span>
                <span class="day">28</span>
              </div>
              <a class="tag" href="#">Living Room</a>
              <h6 class="title mt-1"><a href="page-blog-single.html">Private Contemporary Home Balancing Openness</a></h6>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Our CTA --> 
  <section class="our-cta p-0">
    <div class="cta-banner bgc-thm-light mx-auto maxw1600 pt90 pt60-md pb90 pb60-md bdrs12 position-relative mx20-lg px20-md">
      <div class="img-box-5">
        <img class="img-1 bounce-y" src="images/about/element-4.png" alt="">
      </div>
      <div class="container">
        <div class="row">
          <div class="col-lg-7 col-xl-6 wow fadeInLeft">
            <div class="cta-style3">
              <h2 class="cta-title">Get Your Dream House</h2>
              <p class="cta-text mb25">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do <br class="d-none d-md-block"> eiusmod tempor incididunt.</p>
              <a href="page-contact.html" class="ud-btn btn-dark">Register Now <i class="fal fa-arrow-right-long"></i></a>
            </div>
          </div>
          <div class="col-lg-5 col-xl-4 offset-xl-2 d-none d-lg-block wow fadeIn" data-wow-delay="300ms">
            <div class="cta-img">
              <img src="images/about/cta-building-1.png" alt="">
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Our Footer --> 
  <section class="footer-style1 at-home4 pt60 pb-0">
    <div class="container">
      <div class="row">
        <div class="col-sm-6 col-lg-3">
          <div class="footer-widget light-style mb-4 mb-lg-5">
            <a class="footer-logo" href="index.html"><img class="mb40" src="images/header-logo2.svg" alt=""></a>
            <div class="contact-info mb25">
              <p class="text mb5">Address</p>
              <h6 class="info-phone"><a href="%2b(0)-123-050-945-02.html">329 Queensberry Street, North Melbourne VIC 3051, Australia.</a></h6>
            </div>
            <div class="contact-info mb25">
              <p class="text mb5">Total Free Customer Care</p>
              <h6 class="info-phone"><a href="%2b(0)-123-050-945-02.html">+(0) 123 050 945 02</a></h6>
            </div>
            <div class="contact-info">
              <p class="text mb5">Nee Live Support?</p>
              <h6 class="info-mail"><a href="mailto:hi@homez.com">hi@homez.com</a></h6>
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-lg-3">
          <div class="footer-widget mb-4 mb-lg-5 ps-0 ps-lg-5">
            <div class="link-style1 light-style mb30">
              <h6 class="mb25">Popular Search</h6>
              <div class="link-list">
                <a href="#">Apartment for Rent</a>
                <a href="#">Apartment Low to hide</a>
                <a href="#">Offices for Buy</a>
                <a href="#">Offices for Rent</a>
              </div>
            </div>
            <div class="link-style1 light-style mb-4">
              <h6 class="mb20">Discover</h6>
              <ul class="ps-0">
                <li><a href="#">Miami</a></li>
                <li><a href="#">Los Angeles</a></li>
                <li><a href="#">Chicago</a></li>
                <li><a href="#">New York</a></li>
              </ul>
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-lg-3">
          <div class="footer-widget mb-4 mb-lg-5 ps-0 ps-lg-5">
            <div class="link-style1 light-style mb-3">
              <h6 class="mb25">Quick Links</h6>
              <ul class="ps-0">
                <li><a href="#">Terms of Use</a></li>
                <li><a href="#">Privacy Policy</a></li>
                <li><a href="#">Pricing Plans</a></li>
                <li><a href="#">Our Services</a></li>
                <li><a href="#">Contact Support</a></li>
                <li><a href="#">Careers</a></li>
                <li><a href="#">FAQs</a></li>
              </ul>
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-lg-3">
          <div class="footer-widget mb-4 mb-lg-5">
            <div class="mailchimp-widget mb30">
              <h6 class="title mb30">Keep Yourself Up to Date</h6>
              <div class="mailchimp-style1 at-home4 white-version">
                <input type="email" class="form-control" placeholder="Your Email">
                <button class="btn" type="submit"><span class="flaticon-send"></span></button>
              </div>
            </div>
            <div class="app-widget">
              <h5 class="title mb10">Apps</h5>
              <div class="row">
                <div class="col-auto">
                  <a href="#">
                    <div class="app-info light-style d-flex align-items-center mb10">
                      <div class="flex-shrink-0">
                        <i class="fab fa-apple fz30 text-white"></i>
                      </div>
                      <div class="flex-grow-1 ml20">
                        <p class="app-text fz13 mb0">Download on the</p>
                        <h6 class="app-title text-white fz14">Apple Store</h6>
                      </div>
                    </div>
                  </a>
                </div>
                <div class="col-auto">
                  <a href="#">
                    <div class="app-info light-style d-flex align-items-center mb10">
                      <div class="flex-shrink-0">
                        <i class="fab fa-google-play fz30 text-white"></i>
                      </div>
                      <div class="flex-grow-1 ml20">
                        <p class="app-text fz13 mb0">Get in on</p>
                        <h6 class="app-title text-white fz14">Google Play</h6>
                      </div>
                    </div>
                  </a>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="container gray-bdrt1 py-4">
      <div class="row">
        <div class="col-sm-6">
          <div class="text-center text-lg-start">
            <p class="copyright-text ff-heading mb-0">© Homez - All rights reserved</p>
          </div>
        </div>
        <div class="col-sm-6">
          <div class="social-widget text-center text-sm-end">
            <div class="social-style1 light-style">
              <a class="me-2 fw600 fz15" href="#">Follow us</a>
              <a href="#"><i class="fab fa-facebook-f list-inline-item"></i></a>
              <a href="#"><i class="fab fa-twitter list-inline-item"></i></a>
              <a href="#"><i class="fab fa-instagram list-inline-item"></i></a>
              <a href="#"><i class="fab fa-linkedin-in list-inline-item"></i></a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <a class="scrollToHome" href="#"><i class="fas fa-angle-up"></i></a>
</div>

    </asp:Content>