<%@ Page Language="C#"  MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="listing.aspx.cs" Inherits="Jewar_API.listing" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   

 <div class="body_content">
  <section class="advance-search-menu bg-white position-relative default-box-shadow2 pt15 pb5 bb1 dn-992">
    <div class="container-fluid">
      <div class="row">
        <div class="col-xl-10">
          <div class="advance-search-list no-box-shadow d-flex justify-content-between">
            <div class="dropdown-lists">
              <ul class="p-0 mb-0">
                <li class="list-inline-item position-relative">
                  <input type="text" class="form-control search-field" placeholder="Enter an address, neighborhood, city, or ZIP code">
                </li>
                <li class="list-inline-item position-relative">
                  <button type="button" class="open-btn mb15 dropdown-toggle" data-bs-toggle="dropdown">For Sale <i class="fa fa-angle-down ms-2"></i></button>
                  <div class="dropdown-menu">
                    <div class="widget-wrapper bdrb1 pb25 mb0 pl20">
                      <h6 class="list-title">Listing Status</h6>
                      <div class="radio-element">
                        <div class="form-check d-flex align-items-center mb10">
                          <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1">
                          <label class="form-check-label" for="flexRadioDefault1">Buy</label>
                        </div>
                        <div class="form-check d-flex align-items-center mb10">
                          <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault2" checked="checked">
                          <label class="form-check-label" for="flexRadioDefault2">Rent</label>
                        </div>
                        <div class="form-check d-flex align-items-center">
                          <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault3">
                          <label class="form-check-label" for="flexRadioDefault3">Sold</label>
                        </div>
                      </div>
                    </div>
                    <div class="text-end mt10 pr10">
                      <button type="button" class="done-btn ud-btn btn-thm drop_btn">Done</button>
                    </div>
                  </div>
                </li>
                <li class="list-inline-item position-relative">
                  <button type="button" class="open-btn mb15 dropdown-toggle" data-bs-toggle="dropdown">Property Type <i class="fa fa-angle-down ms-2"></i></button>
                  <div class="dropdown-menu">
                    <div class="widget-wrapper bdrb1 pb25 mb0 pl20">
                      <h6 class="list-title">Property Type</h6>
                      <div class="checkbox-style1">
                        <label class="custom_checkbox">Houses
                          <input type="checkbox">
                          <span class="checkmark"></span>
                        </label>
                        <label class="custom_checkbox">Apartments
                          <input type="checkbox" checked="checked">
                          <span class="checkmark"></span>
                        </label>
                        <label class="custom_checkbox">Office
                          <input type="checkbox">
                          <span class="checkmark"></span>
                        </label>
                        <label class="custom_checkbox">Villa
                          <input type="checkbox">
                          <span class="checkmark"></span>
                        </label>
                        <label class="custom_checkbox">Townhome
                          <input type="checkbox">
                          <span class="checkmark"></span>
                        </label>
                      </div>
                    </div>
                    <div class="text-end mt10 pr10">
                      <button type="button" class="done-btn ud-btn btn-thm drop_btn2">Done</button>
                    </div>
                  </div>
                </li>
                <li class="list-inline-item position-relative">
                  <button type="button" class="open-btn mb15 dropdown-toggle" data-bs-toggle="dropdown">Price <i class="fa fa-angle-down ms-2"></i></button>
                  <div class="dropdown-menu dd3">
                    <div class="widget-wrapper bdrb1 pb25 mb0 pl20 pr20">
                      <h6 class="list-title">Price Range</h6>
                      <!-- Range Slider Desktop Version -->
                      <div class="range-slider-style1">
                        <div class="range-wrapper">
                          <div class="slider-range mt30 mb20 ui-slider ui-corner-all ui-slider-horizontal ui-widget ui-widget-content"><div class="ui-slider-range ui-corner-all ui-widget-header" style="left: 0.02%; width: 70.967%;"></div><span tabindex="0" class="ui-slider-handle ui-corner-all ui-state-default" style="left: 0.02%;"></span><span tabindex="0" class="ui-slider-handle ui-corner-all ui-state-default" style="left: 70.987%;"></span></div>
                          <div class="text-center">
                            <input type="text" class="amount" placeholder="$20"><span class="fa-sharp fa-solid fa-minus mx-1 dark-color"></span>
                            <input type="text" class="amount2" placeholder="$70987">
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="text-end mt10 pr10">
                      <button type="button" class="done-btn ud-btn btn-thm drop_btn3">Done</button>
                    </div>
                  </div>
                </li>
                <li class="list-inline-item position-relative">
                  <button type="button" class="open-btn mb15 dropdown-toggle" data-bs-toggle="dropdown">Beds / Baths <i class="fa fa-angle-down ms-2"></i></button>
                  <div class="dropdown-menu dd4 pb20">
                    <div class="widget-wrapper pl20 pr20">
                      <h6 class="list-title">Bedrooms</h6>
                      <div class="d-flex">
                        <div class="selection">
                          <input id="any2" name="beds" type="radio" checked>
                          <label for="any2">any</label>
                        </div>
                        <div class="selection">
                          <input id="oneplus2" name="beds" type="radio">
                          <label for="oneplus2">1+</label>
                        </div>
                        <div class="selection">
                          <input id="twoplus2" name="beds" type="radio">
                          <label for="twoplus2">2+</label>
                        </div>
                        <div class="selection">
                          <input id="threeplus2" name="beds" type="radio">
                          <label for="threeplus2">3+</label>
                        </div>
                        <div class="selection">
                          <input id="fourplus2" name="beds" type="radio">
                          <label for="fourplus2">4+</label>
                        </div>
                        <div class="selection">
                          <input id="fiveplus2" name="beds" type="radio">
                          <label for="fiveplus2">5+</label>
                        </div>
                      </div>
                    </div>
                    <div class="widget-wrapper bdrb1 pb25 mb0 pl20 pr20">
                      <h6 class="list-title">Bathrooms</h6>
                      <div class="d-flex">
                        <div class="selection">
                          <input id="bathany2" name="bath" type="radio" checked>
                          <label for="bathany2">any</label>
                        </div>
                        <div class="selection">
                          <input id="bathoneplus2" name="bath" type="radio">
                          <label for="bathoneplus2">1+</label>
                        </div>
                        <div class="selection">
                          <input id="bathtwoplus2" name="bath" type="radio">
                          <label for="bathtwoplus2">2+</label>
                        </div>
                        <div class="selection">
                          <input id="baththreeplus2" name="bath" type="radio">
                          <label for="baththreeplus2">3+</label>
                        </div>
                        <div class="selection">
                          <input id="bathfourplus2" name="bath" type="radio">
                          <label for="bathfourplus2">4+</label>
                        </div>
                        <div class="selection">
                          <input id="bathfiveplus2" name="bath" type="radio">
                          <label for="bathfiveplus2">5+</label>
                        </div>
                      </div>
                    </div>
                    <div class="text-end mt10 pr10">
                      <button type="button" class="done-btn ud-btn btn-thm drop_btn4">Done</button>
                    </div>
                  </div>
                </li>
                <li class="list-inline-item">
                  <!-- Advance Features modal trigger -->
                  <button type="button" class="open-btn mb15" data-bs-toggle="modal" data-bs-target="#exampleModal"> <i class="flaticon-settings me-2"></i> More Filter</button>
                </li>
              </ul>                  
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- Property Half Map V4 -->
  <section class="p-0 bgc-f7">
    <div class="container-fluid">
      <div class="row wow fadeInUp" data-wow-delay="300ms">
        <div class="col-xl-7 overflow-hidden position-relative">
          <div class="half_map_area">
            <div class="map-canvas half_style" id="map" data-map-zoom="7" data-map-scroll="true"></div>
          </div>
        </div>
        <div class="col-xl-5">
          <div class="half_map_area_content mt30">
            <div class="text-center text-xl-start">
              <h4 class="mb-1">New York Homes for Sale</h4>                
            </div>
            <div class="row align-items-center mb10">
              <div class="col-xl-6">
                <div class="text-center text-xl-start">
                  <p class="pagination_page_count mb-0">Showing 1–10 of 13 results</p>
                </div>
              </div>
              <div class="col-xl-6">
                <div class="page_control_shorting d-flex align-items-center justify-content-center justify-content-xl-end">
                  <div class="pcs_dropdown pr10"><span>Sort by</span>
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
            <div class="row">
              <div class="col-md-6">
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
                    <h6 class="list-title"><a class="line-clamp" href="#">Equestrian Family <br> Home</a></h6>
                    <p class="list-text">California City, CA, USA</p>
                  </div>
                </div>
              </div>
              <div class="col-md-6">
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
                    <h6 class="list-title"><a class="line-clamp" href="#">Luxury villa in <br> Rego Park</a></h6>
                    <p class="list-text">California City, CA, USA</p>
                  </div>
                </div>
              </div>
              <div class="col-md-6">
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
                    <h6 class="list-title"><a class="line-clamp" href="#">Villa on Hollywood <br> Boulevard</a></h6>
                    <p class="list-text">California City, CA, USA</p>
                  </div>
                </div>
              </div>
              <div class="col-md-6">
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
                    <h6 class="list-title"><a class="line-clamp" href="#">Equestrian Family <br> Home</a></h6>
                    <p class="list-text">California City, CA, USA</p>
                  </div>
                </div>
              </div>
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
    </div>
  </section>
  <a class="scrollToHome" href="#"><i class="fas fa-angle-up"></i></a>
</div>

    </asp:Content>