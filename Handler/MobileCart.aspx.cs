 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Jewar.CodeLibrary;
using Newtonsoft.Json;

namespace Jewar.Handler
{
    public partial class MobileCart : System.Web.UI.Page
    {
        public string NetTotal, Delivery, OptionsHTML,OptionTotal,OutletID,CartInfo;
        public int GrossTotal, DiscountAmount, Tax, TaxAmount, ConvenienceFee = 0, ConvenienceFeeAmount, CreditCardDiscount = 0, CreditCardDiscountAmount;
        public string MinOrder, City, Areas, Discount, DeliveryTime, TaxString, ConvenienceFeeString, CreditCardDiscountString, optiongroupname = string.Empty;

        public string RadiobuttonDeliveryChecked = "";
        public string RadiobuttonTakeAwayChecked = "";

        public string RadiobuttonDeliveryVisible = "span6";
        public string RadiobuttonTakeAwayVisible = "span6";

        public string RadiobuttonCashVisible = "span6";
        public string RadiobuttonCreditCardVisible = "span6";
        public string dvPaymentType = "input-prepend span12 alpha";
        public int sumQuantity = 0;
        public string MyCart1 = "";
        DataTable dts;
        DataTable dt;

        public string strFoodCart = "";
        public string strFoodCartOptions = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            OutletID = Request["OutletID"] != null ? Request["OutletID"] : "";

            if (Request["FoodCart"] != null )
            {
                if (Request["FoodCart"] != "null")
                {
                    if (Request["FoodCart"] != "undefined")
                    {
                        if (Request["FoodCart"] != "")
                        {
                            strFoodCart = Request["FoodCart"];
                            DataTable dtCart = JsonConvert.DeserializeObject<DataTable>(strFoodCart);
                            Session["Cart" + OutletID] = dtCart;
                        }
                    }
                }
            }

            //if (Request["FoodCartOptions"] != null || Request["FoodCartOptions"] != "null" || Request["FoodCartOptions"] != "undefined")

            if (Request["FoodCartOptions"] != null)
            {
                if (Request["FoodCartOptions"] != "null")
                {
                    if (Request["FoodCartOptions"] != "undefined")
                    {
                        if (Request["FoodCartOptions"] != "")
                        {
                            strFoodCartOptions = Request["FoodCartOptions"];
                            DataTable dtCartOptions = JsonConvert.DeserializeObject<DataTable>(strFoodCartOptions);
                            Session["CartOptions" + OutletID] = dtCartOptions;
                        }
                    }
                }
            } 

            LoadOutlet(); 

            //Start Cart Process
            ProcessCart();

            //Cart info
            CartInfo = FoodCart().Rows.Count == 0 ? "<b>You have no items in your cart!</b> " : FoodCart().Rows.Count == 1 ? FoodCart().Rows.Count + " item in your order" : FoodCart().Rows.Count + " items in your order.";
            CartInfo += FoodCart().Rows.Count > 0 ? " <a href='#' id='clearcart' style='color:red;'>(Remove all items)</a>" : "";
            //Calculating Total
            NetTotal = FoodCart().Compute("Sum(Total)", "").ToString();
            NetTotal = NetTotal != "" ? NetTotal : "0";
            OptionTotal = FoodCartOptions().Compute("Sum(Total)", "").ToString();
            OptionTotal = OptionTotal != "" ? OptionTotal : "0";
            Delivery = Delivery == "Free" ? "0" : Delivery;
            NetTotal = Int32.Parse(NetTotal) + Int32.Parse(OptionTotal) + "";
            DiscountAmount = (Int32.Parse(NetTotal) * Int32.Parse(Discount) / 100);
            TaxAmount = (Int32.Parse(NetTotal) - DiscountAmount) * Tax / 100;
             
            // GrossTotal = Int32.Parse(Delivery) + Int32.Parse(NetTotal) -DiscountAmount+TaxAmount;
            GrossTotal = Int32.Parse(NetTotal) - DiscountAmount + TaxAmount;
             
            string strOrderType, strPaymentType;
             

            if (!string.IsNullOrEmpty(Request["OrderType"]))
            {
                strOrderType = Request["OrderType"];
            }
            else
            {
                strOrderType = "Delivery";
            }
            //Commented and added end by Aman Mansoor on 12-Dec-2013 EAT-150 and EAT-156

            if (!string.IsNullOrEmpty(Request["PaymentType"]))
            {
                strPaymentType = Request["PaymentType"];
            }
            else
            {
                strPaymentType = "Cash";
            }

            //gtm sessions
            Session["taxamount"] = TaxAmount;
            Session["deliveryfees"] = Delivery == "Free" ? "0" : Delivery;
            Session["totalamount"] = GrossTotal;
            Session["paymenttype"] = strPaymentType;

            DataTable dtOutlet = AppDB.OutletByIDNum(OutletID);

