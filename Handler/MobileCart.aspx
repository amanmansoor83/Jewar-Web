<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileCart.aspx.cs" Inherits="Jewar.Handler.MobileCart"  EnableViewState="false" %>

<head>
    <title>Cart</title>
    <script>window.jQuery || document.write('<script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"><\/script>')</script>
    <script src="/assets/js/plugins.js"></script>
    <script src="/assets/js/app14.js"></script>
    
</head>


<body>
      
    <form id="form1" runat="server">
        <div id="dvFoodCart"><%=strFoodCart%></div>
            <div id="dvFoodCartOptions" ><%=strFoodCartOptions%></div>

           
        <div class="CartArea" id="CastArea">

           
            <style>
                .sizes {
                    padding: 0;
                }

                .qty_btn {
                    float: left;
                    background-color: gray;
                    padding: 2px;
                    margin-right: 5px;
                    width: 20px;
                    line-height: 20px;
                    text-align: center;
                    font-weight: bold;
                    font-size: 16px;
                    color: white;
                }
            </style>
           
             <%--<script type="text/javascript">
                 $(document).ready(function () {
                     alert('Doc Ready');
                     RadioOrderTypeClick();
                     alert('test');
                 });
            </script>--%>
        
            <%-- ----------------------------------------------------------------------------------------------------------------------------------------------------------
                START
            Added BY        : Junaid Hassan
            Dated           : 2013-11-21
            Purpose         : This Function Moved from App.js to here as It was not firing from there on Outlet Page.
            Jira Task ID    : EAT-73--%>



            <script type="text/javascript">
                //// ///////////////////////////////////////////// START ///////////////////////////////////////////// 
                /// Added By        : Junaid Hassan
                /// Purpose         : call the RadioOrderTypeClick(); once on first time load so that it can set all the Client side properties properly.
                $(document).ready(function () {
                    runOnce();

                });

                function runOnce() {


                    //var Total123 = <%=GrossTotal%>;//parseInt($(this).find('#TotalAmount').html().replace('Rs.', ''))
                    //alert(Total123);
                    $('.dvCartFooter1').html('<a href="#sidebar"><h3>Your Cart :Rs <%=GrossTotal%> (<%=sumQuantity%> Items)</h3></a>');

                    if (!this.alreadyRan) {
                        // put all your functionality here
                        // alert('running my function!');


                        RadioOrderTypeClick();
                        RadioPaymentTypeClick();
                        // set a property on the function itself to prevent it being run again
                        this.alreadyRan = true;
                    }
                }
                //// ///////////////////////////////////////////// END ///////////////////////////////////////////// 

                /// Order type Radio Button 
                function RadioOrderTypeClick() {
                    //// Task Reference EAT-43
                    //// START : Save current state of Details DIV //////////////////////////////////////////////////////////////////////////
                    //// Added and Commented By Junaid Hassan.
                    //// Dated : 2013-11-27
                    //var CurrentState;
                    //if ($('.extrascont').attr('style') == 'display: block;') {
                    //    CurrentState = 'block';
                        
                    //}
                    //else {
                    //    CurrentState = 'none';
                    //}
                    //// END : Save current state of Details DIV //////////////////////////////////////////////////////////////////////////
                     
                    if ($("input[name='OrderType']:checked").val() == 'Delivery') {
                      
                        $('#hdnOrderType').val('Delivery');
                        $('#pickupblock').hide();
                        $('#deliveryblock').slideDown();

                        

                        var OutletID = $('#outletid').val();
                        var PaymentType = $("input[name='PaymentType']:checked").val();
                        //if (PaymentType == 'undefined')
                        //{
                        //    PaymentType = 'Cash';
                        //}

                        $('<div>').load('http://www.broadwaypizza.com.pk/Handler/MobileCart.aspx?OutletID=' + 5437 + "&OrderType=Delivery&PaymentType=Cash" + '#cart_box', function () {
                                                        
                            var htm = $(this).find('.CartArea').html();

                            $('#CartWidget').html(htm);


                            // $('#get').html(htm);

                            var Total = parseInt($(this).find('#TotalAmount').html().replace('Rs.', ''));
                            var DeliveryFee = parseInt($(this).find('#DivDeliveryfee').html().replace('</strong>', '').replace('<span>Delivery fee:</span><strong class="text-right">Rs.', ''));

                            if ((Total - DeliveryFee) >= $('#MinOrder').val() && Total > $('#deliveryfeenew').val()) {
                                $('#BtnOrder').removeAttr('disabled');
                                $('#minordernote').addClass('hide'); 
                            }
                            else {
                                $('#BtnOrder').attr('disabled', 'true');
                                $('#minordernote').removeClass('hide'); 
                            }
                            
                            $('#DivDeliveryArea').show();
                            $('#DivDeliveryfee').show();

                            $('#liDeliveryTime').show();
                            $('#liDeliveryFee').show();                            
                            $('#liMinOrder').show();
                            $('#li1').hide();
                            $('#li2').hide();
                            $('#li3').hide();

                            
                            //// Task Reference EAT-43
                            //// START : Save current state of Details DIV //////////////////////////////////////////////////////////////////////////
                            //// Added and Commented By Junaid Hassan.
                            //// Dated : 2013-11-27
                            //$('#DivExtrasCount').css("display", CurrentState);
                            //// END : Save current state of Details DIV //////////////////////////////////////////////////////////////////////////
                        });
                        

                    }
                    else {
                        
                        /// PICK UP case
                        $('#hdnOrderType').val('TakeAway');

                        $('#deliveryblock').slideUp();
                        
                        var OutletID = $('#outletid').val();
                        var PaymentType = $("input[name='PaymentType']:checked").val();

                        //if (PaymentType == 'undefined') {
                        //    PaymentType = 'Cash';
                        //}

                        $('<div>').load('http://www.broadwaypizza.com.pk/Handler/MobileCart.aspx?OutletID=' + 5437 + "&OrderType=TakeAway&PaymentType=Cash" + '#cart_box', function () {
                            var htm = $(this).find('.CartArea').html();
                                                        
                            $('#CartWidget').html(htm);

                            var Total = parseInt($(this).find('#TotalAmount').html().replace('Rs.', ''));

                            if (Total > 0) {
                                // alert('IF Total' + Total);

                                $('#BtnOrder').removeAttr('disabled');
                                $('#minordernote').addClass('hide'); 
                            }
                            else {
                                // alert('ELSE Total' + Total);

                                $('#BtnOrder').attr('disabled', 'true');
                                $('#minordernote').removeClass('hide'); 
                            }

                            $('#DivDeliveryArea').hide();
                            $('#DivDeliveryfee').hide();
                            $('#liDeliveryTime').hide();
                            $('#liDeliveryFee').hide();
                            $('#liMinOrder').hide();
                            $('#li1').show();
                            $('#li2').show();
                            $('#li3').show();
                            
                            //// Task Reference EAT-43
                            //// START : Save current state of Details DIV //////////////////////////////////////////////////////////////////////////
                            //// Added and Commented By Junaid Hassan.
                            //// Dated : 2013-11-27
                            //$('#DivExtrasCount').css("display", CurrentState);
                            //// END : Save current state of Details DIV //////////////////////////////////////////////////////////////////////////

                        });

                        
                        
                        $('#pickupblock').show();
                        $('#BtnOrder').removeAttr('disabled');
                        
                    }
                }


                /// Payment type Radio Button 
                function RadioPaymentTypeClick() {

                   // alert($("input[name='PaymentType']:checked").val());
                 
                    if ($("input[name='PaymentType']:checked").val() == 'Cash') {
                        
                        $('#hdnPaymentType').val('Cash');                       

                        var OutletID = $('#outletid').val();
                        var OrderType = $("input[name='OrderType']:checked").val();
                        $('<div>').load('http://www.broadwaypizza.com.pk/Handler/MobileCart.aspx?OutletID=' + 5437 + "&OrderType=" + OrderType + "&PaymentType=Cash" + '#cart_box', function () {

                            var htm = $(this).find('.CartArea').html();
                            $('#CartWidget').html(htm);

                            if (OrderType == 'Delivery') {
                                $('#liDeliveryTime').show();
                                $('#liDeliveryFee').show();
                                $('#liMinOrder').show();
                                $('#li1').hide();
                                $('#li2').hide();
                                $('#li3').hide();
                            }
                            else {
                                $('#liDeliveryTime').hide();
                                $('#liDeliveryFee').hide();
                                $('#liMinOrder').hide();
                                $('#li1').show();
                                $('#li2').show();
                                $('#li3').show();                                
                            }

                        });
                    }
                    else {                        
                        /// Credit Card Case
                        $('#hdnPaymentType').val('CreditCard');
                        
                        var OutletID = $('#outletid').val();
                        var OrderType = $("input[name='OrderType']:checked").val();
                        $('<div>').load('http://www.broadwaypizza.com.pk/Handler/MobileCart.aspx?OutletID=' + 5437 + "&OrderType=" + OrderType + "&PaymentType=CreditCard1" + '#cart_box', function () {
                            var htm = $(this).find('.CartArea').html();
                            $('#CartWidget').html(htm);

                            if (OrderType == 'Delivery') {
                                $('#liDeliveryTime').show();
                                $('#liDeliveryFee').show();
                                $('#liMinOrder').show();
                                $('#li1').hide();
                                $('#li2').hide();
                                $('#li3').hide();
                            }
                            else {
                                $('#liDeliveryTime').hide();
                                $('#liDeliveryFee').hide();
                                $('#liMinOrder').hide();
                                $('#li1').show();
                                $('#li2').show();
                                $('#li3').show();
                            }

                        });

                    }
                }


                // Added by Junaid Hassan
                // Dated 2013-11-27
                $("#slideToggle").click(function () {
                    $('.extrascont').slideToggle();
                    $('#slideToggle i').toggleClass('icon-angle-up');
                    $('#slideToggle i').toggleClass('icon-angle-down');
                });
                // -----------------------------------------------

    </script>

          
            

            <%--
                END
            ------------------------------------------------------------------------------------------------------------------------------------------------------------%>



                <div class="input-prepend span12 alpha hide" style="margin-bottom: 0;padding:10px 10px 0;">
                    
                    <div class="<%=RadiobuttonDeliveryVisible %>" style="display: none;"><label><input id="RadioDelivery" runat="server" type="radio" name="OrderType" onclick="javascript:RadioOrderTypeClick();" value="Delivery" /><span style="margin-left:5px;line-height: 24px;">Delivery</span></label></div>
                    <div class="<%=RadiobuttonTakeAwayVisible %>" style="display: none;"><label><input id="RadioTakeAway" runat="server" type="radio" name="OrderType" onclick="javascript:RadioOrderTypeClick();" value="TakeAway" /><span style="margin-left:5px;line-height: 24px;">Takeaway</span></label></div>
                    <input type="hidden" runat="server" id="hdnOrdertype" />
                </div>

                <div class="widget-info">
                    <%--<%Response.Write(Request.UrlReferrer); %>--%>
                    <ul class="inline clearfix">
                         <li>
                            <div class="input-prepend"  runat="server"  id="Div1">
                                
                                <select class="span5 checkselect" id="City" style="width: 100%; padding: 5px;">
                                    <option value="">Select Your City</option>
                                    <%=City %>
                                </select>
                            </div>
                        </li>
                        <li>
                            <div class="input-prepend"  runat="server"  id="DivDeliveryArea">
                                
                                <select class="span5 checkselect" id="DeliveryArea" style="width: 100%; padding: 5px;">
                                    <option value="">Select Your Area</option>
                                    <%=Areas %>
                                </select>
                            </div>
                        </li>
                       <%-- <li id="liDeliveryTime" <%if (Jewar.CodeLibrary.Cookies.GetCookie("customerName").ToString() == "")
                                                  { %> class="hidden" <%} %>>
                            <span class="icon-time pull-left" title="Delivery Time" aria-hidden="true"></span>
                            <span class="inf" id="deliverytime"><%=DeliveryTime!="0" ? Jewar.Handler.Process.Duration(DeliveryTime):"1 hr" %></span>
                        </li>
                        <li id="liDeliveryFee" <%if (Jewar.CodeLibrary.Cookies.GetCookie("customerName").ToString() == "")
                                                 { %> class="hidden" <%} %>>
                            <span class="icon-delivery pull-left" title="Delivery Fee" aria-hidden="true"></span>
                            <span class="inf"><%=Delivery!="0" ? "Rs. " + Delivery: "Free" %></span>
                            <input type="hidden" id="hdnDeliveryFee" value="<%=Delivery %>" />
                        </li>--%>
                        <li id="liMinOrder" >
                            <span class="icon-wallet pull-left" title="Minimum Order" aria-hidden="true"></span>
                            <span class="inf" style="font-weight:bold;"><%=MinOrder!="0"? "Minimum Order Rs. " + MinOrder : "Rs. 0" %></span>
                            <input type="hidden" id="MinOrder" value="<%=MinOrder %>" />
                        </li>
                       <%-- <li id="li1">
                            <span class="icon-time pull-left" title="Delivery Time" aria-hidden="true"></span>
                              
                              <span class="inf" id="Span2"><%=Convert.ToInt32(DeliveryTime) > 20 ? Jewar.Handler.Process.Duration((Convert.ToInt32(DeliveryTime) -20).ToString()) : Jewar.Handler.Process.Duration(DeliveryTime) %></span>
                        </li> 
                        <li id="li2">
                            <span class="icon-delivery pull-left" title="Delivery Fee" aria-hidden="true"></span>
                            <span class="inf">Free</span>
                             
                        </li>
                        <li id="li3">
                            <span class="icon-wallet pull-left" title="Minimum Order" aria-hidden="true"></span>
                            <span class="inf">Rs. 0</span>
                           
                        </li>--%>
                         
                    </ul>
                </div>
                <div class="cart-items">
                    <p class="note"><%=CartInfo %></p>
                    <asp:Repeater runat="server" ID="rptItems" OnItemDataBound="rptItems_ItemDataBound">
                        <Itemtemplate>
                            <table class="table table_summary">
                                <tbody>
                                    <tr>

                                        <td><a href="#" class="add qty_btn">+</a>
                                            <a href="#" class="minus qty_btn">-</a>
                                            <a href="#0" class="remove_item"><i class="icon_minus_alt"></i></a>
                                            <input readonly="true" class="qty-box hide" maxlength="3" type="text" style="width: 15px;" value="<%#Eval("Qty") %>">
                                            <strong><%#Eval("Qty") %></strong><%#Eval("Item") %>
                                            <asp:HiddenField runat="server" ID="lblOptionID" Value='<%#Eval("ID")%>'></asp:HiddenField>
                                            <input type="hidden" id="cartid" value='<%#Eval("ID")%>'></input>
                                            <p>
                                                <asp:Repeater runat="server" ID="rptOptionItems" OnItemDataBound="rptOptionItems_ItemDataBound">
                                                    <Itemtemplate>
                                                        <%--<strong><%#Eval("Option") %></strong>(Rs. <%#Eval("Total") %>)--%>
                                                        <strong>
                                                            <asp:Label ID="lbloptiongroupName" runat="server"></asp:Label></strong>
                                                        <asp:Label ID="lblOptionitem" runat="server"></asp:Label>
                                                        <asp:Label ID="lblOptionPrice" runat="server"></asp:Label>
                                                    </Itemtemplate>
                                                </asp:Repeater>
                                            </p>
                                        </td>
                                        <td>
                                             <div class="cart-item-price"><strong><%#Eval("Total") %></strong></div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table> 
                        </Itemtemplate>
                    </asp:Repeater>

                </div>
                <script> 
                    $(".cart-items").animate({ scrollTop: $(document).height() }, "slow");
                </script>
            <div class="delivery-box cart-total">
                <input type="hidden" id="outlettotal" value="<%=NetTotal%>" />
                <input type="hidden" id="taxamt" value="<%=TaxAmount%>" />
                <input type="hidden" id="outletdelivery" value="<%=Delivery%>" />

                <div class="hide extrascont" id="DivExtrasCount">
                    <div class="dlbox-item"><span>Sub-total:</span><strong class="text-right">Rs. <%=NetTotal%></strong></div>
                    <div class="dlbox-item"><span id="outletdiscount">Discount (<%=Discount%>%):</span><strong class="text-right">Rs. <%=DiscountAmount %></strong></div>
                    <%=TaxString%>
                    <input type="hidden" id="outlettax" value="<%=Tax%>" />
                    <input type="hidden" id="outletdiscountvalue" value="<%=Discount%>" />
                    <div class="dlbox-item" id="DivDeliveryfee"><span>Delivery fee:</span><strong class="text-right">Rs. <%=Delivery%></strong></div>
                    <input type="hidden" id="deliveryfeenew" value='<%=Delivery%>'></input>

                    <%--<div class="dlbox-item"><span>Convince fee(5%):</span><strong class="text-right">Rs. <%=ConvinceFee%></strong></div>      --%>

                    <%=CreditCardDiscountString%>
                    <input type="hidden" id="CreditCardDiscountPercentage" value="<%=CreditCardDiscount%>" />
                    <input type="hidden" id="CreditCardDiscountAmount" value="<%=CreditCardDiscountAmount%>" />


                    <%=ConvenienceFeeString%>
                    <input type="hidden" id="conveniencefeepercentage" value="<%=ConvenienceFee%>" />
                    <input type="hidden" id="conveniencefee" value="<%=ConvenienceFeeAmount%>" />
                </div>
                <div class="payment-row" runat="server" visible="false">
                    <%--<div class="input-prepend span12 alpha"  style="margin-bottom: 0px; padding: 10px 10px 0px;">--%>
                    <div class="<%=dvPaymentType %>" style="margin-bottom: 0px; padding: 10px 10px 0px;">
                        <div class="<%=RadiobuttonCashVisible %>">
                            <label>
                                <input type="radio" value="Cash" name="PaymentType" runat="server" onclick="javascript: RadioPaymentTypeClick();" id="RadioCash"><span style="margin-left: 5px; line-height: 24px;">Pay by Cash</span></label></div>
                        <div class="<%=RadiobuttonCreditCardVisible %>">
                            <label>
                                <input type="radio" value="CreditCard" name="PaymentType" runat="server" onclick="javascript: RadioPaymentTypeClick();" id="RadioCreditCard"><span style="margin-left: 5px; line-height: 24px;">Pay by Card</span></label></div>
                        <input type="hidden" runat="server" id="hdnPaymentType" />
                    </div>
                </div>
               <%-- <div class="total">
                    <span> 
                        <a href="javascript:void(0)" class="ShowDetails cart-show-details-link" id="slideToggle">Bill Details <i class="icon-angle-down"></i></a>

                        Total:</span><strong id="TotalAmount" class="pull-right">Rs. <%=GrossTotal%></strong>
                </div>--%>
                <hr>
					<table class="table table_summary">
					<tbody>
					<tr>
						<td>
							 Subtotal <span class="pull-right">Rs. <%=NetTotal%></span>
						</td>
					</tr>
					<tr>
						<td>
							 Delivery fee <span class="pull-right">Rs. <%=Delivery%></span>
						</td>
					</tr>
					<tr>
						<td class="total">
						<strong  >TOTAL</strong> <span class="pull-right" id="TotalAmount">Rs. <%=GrossTotal%></span>
						</td>
					</tr>
					</tbody>
					</table>


            </div>
            
        </div>

        <div class="ItemOptions" id="ItemOptions" runat="server">

            <%=OptionsHTML %>

            <input type="hidden" id="PageURL" value="<%=Request.Url.PathAndQuery%>" />
        </div>

       <div class="dvCartFooter1"><%=MyCart1%></div> 
        
    </form>
</body>
