<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Broadway_New.Handler.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Cart</title>
    <script>window.jQuery || document.write('<script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"><\/script>')</script>
    <script src="/assets/js/plugins.js"></script>
    <script src="/assets/js/app14.js"></script>

</head>



<body>
    <form id="form1" runat="server">
        <div class="CartArea">
            <div class="dlbox-item hide" id="DivDeliveryfee"><span>Delivery fee:</span><strong class="text-right">Rs. <%=Delivery%></strong></div>
            <input type="hidden" id="deliveryfeenew" value='<%=Delivery%>'></input>
            <div class="dlbox-item  hide"><span id="outletdiscount">Discount (<%=Discount%>%):</span><strong class="text-right">Rs. <%=DiscountAmount %></strong></div>
            <span class="inf hide" id="deliverytime"><%=DeliveryTime!="0" ? Jewar.Handler.Process.Duration(DeliveryTime):"1 hr" %></span>
            <span class="inf hide" id="Span2"><%=Convert.ToInt32(DeliveryTime) > 20 ? Jewar.Handler.Process.Duration((Convert.ToInt32(DeliveryTime) -20).ToString()) : Jewar.Handler.Process.Duration(DeliveryTime) %></span>
            <input type="hidden" id="outlettax" value="<%=Tax%>" />
            <input type="hidden" id="outletdiscountvalue" value="<%=Discount%>" />
            <input type="hidden" id="conveniencefeepercentage" value="<%=ConvenienceFee%>" />
            <input type="hidden" id="conveniencefee" value="<%=ConvenienceFeeAmount%>" />
            <input type="hidden" id="CreditCardDiscountPercentage" value="<%=CreditCardDiscount%>" />
            <input type="hidden" id="CreditCardDiscountAmount" value="<%=CreditCardDiscountAmount%>" />
            <input type="hidden" id="MinOrder" value="<%=MinOrder %>" />

           <%-- <div class="<%=RadiobuttonDeliveryVisible %>"><label><input id="RadioDelivery" runat="server" type="radio" name="OrderType" onclick="javascript: RadioOrderTypeClick();" value="Delivery" /><span style="margin-left:5px;line-height: 24px;">Delivery</span></label></div>
                    <div class="<%=RadiobuttonTakeAwayVisible %>"><label><input id="RadioTakeAway" runat="server" type="radio" name="OrderType" onclick="javascript: RadioOrderTypeClick();" value="TakeAway" /><span style="margin-left:5px;line-height: 24px;">Takeaway</span></label></div>
                    <input type="hidden" runat="server" id="hdnOrdertype" />--%>

          <%--  <li class="bg-grey"><a href="#location-select" class="modal-trigger" style="font-size: 20px;"><i class="material-icons">location_on</i> <span id="location222"></span> <small><u>Edit</u></a><small></li>--%>
            <li>
                <div>
                    <a href="#location-select" class="modal-trigger">
                        <div class="locaation-box-model">
                            <i class="material-icons">location_searching</i><span id="location222" class="location222333"><%= Session["Area"] != null ? Session["Area"].ToString() : "" %></span> <small>| <u>Edit</u></small>
                        </div>
                    </a>
                </div>
            </li>
            <asp:Repeater runat="server" ID="rptItems" OnItemDataBound="rptItems_ItemDataBound">
                <ItemTemplate>

                    <li>
                        <div class="cart_small_card panel">
                            <a href="#" class="cart_remove"><i class="material-icons">close</i></a>


                            <div class="title_cart"><%#Eval("Item") %></div>
                            <asp:HiddenField runat="server" ID="lblOptionID" Value='<%#Eval("ID")%>'></asp:HiddenField>



                            <asp:Repeater runat="server" ID="rptOptionItems" OnItemDataBound="rptOptionItems_ItemDataBound">
                                <ItemTemplate>
                                    <span class="sub">
                                        <strong>
                                            <asp:Label ID="lbloptiongroupName" runat="server"></asp:Label>
                                        </strong>
                                        <asp:Label ID="lblOptionitem" runat="server"></asp:Label>
                                        <asp:Label ID="lblOptionPrice" runat="server"></asp:Label>
                                    </span>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="row top20">
                                <div class="col s6">

                                    <div class="price_product">
                                        <%#Eval("Price")%> PKR
                                  <br>
                                        <small><%#Eval("Size")%></small>
                                    </div>
                                </div>
                                <div class="col s6">
                                    <div class="qtyAction">
                                        <input type="hidden" id="cartid" value='<%#Eval("ID")%>'></input>
                                        <input type='button' value='-' class='qtyminus qtybutton browser-default' />
                                        <input type='text' name='quantity' value='<%#Eval("Qty") %>' class='qty browser-default qty-box' readonly="readonly" />
                                        <input type='button' value='+' class='qtyplus qtybutton browser-default' />
                                    </div>
                                </div>
                            </div>

                        </div>

                    </li>
                </ItemTemplate>
            </asp:Repeater>
            <ul>
                <li>
                    <div class="divider"></div>
                </li>
                <li><span style="display: block;padding: 5px 15px;line-height: 1.3;color: #747474;" class="subheader">Subtotal: Rs <%=NetTotal%></span></li>
                <li><span style="display: block;padding: 5px 15px;line-height: 1.3;color: #747474;" class="subheader">GST (<%=Tax%>%): Rs <%=TaxAmount%></span></li>
                <li><span style="display: block;padding: 5px 15px;line-height: 1.3;color: #747474;"  class="subheader">Delivery fee: Rs <%=Delivery%></span></li>
                <li><span style="display: block;padding: 5px 15px;line-height: 1.3;color: #747474;" class="subheader" id="TotalAmount">Total: Rs <%=GrossTotal%></span></li>
            </ul>

           <%-- <ul>
                <li>
                    <div class="divider"></div>
                </li>
                <li class="bottom-fixed">
                <span class="tubtotals subheader ">Subtotal: <span>Rs <%=NetTotal%></span> </span> 
                 <span class="tubtotals subheader ">Delivery fee: <span>Rs <%=Delivery%></span> </span> 
                  <span class="subheader " id="TotalAmount">Total: <span>Rs <%=GrossTotal%></span></span>  
            </li>
                    </ul>--%>

           <%-- <ul>
                <li>
                    <div class="divider"></div>
                </li>

                <li class="bottom-fixed">
                    <span class="tubtotals">Subtotal: <span>Rs <%=NetTotal%></span></span>
                    <span class="tubtotals">Delivery fee: <span>Rs  <%=Delivery%></span></span>
                    <span class="tubtotals" id="TotalAmount">Total: <span>Rs <%=GrossTotal%></span></span>
                </li>
            </ul>--%>

            <input type="hidden" id="PageURL" value="<%=Request.Url.PathAndQuery%>" />
        </div>
    </form>
</body>

     

</html>