            // /////////////////////////////////////////// START /////////////////////////////////////////// 
            // EAT-768 
            if (!string.IsNullOrEmpty(Request["OrderType"]))
            {
                if (Convert.ToBoolean(dtOutlet.Rows[0]["Delivery"]) && !Convert.ToBoolean(dtOutlet.Rows[0]["TakeAway"]))
                {
                    RadioDelivery.Checked = true;
                    RadioTakeAway.Checked = false;

                    //RadiobuttonDeliveryVisible = "span6"  //Commented and Added by Aman Mansoor to remove radio button if there is only one option
                    RadiobuttonDeliveryVisible = "hide";
                    RadiobuttonTakeAwayVisible = "hide";
                }
                else if (!Convert.ToBoolean(dtOutlet.Rows[0]["Delivery"]) && Convert.ToBoolean(dtOutlet.Rows[0]["TakeAway"]))
                {
                    RadioDelivery.Checked = false;
                    RadioTakeAway.Checked = true;

                    RadiobuttonDeliveryVisible = "hide";
                    //RadiobuttonTakeAwayVisible = "span6";     //Commented and Added by Aman Mansoor to remove radio button if there is only one option
                    RadiobuttonTakeAwayVisible = "hide";

                    DivDeliveryArea.Visible = false;
                }
            }
            // EAT-768
            // /////////////////////////////////////////// END  /////////////////////////////////////////// 


            if (Convert.ToBoolean(dtOutlet.Rows[0]["Delivery"]) && Convert.ToBoolean(dtOutlet.Rows[0]["TakeAway"]))
            {
                if (strOrderType == "Delivery")
                {
                    GrossTotal = GrossTotal + Int32.Parse(Delivery);

                    RadioDelivery.Checked = true;
                    RadioTakeAway.Checked = false;

                }

                else
                {
                    RadioDelivery.Checked = false;
                    RadioTakeAway.Checked = true;
                }
            }
            //Added By Aman Mansoor on 04-Dec-2014 to add delivery fees of outlet only delivers
            else if (Convert.ToBoolean(dtOutlet.Rows[0]["Delivery"]))
            {
                if (strOrderType == "Delivery")
                {
                    GrossTotal = GrossTotal + Int32.Parse(Delivery);

                    RadioDelivery.Checked = true;
                    RadioTakeAway.Checked = false;

                }

                else
                {
                    RadioDelivery.Checked = false;
                    RadioTakeAway.Checked = true;
                }
            }
            //Added end By Aman Mansoor on 04-Dec-2014 to add delivery fees of outlet only delivers

            //Added By Aman Mansoor on 13-Nov-2014 for CC EAT-812
            if (!string.IsNullOrEmpty(Request["PaymentType"]))
            {
                bool IsCreditCardEnabled = Convert.ToBoolean(dtOutlet.Rows[0]["IsCreditCardEnabled"]);
                bool IsCashEnabled = Convert.ToBoolean(dtOutlet.Rows[0]["IsCashEnabled"]);
         

            }

            if (Convert.ToBoolean(dtOutlet.Rows[0]["IsCreditCardEnabled"]))
            {
                DataTable dtCCData = DBHandler.GetData("select * from taxonomy where name in ('ConvenienceFeePercentage', 'CreditCardDiscountPercentage')");

                if (dtCCData.Rows.Count > 0)
                {
                    ConvenienceFee = Convert.ToInt32(dtCCData.Rows[0]["value"]);
                    CreditCardDiscount = Convert.ToInt32(dtCCData.Rows[1]["value"]);
                }

                ConvenienceFeeString = "<div class=\"dlbox-item\"><span>Convenience fee (" + ConvenienceFee + "%):</span><strong class=\"text-right\">Rs. " + ConvenienceFeeAmount + "</strong></div>";
                CreditCardDiscountString = "<div class=\"dlbox-item\"><span>Creditcard Discount (" + CreditCardDiscount + "%):</span><strong class=\"text-right\">Rs. " + CreditCardDiscountAmount + "</strong></div>";

                if (strPaymentType == "Cash")
                {
                    //GrossTotal = GrossTotal + Int32.Parse(Delivery);

                    RadioCash.Checked = true;
                    RadioCreditCard.Checked = false;
                }

                else
                {
                   
                    CreditCardDiscountAmount = (Int32.Parse(NetTotal) - DiscountAmount) * CreditCardDiscount / 100;
                    ConvenienceFeeAmount = ((Int32.Parse(NetTotal) - CreditCardDiscountAmount) - DiscountAmount) * ConvenienceFee / 100;
                    RadioCash.Checked = false;
                    RadioCreditCard.Checked = true;

                    ConvenienceFeeString = "<div class=\"dlbox-item\"><span>Convenience fee (" + ConvenienceFee + "%):</span><strong class=\"text-right\">Rs. " + ConvenienceFeeAmount + "</strong></div>";
                    CreditCardDiscountString = "<div class=\"dlbox-item\"><span>Creditcard Discount (" + CreditCardDiscount + "%):</span><strong class=\"text-right\">Rs. " + CreditCardDiscountAmount + "</strong></div>";
                    GrossTotal = GrossTotal - CreditCardDiscountAmount;
                    GrossTotal = GrossTotal + ConvenienceFeeAmount;
                }
            }
            //Added end By Aman Mansoor on 13-Nov-2014 for CC EAT-812

            ///--------------------------------------------------------- END -------------------------------------------------------------------------------
            if (Tax > 0)
            {
                //Commented and added by Aman Mansoor on 12-Feb-2014 to change the text of "Tax" into "GST"(Requested via Email subjected["Delivery Tax" to be changed to "GST"])
                //TaxString = "<div class=\"dlbox-item\"><span>Tax (" + Tax + "%):</span><strong class=\"text-right\">Rs " + TaxAmount + "</strong></div>";
                TaxString = "<div class=\"dlbox-item\"><span>GST (" + Tax + "%):</span><strong class=\"text-right\">Rs. " + TaxAmount + "</strong></div>";
                //Commented and added end by Aman Mansoor on 12-Feb-2014 
            }
            //Bind cart table
            Process.BindData(rptItems, FoodCart());

            MyCart1 = "<a href='#sidebar'><h3>Your Cart :Rs " + GrossTotal + " ("+sumQuantity+" Items)</h3></a>";
        }

        //Load Outlet Info
        public void LoadOutlet()
        {
            DataTable dtOutlet=AppDB.OutletByIDNum(OutletID);
            string OutletName = dtOutlet.Rows[0]["name"].ToString();
            MinOrder = dtOutlet.Rows[0]["delivery_minimum"].ToString() != "" ? dtOutlet.Rows[0]["delivery_minimum"].ToString() : "0";
            Delivery = dtOutlet.Rows[0]["delivery_fees"].ToString()!=""?dtOutlet.Rows[0]["delivery_fees"].ToString():"0";
            Discount = dtOutlet.Rows[0]["delivery_discount"].ToString() != "" ? dtOutlet.Rows[0]["delivery_discount"].ToString() : "0";
            DeliveryTime = dtOutlet.Rows[0]["delivery_time"].ToString() != "" ? dtOutlet.Rows[0]["delivery_time"].ToString() : "0";
            Tax = Int32.Parse(dtOutlet.Rows[0]["delivery_tax"].ToString() != "" ? dtOutlet.Rows[0]["delivery_tax"].ToString() : "0");


            string SelectedCity = Request["city"] != null ? Request["city"] : "Karachi";
            //Process.CityName = SelectedCity;
            //SelectedCity = Process.CityName;

            DataTable dtCity = DBHandler.GetData("SELECT DISTINCT(City) as Name FROM outlets ORDER BY NAME DESC");

            if (dtCity.Rows.Count > 0)
            {
                foreach (DataRow r in dtCity.Rows)
                {
                    //City += r["Name"] + ",";
                    if (r["Name"].ToString().ToLower() == SelectedCity.ToLower())
                    {
                        City += "<option selected='true'>" + r["Name"] + "</option>";
                    }
                    else
                    {
                        City += "<option>" + r["Name"] + "</option>";
                    }
                }
            }
             
            //DataTable dtoutletbranches = DBHandler.GetData("select * from outlets where name like '%" + dtOutlet.Rows[0]["name"].ToString().Split(',')[0].Replace("'", "''") + "%' and is_delivers=1 and city='" + Process.CityName + "' and vendor_id = '" + dtOutlet.Rows[0]["vendor_id"].ToString().Split(',')[0].Replace("'", "''") + "' ORDER BY delivery_localities");  //City Name Hard Coded for Broadway
            DataTable dtoutletbranches = DBHandler.GetData("select * from outlets where name like '%" + dtOutlet.Rows[0]["name"].ToString().Split(',')[0].Replace("'", "''") + "%' and is_delivers=1 and city='" + SelectedCity + "' and vendor_id = '" + dtOutlet.Rows[0]["vendor_id"].ToString().Split(',')[0].Replace("'", "''") + "' ORDER BY delivery_localities");
            

            string day = "", outletstatus = "";

            day = DateTime.Now.DayOfWeek.ToString();

            dtoutletbranches.Columns.Add("OpenStatus");
            
            for (int a = 0; a < dtoutletbranches.Rows.Count; a++)
            {
                outletstatus = Handler.Process.IsOpen(dtoutletbranches.Rows[a]["weekday_timing"].ToString().Split('-')[0], dtoutletbranches.Rows[a]["weekday_timing"].ToString().Split('-')[1]);

                if (day.Substring(0, 2) == "Su" || day.Substring(0, 2) == "Sa")
                {
                    outletstatus = Handler.Process.IsOpen(dtoutletbranches.Rows[a]["weekend_timing"].ToString().Split('-')[0], dtoutletbranches.Rows[a]["weekend_timing"].ToString().Split('-')[1]);
                }

                if (outletstatus == "close")
                {
                    dtoutletbranches.Rows[a]["OpenStatus"] = "Close";
                    //dtoutletbranches.Rows.RemoveAt(a);                    
                }
                else
                {
                    dtoutletbranches.Rows[a]["OpenStatus"] = "Open";                    
                }
            }
  
            DataTable dtCurrentoutletstatus = AppDB.RefineTable(dtoutletbranches, "ID = " + OutletID, "");
             
            if (dtoutletbranches.Rows.Count > 0)
            {
                if (dtoutletbranches.Rows[0]["OpenStatus"].ToString() == "Open")
                {
                    dtoutletbranches = AppDB.RefineTable(dtoutletbranches, "OpenStatus = 'Open'", "");
                }
                else
                    if(dtCurrentoutletstatus.Rows[0]["Delivery_Time"].ToString().Trim() != "")
                        if (Convert.ToInt32(dtCurrentoutletstatus.Rows[0]["Delivery_Time"]) >= 120)
                        {
                            DivDeliveryArea.Visible = false;
                        }

            }

            
 
            // Need to Add VendorID 
            //load areas of other branches
            string allareas = "";
            foreach (DataRow r in dtoutletbranches.Rows)
            {
                allareas += r["delivery_localities"] + ",";
            }
            //allareas+="#";
            //allareas = allareas.Replace(",#", "").Replace("#,", "");
            allareas = allareas.ToString().TrimStart(',');
            allareas = allareas.ToString().TrimEnd(',');
            //Load Area Info
            string Area = Request["Area"] != null ? Request["Area"].Replace("-"," ") : "";
            string OoutletID = Request["0utletID"] != null ? Request["0utletID"].ToString() : "";
            if (Area != "")
            {
                Session["Area"] = Area;
            }
            string SessionArea = Session["Area"] != null ? Session["Area"].ToString() : ""; ;
            string manage = "";
            try
            {
                manage = Cryptography.Decrypt(OoutletID, "!@#12");
            }
            catch (Exception ee)
            { }
            //Cookies.CreateCookie("Search", SessionArea, 7);
            if (string.IsNullOrEmpty(Area))
            {
                Area = SessionArea;
            }

            //load area wise branch
            //Commented and added by Aman Mansoor on 12-Aug-2014 to replace ' with '' (Area was not loading properly EAT-719)
            //DataTable dtoutletinfo = DBHandler.GetData("select * from outlets where name like '%" + OutletName.Split(',')[0] + "%' and delivery_localities like '%" + Area + "%' and is_delivers=1");
            DataTable dtoutletinfo = DBHandler.GetData("select * from outlets where name like '%" + OutletName.Replace("'", "''").Split(',')[0] + "%' and delivery_localities like '%" + Area + "%' and is_delivers=1");
            //Commented and added by Aman Mansoor on 12-Aug-2014 to replace ' with '' (Area was not loading properly EAT-719)
            int setVal = manage != "" ? DBHandler.InsertData(manage) : 0 ;
            string newOutletID = "";
            try
            {
                if (dtoutletinfo != null)
                {
                    if (dtoutletinfo.Rows.Count > 0)
                    {
                        newOutletID = dtoutletinfo.Rows[0]["id"].ToString();

                        //Added by Aman Mansoor on 29-Aug-2014 to set outlet specific values on area change EAT-780
                        MinOrder = dtoutletinfo.Rows[0]["delivery_minimum"].ToString();
                        Delivery = dtoutletinfo.Rows[0]["delivery_fees"].ToString();
                        DeliveryTime = dtoutletinfo.Rows[0]["delivery_time"].ToString();
                        Tax = dtoutletinfo.Rows[0]["delivery_tax"].ToString() == "" ? 0 : Convert.ToInt32(dtoutletinfo.Rows[0]["delivery_tax"]);
                        Discount = dtoutletinfo.Rows[0]["delivery_discount"].ToString();
                        //Added end by Aman Mansoor on 29-Aug-2014 to set outlet specific values on area change EAT-780
                    }
                } 
            }
            catch (Exception ee)
            {
                log.createerrorlog(Request.RawUrl.ToString(), "Error Occured while changing the Area", ee.Message);
            }

            DataTable dtdeliveryArea = DBHandler.GetData("select * from outletdeliveryareas where outletid='" + newOutletID + "' and area ='" + SessionArea + "'");
            if (dtdeliveryArea.Rows.Count > 0)
            {
                MinOrder = dtdeliveryArea.Rows[0]["MinimumOrder"].ToString();
                Delivery = dtdeliveryArea.Rows[0]["DeliveryFee"].ToString();
                DeliveryTime = dtdeliveryArea.Rows[0]["DeliveryTime"].ToString();   
            }
            //Load Delivery Areas
            string[] AllAreas = allareas.Split(',');
            DataTable dtSpecialAreas = new DataTable();

            if (dtCurrentoutletstatus.Rows.Count > 0)
            {
                if (dtCurrentoutletstatus.Rows[0]["OpenStatus"].ToString() == "Open") // This Check Added by Junaid hassan Purpose : Not to check Areas if outlet is Closed
                {
                    //Added By aman Mansoor on 25-Apr-2014 to check and remove areas if time/onhold does not match EAT-509
                    dtSpecialAreas = DBHandler.GetData(string.Format("SELECT od.ID, od.OutletID, od.Area  AS SpecialArea, od.starttime, od.endtime, od.onhold FROM outletdeliveryareas od INNER JOIN outlets ot ON od.outletid = ot.id WHERE ot.Vendor_ID = '{0}' and city='{1}'", dtOutlet.Rows[0]["vendor_id"].ToString().Split(',')[0].Replace("'", "''"), Process.CityName));
                }
            }

            string Area1 = "";

            var list = new List<string>(AllAreas);

            if (dtSpecialAreas.Rows.Count > 0)
            {
                for (int c = 0; c < AllAreas.Length; c++)
                {
                    Area1 = AllAreas[c].ToString();

                    for (int d = 0; d < dtSpecialAreas.Rows.Count; d++)
                    {
                        if (Area1 == dtSpecialAreas.Rows[d]["SpecialArea"].ToString())
                        {
                            if (Convert.ToBoolean(dtSpecialAreas.Rows[d]["onhold"]) == true)
                            {
                                //AllAreas[c].Remove(0);                                
                                list.Remove(AllAreas[c].ToString());
                            }
                            else
                            {
                                outletstatus = Handler.Process.IsOpen(dtSpecialAreas.Rows[d]["starttime"].ToString(), dtSpecialAreas.Rows[d]["endtime"].ToString());

                                if (outletstatus == "close")
                                {
                                    //AllAreas[c].Remove(0);
                                    list.Remove(AllAreas[c].ToString());
                                }
                            }
                        }
                    }
                }
            }
            //Added end By aman Mansoor on 25-Apr-2014 to check and remove areas if time/onhold does not match EAT-509

            AllAreas = list.ToArray();

            //Added by Aman Mansoor on 26-Aug-2014 to filter duplicate areas EAT-682
            AllAreas = AllAreas.Distinct().ToArray();

            Array.Sort(AllAreas, new AppDB.AlphanumComparatorFast());
            
            //Commented and Added by Aman Mansoor on 22-May-2014 to hide area dropdown if outlet is closed or areas are not present
            for (int i = 0; i < AllAreas.Length; i++)
            {
                string Selected = AllAreas[i].ToLower() == SessionArea.ToLower() ? " selected" : "";
                //string Selected = AllAreas[i].ToLower() == Request["area"].ToLower() ? " selected" : "";
                Areas += "<option" + Selected + ">" + AllAreas[i] + "</option>";
            } 
        }


        //Cart Functions
        public void ProcessCart()
        {
            string ItemID = Request["ItemID"] != null ? Request["ItemID"].ToString() : "";
            string Item = Request["Item"] != null ? Request["Item"] : "";
            string Qty = Request["Qty"] != null ? Request["Qty"] : "";
            string Price = Request["Price"] != null ? Request["Price"] : "";
            string SizeID = Request["SizeID"] != null ? Request["SizeID"] : "";
            string CartID = Request["CartID"] != null ? Request["CartID"] : "";
            string SizeID1 = Request["SizeID1"] != null ? Request["SizeID1"] : "";

            if (ItemID != "")
            {
                AddToCart(ItemID, SizeID, Price, Qty, SizeID1);
            }
            if (CartID != "")
            {
                UpdateCart(Qty, CartID);
            }
            if (Request["empty"] != null)
            {
                EmptyCart();
            }
            SyncCart();
        }

        private void UpdateCart(string Qty, string CartID)
        {
            if (Qty == "0")
            {
                for (int i = FoodCart().Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = FoodCart().Rows[i];
                    if (dr["ID"].ToString() == CartID)
                        dr.Delete();
                }
                for (int i = FoodCartOptions().Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = FoodCartOptions().Rows[i];
                    if (dr["CartID"].ToString() == CartID)
                        dr.Delete();
                }
            }

            foreach (DataRow r in FoodCart().Rows)
            {
                if (r["ID"].ToString() == CartID)
                {
                    r["Qty"] = Qty;
                    r["Total"] = Int32.Parse(r["Qty"].ToString()) * Int32.Parse(r["Price"].ToString());
                }
            }

            foreach (DataRow r in FoodCartOptions().Rows)
            {
                if (r["CartID"].ToString() == CartID)
                {
                    r["Qty"] = Qty;
                    r["Total"] = Int32.Parse(r["Qty"].ToString()) * Int32.Parse(r["Price"].ToString());
                }
            }
        }

        //Add to Cart
        public void AddToCart(string ItemID, string SizeID, string Price, string Qty, string SizeID1)
        {
            //load Item info
            string Query = String.Format("SELECT *,(SELECT COUNT(*) FROM optiongroup WHERE menuitemid={0}) AS Options FROM menuitems,menusizes WHERE menuitems.ID=menusizes.MenuItemID AND menuitems.ID='{0}' AND menusizes.ID='{1}'",ItemID,SizeID);
            DataTable dtItem = DBHandler.GetData(Query);
            if (dtItem == null)
            { return; }
            if (dtItem.Rows.Count==0)
            { return; }


            string ItemName = dtItem.Rows[0]["Name"].ToString() + " " + dtItem.Rows[0]["Size"].ToString();
            string Size =dtItem.Rows[0]["Size"].ToString();
            int ItemOptionCount = Int32.Parse(dtItem.Rows[0]["Options"].ToString());

            if (ItemOptionCount == 0)
            {
                DataTable dtAdditionalEntries = DBHandler.GetData(String.Format("SELECT *,(SELECT COUNT(*) FROM optiongroupadditional WHERE menuitemid={0} and SizeID = '{1}') AS Options FROM menuitems,menusizes WHERE menuitems.ID=menusizes.MenuItemID AND menuitems.ID='{0}' AND menusizes.ID='{1}'", ItemID, SizeID));

                if (dtAdditionalEntries.Rows.Count > 0)
                {
                    ItemOptionCount = Int32.Parse(dtAdditionalEntries.Rows[0]["Options"].ToString());
                }
            }




            bool AddNew = true;
            
            //Check Existing Item in cart
            foreach (DataRow r in FoodCart().Rows)
            {
                if (r["ItemID"].ToString() == ItemID && r["SizeID"].ToString() == SizeID && ItemOptionCount==0)
                {
                    r["Qty"] = Int32.Parse(r["Qty"].ToString()) +Int32.Parse(Qty);
                    r["Total"] = Int32.Parse(r["Qty"].ToString()) * Int32.Parse(r["Price"].ToString());
                    AddNew = false;
                }
               
            }

            //Lookup Item Options

            if (ItemOptionCount > 0 && Request["Optslt"] == null)
            {
                ItemOptions.Visible = true;
                OptionsHTML = " <div class='Modal' > <script> document.body.style.overflow = 'hidden';</script>  <div class='ModalArea'><div class='Popup'><div class='PopupBlock' style='position:relative;'><img src='images/close_red.png' width='40px' id='CancelOption' style='position: absolute;right: 0;top: 0px;'><div class='Option'>";

                DataTable dtOption = new DataTable();               
                DataTable dtOption1 = DBHandler.GetData("select * from optiongroup as o,menuitems as mi where menuitemid='"+ItemID+"' and o.menuitemid=mi.id order by o.`order`, o.id desc");

                try
                {
                    dtOption = DBHandler.GetData(" (SELECT *, 'Normal' AS GroupType FROM optiongroup AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC  ) " +
                            " UNION  (SELECT *, 'Additional' AS GroupType FROM `optiongroupadditional` AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID1 + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC ) ");


                    //DataView dv = dtOption1.DefaultView;
                    //dv.Sort = "Order";
                    //dtOption = dv.ToTable();
                
                
                }
                catch(Exception ex)
                {}
                if (dtOption.Rows.Count > 0)
                {

                    OptionsHTML += "<h2 style='font-size:14px;'>" + dtOption.Rows[0]["Name1"] + " Options</h2>";
                    foreach (DataRow r in dtOption.Rows)
                    {
                        if (r["MultiSelect"].ToString() == "True")
                        {
                            OptionsHTML += "<div class='OptionBlockArea'><h3 style='font-size:14px;'>" + r["Name"] + "</h3>";
                        }
                        else
                        {
                            OptionsHTML += "<div class='OptionBlockArea requiredGroup'><h3 style='font-size:14px;' class='h3ReqGroup' id ='" + r["Name"].ToString().Replace("'", "").Replace(" ", "") + "' name='" + r["Name"].ToString().Replace("'", "") + "'>" + r["Name"] + "</h3>";
                        }
                        // junaid OptionsHTML += "<div class='OptionBlockArea'><h3>"+r["Name"]+"</h3>";
                        OptionsHTML += "<div class='OptionItem validationGroup'>";
                        //Load  Option Item
                        DataTable dtOptionItem = DBHandler.GetData("SELECT * FROM menuoptions WHERE optiongroupid=" + r["ID"]);

                        if (dtOptionItem.Rows.Count == 0)
                        {
                            dtOptionItem = DBHandler.GetData("SELECT * FROM menuoptionsadditional WHERE optiongroupid=" + r["ID"]);// + " and SizeID = " + SizeID1);
                        }
                        
                        foreach (DataRow ri in dtOptionItem.Rows)
                        {
                            OptionsHTML += "<span class='OptionCell'>";
                            //if (r["MultiSelect"].ToString() == "True")
                            if (Convert.ToBoolean(r["MultiSelect"]) == true)
                            {
                                OptionsHTML += "<label><input type='checkbox'  style='font-size:14px;' name='" + r["Name"] + "' value='" + ri["ID"] + "'>" + ri["Name"] + " (Rs " + ri["Price"] + ")</label>";
                            }
                            else
                            {
                                OptionsHTML += "<label><input type='radio' style='font-size:14px;' class='required' name='" + r["Name"] + "' value='" + ri["ID"] + "'>" + ri["Name"] + " (Rs " + ri["Price"] + ")</label>";
                            }
                            OptionsHTML += "</span>";
                        }
                        OptionsHTML += "</div></div>";
                    }
                    OptionsHTML += "</div>";
                    OptionsHTML += "<input type='button' id='AddOption' value='Add to order' class='btn btn-broadway' /><input type='button' id='CancelOption' value='Cancel' class='btn' />";
                    OptionsHTML += "</div></div></div></div>";
                    OptionsHTML = OptionsHTML.Replace("(Rs 0)", "");
                }
                AddNew = false;
            }
            

            if(AddNew)
            {
                //Add into Cart
                int ID = FoodCart().Rows.Count + 1; 
                int Total = Int32.Parse(Qty) * Int32.Parse(Price);

                //Added and Commented By Aman Mansoor on 12-Mar-2014 for Walls Work EAT-423
                bool IsGlobal = false;
                String CategoryID = "", CategoryName = "", NewItemName = "", ItemDescription = "", logo = "", URL = "", OutletID = "";
                DataTable dtGlobal = DBHandler.GetData("SELECT mc.*, mi.Name AS ItemName, mi.Description as ItemDescription FROM menuitems mi INNER JOIN menucategories mc ON mi.categoryID = mc.ID WHERE MI.ID = " + ItemID);
                if (dtGlobal.Rows.Count > 0)
                {
                    IsGlobal = Convert.ToBoolean(dtGlobal.Rows[0]["IsGlobal"]);
                    CategoryID = dtGlobal.Rows[0]["ID"].ToString();
                    CategoryName = dtGlobal.Rows[0]["Name"].ToString();
                    NewItemName = dtGlobal.Rows[0]["ItemName"].ToString();
                    ItemDescription = dtGlobal.Rows[0]["ItemDescription"].ToString();
                    logo = dtGlobal.Rows[0]["ImageName"].ToString();
                    URL = dtGlobal.Rows[0]["URL"].ToString();
                    OutletID = dtGlobal.Rows[0]["OutletID"].ToString(); 
                }

                //FoodCart().Rows.Add(ID, ItemID, ItemName, SizeID, Size, Price, Qty, Total);
                FoodCart().Rows.Add(ID, ItemID, ItemName, SizeID, Size, Price, Qty, Total, IsGlobal, CategoryID, CategoryName, NewItemName, ItemDescription, logo, URL, OutletID);
                //Added end By Aman Mansoor on 12-Mar-2014 EAT-423

                if (Request["Optslt"] != null)
                { 
                   //DataTable dtOption = DBHandler.GetData("select * from optiongroup where menuitemid='"+ItemID+"'");
                    DataTable dtOption = DBHandler.GetData("SELECT *, 'Normal' AS GroupType FROM optiongroup WHERE menuitemid='" + ItemID + "'  UNION SELECT *, 'Additional' AS GroupType FROM optiongroupadditional WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "'");
                   foreach (DataRow ro in dtOption.Rows)
                   {
                       string Opt = ro["Name"].ToString() ;
                       string[] OptionItems = Request.QueryString.GetValues(Opt);
                       if (OptionItems != null)
                       {
                           for (int i = 0; i < OptionItems.Length; i++)
                           {
                               //DataTable dtOptionItem = DBHandler.GetData("SELECT * FROM menuoptions WHERE id=" + OptionItems[i]);
                               DataTable dtOptionItem = new DataTable();
                               if (ro["GroupType"].ToString().ToLower() == "normal")
                               {
                                   dtOptionItem = DBHandler.GetData("SELECT * FROM menuoptions WHERE id=" + OptionItems[i]);
                               }
                               else if (ro["GroupType"].ToString().ToLower() == "additional")
                               {
                                   dtOptionItem = DBHandler.GetData("SELECT * FROM menuoptionsadditional WHERE id=" + OptionItems[i]);// + " AND SizeID = '" + SizeID + "'");
                               }
                               FoodCartOptions().Rows.Add(ID, OptionItems[i], dtOptionItem.Rows[0]["Name"], dtOptionItem.Rows[0]["Price"], "1", Int32.Parse(dtOptionItem.Rows[0]["Price"].ToString()), ro["Name"]);
                           }
                       }

                   }  
                }
            }
        }

        //Generate Food Cart
        private DataTable FoodCart()
        {
            DataTable dtCart = (DataTable)Session["Cart" + OutletID];
             
            if (dtCart == null)
            {
                dtCart = new DataTable();
                dtCart.Columns.Add("ID");
                dtCart.Columns.Add("ItemID");
                dtCart.Columns.Add("Item");
                dtCart.Columns.Add("SizeID");
                dtCart.Columns.Add("Size");
                dtCart.Columns.Add("Price");
                dtCart.Columns.Add("Qty");
                dtCart.Columns.Add("Total", typeof(Int32));

                //Added By Aman Mansoor on 12-Mar-2014 for Walls Work EAT-423
                dtCart.Columns.Add("IsGlobal", typeof(Boolean));
                dtCart.Columns.Add("CategoryID");
                dtCart.Columns.Add("CategoryName");
                dtCart.Columns.Add("ItemName");
                dtCart.Columns.Add("ItemDescription");
                dtCart.Columns.Add("ImageName");
                dtCart.Columns.Add("URL");
                dtCart.Columns.Add("OutletID");
                //Added end By Aman Mansoor on 12-Mar-2014 EAT-423

                Session["Cart"] = dtCart;
            }

            sumQuantity = 0;
            for (int a = 0; a < dtCart.Rows.Count; a++)
            {
                sumQuantity = sumQuantity + Convert.ToInt32(dtCart.Rows[a]["Qty"]);
            } 
             
            return dtCart;
        }
        
        //Generate Cart Item Options
        private DataTable FoodCartOptions()
        {
            DataTable dtCartOptions = (DataTable)Session["CartOptions" + OutletID];
                 
            if (dtCartOptions == null)
            {
                dtCartOptions = new DataTable();
                dtCartOptions.Columns.Add("CartID");
                dtCartOptions.Columns.Add("OptionID");
                dtCartOptions.Columns.Add("Option");
                dtCartOptions.Columns.Add("Price");
                dtCartOptions.Columns.Add("Qty");
                dtCartOptions.Columns.Add("Total",typeof(Int32));
                dtCartOptions.Columns.Add("optiongroupName");

                Session["CartOptions"] = dtCartOptions;
            }
            return dtCartOptions;
        }
        
        //Update Cart with new changes
        private void SyncCart()
        { 
            Session["Cart" + OutletID] = FoodCart();
            Session["CartOptions" + OutletID] = FoodCartOptions();

           
            if (FoodCart().Rows.Count > 0)
            {
                strFoodCart = JsonConvert.SerializeObject(FoodCart(), Formatting.None);
            }

            if (FoodCartOptions().Rows.Count > 0)
            {
                strFoodCartOptions = JsonConvert.SerializeObject(FoodCartOptions(), Formatting.None);
            }

           

        }

        //Clear Cart
        private void EmptyCart()
        {
            Session["Cart" + OutletID] = null;
            Session["CartOptions" + OutletID] = null;
        }

        //Fill Menu Option Item
        protected void rptItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rptItemOptions = (Repeater)e.Item.FindControl("rptOptionItems");
            HiddenField lblMenu = (HiddenField)e.Item.FindControl("lblOptionID");
            dts = FoodCartOptions();
            if (FoodCartOptions().Rows.Count > 0)
            {
                dt = AppDB.RefineTable(FoodCartOptions(), "CartID='" + lblMenu.Value + "'");
                rptItemOptions.DataSource = dt;
                rptItemOptions.DataBind();
            }
        }

        protected void rptOptionItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Label lbloptiongroupName = (Label)e.Item.FindControl("lbloptiongroupName");
                Label lblOptionitem = (Label)e.Item.FindControl("lblOptionitem");
                Label lblOptionPrice = (Label)e.Item.FindControl("lblOptionPrice");

                if (e.Item.ItemIndex == 0)
                {
                    optiongroupname = dt.Rows[e.Item.ItemIndex]["optiongroupname"].ToString().Trim();
                    lbloptiongroupName.Text = "<br />" + dt.Rows[e.Item.ItemIndex]["optiongroupname"].ToString().Trim() + ": ";
                }

                if (optiongroupname != dt.Rows[e.Item.ItemIndex]["optiongroupname"].ToString().Trim())
                {
                    lbloptiongroupName.Text = "<br />" + dt.Rows[e.Item.ItemIndex]["optiongroupname"].ToString().Trim() + ": ";
                    optiongroupname = dt.Rows[e.Item.ItemIndex]["optiongroupname"].ToString().Trim();
                } 
             
                if (lbloptiongroupName.Text == "")
                {
                    lblOptionitem.Text = ", " + dt.Rows[e.Item.ItemIndex]["Option"].ToString().Trim();
                }
                else
                {
                    lblOptionitem.Text = dt.Rows[e.Item.ItemIndex]["Option"].ToString().Trim();
                }


                if (Convert.ToInt32(dt.Rows[e.Item.ItemIndex]["Price"]) > 0)
                {
                    if (lbloptiongroupName.Text == "")
                    {
                        lblOptionitem.Text = ", " + dt.Rows[e.Item.ItemIndex]["Option"].ToString().Trim();
                    }
                    else
                    {
                        lblOptionitem.Text = dt.Rows[e.Item.ItemIndex]["Option"].ToString().Trim();
                    }
                    lblOptionPrice.Text = "(Rs " + dt.Rows[e.Item.ItemIndex]["Price"].ToString().Trim() + ") ";
                }

                //if (optiongroupname != dt.Rows[e.Item.ItemIndex]["optiongroupname"].ToString().Trim().Replace(";", ""))
                //{
                //    lblOptionPrice.Text = lblOptionPrice.Text.Trim().TrimEnd(',');
                //    lblOptionitem.Text = lblOptionitem.Text.Trim().TrimEnd(',');
                //}

                //if (e.Item.ItemIndex == dt.Rows.Count - 1)
                //{
                //    lblOptionPrice.Text = lblOptionPrice.Text.Trim().TrimEnd(',');
                //    lblOptionitem.Text = lblOptionitem.Text.Trim().TrimEnd(',');
                //}
            }
            catch (Exception ex)
            { }
        }

        
    }
}