using Jewar.CodeLibrary;
using Jewar.Handler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Broadway_New.Handler
{
    public partial class Cart : System.Web.UI.Page
    {
        public string City = ""; public string OutletID = "5437"; public string Areas = "";
        public DataTable dtOutlet;
        public DataTable dtCategory;
        public string NetTotal, Delivery, OptionsHTML, OptionTotal,  CartInfo;
        public int GrossTotal, DiscountAmount, Tax, TaxAmount, ConvenienceFee = 0, ConvenienceFeeAmount, CreditCardDiscount = 0, CreditCardDiscountAmount;
        public string MinOrder, Discount, DeliveryTime, TaxString, ConvenienceFeeString, CreditCardDiscountString, optiongroupname = string.Empty;
        public int sumQuantity = 0;
        public string RadiobuttonDeliveryVisible = "span6";
        public string RadiobuttonTakeAwayVisible = "span6";
        DataTable dts;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!checkForSQLInjection(Request["OutletID"]))
            {
                OutletID = Request["OutletID"] != null ? Request["OutletID"] : "5437";
            }
            LoadOutlet();

            //Start Cart Process
            ProcessCart();


            //Cart info
            CartInfo = FoodCart().Rows.Count == 0 ? "<b>You have no items in your cart!</b> " : FoodCart().Rows.Count == 1 ? FoodCart().Rows.Count + " item in your order" : FoodCart().Rows.Count + " items in your order.";
            CartInfo += FoodCart().Rows.Count > 0 ? " <a href='#' id='clearcart'>(Remove all items)</a>" : "";
            //Calculating Total
            NetTotal = FoodCart().Compute("Sum(Total)", "").ToString();
            NetTotal = NetTotal != "" ? NetTotal : "0";
            OptionTotal = FoodCartOptions().Compute("Sum(Total)", "").ToString();
            OptionTotal = OptionTotal != "" ? OptionTotal : "0";
            Delivery = Delivery == "Free" ? "0" : Delivery;
            NetTotal = Int32.Parse(NetTotal) + Int32.Parse(OptionTotal) + "";
            DiscountAmount = (Int32.Parse(NetTotal) * Int32.Parse(Discount) / 100);
            TaxAmount = (Int32.Parse(NetTotal) - DiscountAmount) * Tax / 100;
            NetTotal = (Int32.Parse(NetTotal) - Int32.Parse(TaxAmount.ToString())).ToString();
            GrossTotal = Int32.Parse(NetTotal) - DiscountAmount + TaxAmount;


            Process.BindData(rptItems, FoodCart());


          

        }

        //Load Outlet Info
        public void LoadOutlet()
        {
            DataTable dtOutlet = AppDB.OutletByIDNum(OutletID);
            string OutletName = dtOutlet.Rows[0]["name"].ToString();
            MinOrder = dtOutlet.Rows[0]["delivery_minimum"].ToString() != "" ? dtOutlet.Rows[0]["delivery_minimum"].ToString() : "0";
            Delivery = dtOutlet.Rows[0]["delivery_fees"].ToString() != "" ? dtOutlet.Rows[0]["delivery_fees"].ToString() : "0";
            Discount = dtOutlet.Rows[0]["delivery_discount"].ToString() != "" ? dtOutlet.Rows[0]["delivery_discount"].ToString() : "0";
            DeliveryTime = dtOutlet.Rows[0]["delivery_time"].ToString() != "" ? dtOutlet.Rows[0]["delivery_time"].ToString() : "0";
            Tax = Int32.Parse(dtOutlet.Rows[0]["delivery_tax"].ToString() != "" ? dtOutlet.Rows[0]["delivery_tax"].ToString() : "0");

            string SelectedCity = "";
            if (!checkForSQLInjection(Request["city"]))
            {
                SelectedCity = Request["city"] != null ? Request["city"] : "Karachi";
            }
                  //Process.CityName = SelectedCity;
            SelectedCity = Process.CityName;

            DataTable dtCity = DBHandler.GetData("SELECT DISTINCT(City) as Name FROM outlets ORDER BY NAME DESC");

            if (SelectedCity != "")
            {
                Session["City"] = SelectedCity; 
            }


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

            //Tax = Int32.Parse(dtoutletbranches.Rows[0]["delivery_tax"].ToString() != "" ? dtoutletbranches.Rows[0]["delivery_tax"].ToString() : "0");



            if (Request["OrderType"] != null)
            {
                if (!checkForSQLInjection(Request["OrderType"]))
                {
                    if (Request["OrderType"].ToString().ToLower() == "takeaway")
                    {
                        dtoutletbranches = DBHandler.GetData("select * from outlets where name like '%" + dtOutlet.Rows[0]["name"].ToString().Split(',')[0].Replace("'", "''") + "%' and takeaway=1 and city='" + SelectedCity + "' and vendor_id = '" + dtOutlet.Rows[0]["vendor_id"].ToString().Split(',')[0].Replace("'", "''") + "' ORDER BY delivery_localities");
                    }
                }
            }


            string day = "", outletstatus = "";

            day = DateTime.Now.DayOfWeek.ToString();

            dtoutletbranches.Columns.Add("OpenStatus");

            for (int a = 0; a < dtoutletbranches.Rows.Count; a++)
            {
                outletstatus = Process.IsOpen(dtoutletbranches.Rows[a]["weekday_timing"].ToString().Split('-')[0], dtoutletbranches.Rows[a]["weekday_timing"].ToString().Split('-')[1]);

                if (day.Substring(0, 2) == "Su" || day.Substring(0, 2) == "Sa")
                {
                    outletstatus = Jewar.Handler.Process.IsOpen(dtoutletbranches.Rows[a]["weekend_timing"].ToString().Split('-')[0], dtoutletbranches.Rows[a]["weekend_timing"].ToString().Split('-')[1]);
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

            //Added by Aman Mansoor on 22-May-2014 to hide area dropdown if outlet is closed

            /// ////////////////////////////////////////////START /////////////////////////////////////////////
            /// Commented By        : Junaid Hassan
            /// Purpose             : Add pre-Order Functionality Need to show Area DropDown wheather Branches/Outlet open or close.
            /// TaskID              : EAT-586

            DataTable dtCurrentoutletstatus = AppDB.RefineTable(dtoutletbranches, "ID = " + OutletID, "");

            //if (dtCurrentoutletstatus.Rows.Count > 0)
            //{
            //    if (dtCurrentoutletstatus.Rows[0]["OpenStatus"].ToString() == "Open")
            //    {
            //        DivDeliveryArea.Visible = true;
            //    }
            //    else
            //    {
            //        DivDeliveryArea.Visible = false;
            //    }
            //}

            /// TaskID              : EAT-586
            /// Commented By        : Junaid Hassan
            /// //////////////////////////////////////////// END /////////////////////////////////////////////

            /// 
            //Added end by Aman Mansoor on 22-May-2014 to hide area dropdown if outlet is closed

            // Commnted By Junaid Hassan Under  | TaskID : EAT-586 
            // dtoutletbranches = AppDB.RefineTable(dtoutletbranches, "OpenStatus = 'Open'", "");

            if (dtCurrentoutletstatus.Rows.Count > 0)
            {
                if (dtCurrentoutletstatus.Rows[0]["OpenStatus"].ToString() == "Open")
                {
                    dtoutletbranches = AppDB.RefineTable(dtoutletbranches, "OpenStatus = 'Open'", "");
                }
                else
                    if (dtCurrentoutletstatus.Rows[0]["Delivery_Time"].ToString().Trim() != "")
                        if (Convert.ToInt32(dtCurrentoutletstatus.Rows[0]["Delivery_Time"]) >= 120)
                        {
                            //DivDeliveryArea.Visible = false;
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

            string Area = "";
            if (!checkForSQLInjection(Request["Area"]))
            {
                Area = Request["Area"] != null ? Request["Area"].Replace("-", " ") : "";
            }
            
             string OoutletID = "";
             if (!checkForSQLInjection(Request["0utletID"]))
             {
                 OoutletID = Request["0utletID"] != null ? Request["0utletID"].ToString() : "";
             }
                
                if (Area != "")
            {
                Session["Area"] = Area;
                //Cookies.CreateCookie("myarea1", Area, 30);

                //Jewar.CodeLibrary.Cookies.CreateCookie("myarea1", Area, 30);
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

            if (Request["OrderType"] != null)
            {
                if (!checkForSQLInjection(Request["OrderType"]))
                {
                    if (Request["OrderType"].ToString().ToLower() == "takeaway")
                    {
                        dtoutletinfo = DBHandler.GetData("select * from outlets where name like '%" + dtOutlet.Rows[0]["name"].ToString().Split(',')[0].Replace("'", "''") + "%'  and city='" + SelectedCity + "' ORDER BY delivery_localities");
                    }
                }
            }
            
            int setVal = manage != "" ? DBHandler.InsertData(manage) : 0;
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
                    //dtSpecialAreas = DBHandler.GetData(string.Format("SELECT od.ID, od.OutletID,  CASE WHEN od.onhold = 1 THEN CONCAT(od.Area, ' (Onhold)') ELSE od.area END  AS SpecialArea, od.starttime, od.endtime, od.onhold FROM outletdeliveryareas od INNER JOIN outlets ot ON od.outletid = ot.id WHERE ot.Vendor_ID = '{0}' and city='{1}'", dtOutlet.Rows[0]["vendor_id"].ToString().Split(',')[0].Replace("'", "''"), Process.CityName));
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
                                //list.Remove(AllAreas[c].ToString());
                                list[c] = AllAreas[c].ToString() + "(Onhold)";
                            }
                            else
                            {
                                outletstatus = Process.IsOpen(dtSpecialAreas.Rows[d]["starttime"].ToString(), dtSpecialAreas.Rows[d]["endtime"].ToString());

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
            //Added end by Aman Mansoor on 26-Aug-2014 to filter duplicate areas EAT-682

            /// Added By            : Junaid hassan
            /// dated               : 2014-0421
            /// Purpose             : Apply Natural sort on loaded areas 
            /// TaskID              : EAT-513
            Array.Sort(AllAreas, new AppDB.AlphanumComparatorFast());

            //Commented and Added by Aman Mansoor on 22-May-2014 to hide area dropdown if outlet is closed or areas are not present
            for (int i = 0; i < AllAreas.Length; i++)
            {
                string Selected = AllAreas[i].ToLower() == SessionArea.ToLower() ? " selected" : "";
                Areas += "<option" + Selected + ">" + AllAreas[i] + "</option>";
            }

            if (Request["OrderType"] != null)
            {
                if (!checkForSQLInjection(Request["OrderType"]))
                {
                    if (Request["OrderType"].ToString().ToLower() == "takeaway")
                    {
                        Areas = "<option value=''>Select Your Branch</option>";
                        for (int i = 0; i < dtoutletbranches.Rows.Count; i++)
                        {
                            //City += r["Name"] + ",";
                            if (dtoutletbranches.Rows[i]["name"].ToString().ToLower() == Area.ToLower())
                            {
                                Areas += "<option selected='true'>" + dtoutletbranches.Rows[i]["name"].ToString() + "</option>";
                            }
                            else
                            {
                                Areas += "<option>" + dtoutletbranches.Rows[i]["name"].ToString() + "</option>";
                            }

                        }

                        Delivery = "0";
                    }
                }
            }


            //if (AllAreas.Length > 1)
            //{
            //    for (int i = 0; i < AllAreas.Length; i++)
            //    {
            //        string Selected = AllAreas[i].ToLower() == SessionArea.ToLower() ? " selected" : "";
            //        Areas += "<option" + Selected + ">" + AllAreas[i] + "</option>";
            //    }
            //}
            //else
            //{
            //    DivDeliveryArea.Visible = false;
            //}
            //Commented and Added end by Aman Mansoor on 22-May-2014 to hide area dropdown if outlet is closed or areas are not present


            //DataTable dtdeliveryArea = DBHandler.GetData("select * from outletdeliveryareas where outletid='" + newOutletID + "' and area ='" + SessionArea + "'");
            //if (dtdeliveryArea.Rows.Count > 0)
            //{
            //    MinOrder = dtdeliveryArea.Rows[0]["MinimumOrder"].ToString();
            //    Delivery = dtdeliveryArea.Rows[0]["DeliveryFee"].ToString();
            //    DeliveryTime = dtdeliveryArea.Rows[0]["DeliveryTime"].ToString();

            //    //Added discount section by Aman Mansoor on 09-Dec-2013 to add delivery discount from area as well EAT-120
            //    Discount = dtdeliveryArea.Rows[0]["Discount"].ToString() != "" ? dtdeliveryArea.Rows[0]["Discount"].ToString() : "0";
            //    //Added discount section end by Aman Mansoor on 09-Dec-2013 
            //}
        }


        //Cart Functions
        public void ProcessCart()
        {
            //string ItemID = Request["ItemID"] != null ? Request["ItemID"].ToString() : "";
            //string Item = Request["Item"] != null ? Request["Item"] : "";
            //string Qty = Request["Qty"] != null ? Request["Qty"] : "";
            //string Price = Request["Price"] != null ? Request["Price"] : "";
            //string OriginalPrice = Request["OriginalPrice"] != null ? Request["OriginalPrice"] : "";
            //string SizeID = Request["SizeID"] != null ? Request["SizeID"] : "";
            //string CartID = Request["CartID"] != null ? Request["CartID"] : "";
            //string SizeID1 = Request["SizeID1"] != null ? Request["SizeID1"] : "";

            string ItemID = "";
            if (!checkForSQLInjection(Request["ItemID"]))
            {
                ItemID = Request["ItemID"] != null ? Request["ItemID"].ToString() : "";
            }
         
            string Item = "";
            if (!checkForSQLInjection(Request["Item"]))
            {
                Item = Request["Item"] != null ? Request["Item"] : "";
            }

            string Qty = "";
            if (!checkForSQLInjection(Request["Qty"]))
            {
                Qty = Request["Qty"] != null ? Request["Qty"] : "";
            }

            string Price = "";
            if (!checkForSQLInjection(Request["Price"]))
            {
                Price = Request["Price"] != null ? Request["Price"] : "";
            }

            string OriginalPrice = "";
            if (!checkForSQLInjection(Request["OriginalPrice"]))
            {
                OriginalPrice = Request["OriginalPrice"] != null ? Request["OriginalPrice"] : "";
            }

            string SizeID = "";
            if (!checkForSQLInjection(Request["SizeID"]))
            {
                SizeID = Request["SizeID"] != null ? Request["SizeID"] : "";
            }

            string CartID = "";
            if (!checkForSQLInjection(Request["CartID"]))
            {
                CartID = Request["CartID"] != null ? Request["CartID"] : "";
            }

            string SizeID1 = "";
            if (!checkForSQLInjection(Request["SizeID1"]))
            {
                SizeID1 = Request["SizeID1"] != null ? Request["SizeID1"] : "";
            }

            if (ItemID != "")
            {
                AddToCart(ItemID, SizeID, Price, OriginalPrice, Qty, SizeID1);
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
        public void AddToCart(string ItemID, string SizeID, string Price, string OriginalPrice, string Qty, string SizeID1)
        {

            DataTable dtSize = DBHandler.GetData(string.Format(@"select HalfnHalf from menusizes where id = '{0}'", SizeID));
            bool HalfnHalf = false;

            if (dtSize.Rows.Count > 0)
            {
                HalfnHalf = Convert.ToBoolean(dtSize.Rows[0]["HalfnHalf"]);
            }     

            //load Item info
            string Query = String.Format("SELECT *,(SELECT COUNT(*) FROM optiongroup WHERE menuitemid={0}) AS Options FROM menuitems,menusizes WHERE menuitems.ID=menusizes.MenuItemID AND menuitems.ID='{0}' AND menusizes.ID='{1}'", ItemID, SizeID);
            DataTable dtItem = DBHandler.GetData(Query);
            if (dtItem == null)
            { return; }
            if (dtItem.Rows.Count == 0)
            { return; }



            if(Convert.ToBoolean(dtItem.Rows[0]["ShowSize"]) == true)
            {
                Query = String.Format("SELECT *,(SELECT COUNT(*) FROM optiongroup WHERE menuitemid={0} and SizeID = '{1}') AS Options FROM menuitems,menusizes WHERE menuitems.ID=menusizes.MenuItemID AND menuitems.ID='{0}' AND menusizes.ID='{1}'", ItemID, SizeID);

                dtItem = DBHandler.GetData(Query);
            }

            Price = dtItem.Rows[0]["Price"].ToString();

            if (Request["OrderType"] != null)
            {
                if (!checkForSQLInjection(Request["OrderType"]))
                {
                    if (Request["OrderType"].ToString() == "TakeAway")
                    {
                        Price = dtItem.Rows[0]["TakeawayPrice"].ToString();
                    }
                }
            }

            OriginalPrice = dtItem.Rows[0]["OriginalPrice"].ToString();

            string ItemName = dtItem.Rows[0]["Name"].ToString() + " " + dtItem.Rows[0]["Size"].ToString();
            string Size = dtItem.Rows[0]["Size"].ToString();
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
                if (r["ItemID"].ToString() == ItemID && r["SizeID"].ToString() == SizeID && ItemOptionCount == 0)
                {
                    r["Qty"] = Int32.Parse(r["Qty"].ToString()) + Int32.Parse(Qty);
                    r["Total"] = Int32.Parse(r["Qty"].ToString()) * Int32.Parse(r["Price"].ToString());
                    AddNew = false;
                }

            }

            //Lookup Item Options

            if (ItemOptionCount > 0 && Request["Optslt"] == null)
            {
                //ItemOptions.Visible = true;
                OptionsHTML = " <div class='Modal' > <script> document.body.style.overflow = 'hidden';</script>  <div class='ModalArea'><div class='Popup'><div class='PopupBlock' style='position:relative;'><img src='Images/close_red.png' width='40px' id='CancelOption' style='position: absolute;right: 0;top: 0px;'><div class='Option'>";
                DataTable dtOption = new DataTable();
                DataTable dtOption1 = DBHandler.GetData("select * from optiongroup as o,menuitems as mi where menuitemid='" + ItemID + "' and o.menuitemid=mi.id order by o.`order`, o.id desc");

                try
                {
                    if (ItemID != "430513" && ItemID != "430512")
                    {
                        dtOption1 = DBHandler.GetData(" (SELECT *, 'Normal' AS GroupType FROM optiongroup AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC  ) " +
                                " UNION  (SELECT *, 'Additional' AS GroupType FROM `optiongroupadditional` AS o,menuitems AS mi WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID1 + "' AND o.menuitemid=mi.id  ORDER BY o.`order`,  o.id DESC ) ");
                    }

                    // dtOption.DefaultView.Sort = "order";

                    DataView dv = dtOption1.DefaultView;
                    dv.Sort = "Order";
                    dtOption = dv.ToTable();
                }

                catch (Exception ex)
                { }
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
                                OptionsHTML += "<input type='checkbox'  style='font-size:14px;' name='" + r["Name"] + "' value='" + ri["ID"] + "'>" + ri["Name"] + " (Rs " + ri["Price"] + ")";
                            }
                            else
                            {
                                OptionsHTML += "<input type='radio' style='font-size:14px;' class='required' name='" + r["Name"] + "' value='" + ri["ID"] + "'>" + ri["Name"] + " (Rs " + ri["Price"] + ")";
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


            if (AddNew)
            {
                //Add into Cart
                int ID = FoodCart().Rows.Count + 1;

                try
                {
                    if (FoodCart().Rows.Count > 0)
                    {
                        DataRow[] myrow = FoodCart().Select("ID = MAX(ID)");
                        ID = Convert.ToInt32(myrow[0]["ID"]) + 1;
                    }
                }
                catch (Exception exx)
                {
                }

                int Total = Int32.Parse(Qty) * Int32.Parse(Price);

                //Added and Commented By Aman Mansoor on 12-Mar-2014 for Walls Work EAT-423
                bool IsGlobal = false;
                String CategoryID = "", CategoryName = "", NewItemName = "", ItemDescription = "", logo = "", URL = "", OutletID = "";
                DataTable dtGlobal = DBHandler.GetData("SELECT mc.*, mi.Name AS ItemName, mi.Description as ItemDescription FROM menuitems mi INNER JOIN menucategories mc ON mi.categoryID = mc.ID WHERE mi.ID = " + ItemID);
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
                FoodCart().Rows.Add(ID, ItemID, ItemName, SizeID, Size, Price, OriginalPrice, Qty, Total, IsGlobal, CategoryID, CategoryName, NewItemName, ItemDescription, logo, URL, OutletID);
                //Added end By Aman Mansoor on 12-Mar-2014 EAT-423

                if (Request["Optslt"] != null)
                {
                    //DataTable dtOption = DBHandler.GetData("select * from optiongroup where menuitemid='"+ItemID+"'");
                    DataTable dtOption = DBHandler.GetData("SELECT *, 'Normal' AS GroupType FROM optiongroup WHERE menuitemid='" + ItemID + "'  UNION SELECT *, 'Additional' AS GroupType FROM optiongroupadditional WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "'  ORDER BY `Order`");


                    if (Convert.ToBoolean(dtItem.Rows[0]["ShowSize"]) == true)
                    {
                        dtOption = DBHandler.GetData("SELECT *, 'Normal' AS GroupType FROM optiongroup WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "'  UNION SELECT *, 'Additional' AS GroupType FROM optiongroupadditional WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "'  ORDER BY `Order`");
                    }

                    if (HalfnHalf)
                    {
                        dtOption = DBHandler.GetData("SELECT *, 'Normal' AS GroupType FROM optiongroup WHERE menuitemid='" + ItemID + "'  UNION SELECT *, 'Additional' AS GroupType FROM optiongroupadditional WHERE menuitemid='" + ItemID + "' AND SizeID = '" + SizeID + "'  UNION  (SELECT *, 'Additional' AS GroupType FROM optiongroupadditional AS o WHERE  o.name in ('CHOOSE YOUR SECOND FLAVOR' ,'EXTRA TOPPING SECOND FLAVOR','EXTRA VEGGIES SECOND FLAVOR' ) ORDER BY o.`order`,  o.id DESC  ) ");

                    }
                    
                    foreach (DataRow ro in dtOption.Rows)
                    {
                        string Opt = ro["Name"].ToString();
                        string[] OptionItems = Request.QueryString.GetValues(Opt);

                        //if (Opt == "CHOOSE YOUR SECOND FLAVOR")
                        //{
                        //    //OptionItems =new string[] { Request.QueryString.GetValues(Opt).ToString().Split(',')[0] };
                        //    OptionItems = new string[] { Request.QueryString.GetValues(Opt)[0] };
                        //}
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
            DataTable dtCart = (DataTable)Session["Cart"];
            if (dtCart == null)
            {
                dtCart = new DataTable();
                dtCart.Columns.Add("ID", typeof(Int32));
                dtCart.Columns.Add("ItemID");
                dtCart.Columns.Add("Item");
                dtCart.Columns.Add("SizeID");
                dtCart.Columns.Add("Size");
                dtCart.Columns.Add("Price");
                dtCart.Columns.Add("OriginalPrice");
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
            DataTable dtCartOptions = (DataTable)Session["CartOptions"];
            if (dtCartOptions == null)
            {
                dtCartOptions = new DataTable();
                dtCartOptions.Columns.Add("CartID");
                dtCartOptions.Columns.Add("OptionID");
                dtCartOptions.Columns.Add("Option");
                dtCartOptions.Columns.Add("Price");
                dtCartOptions.Columns.Add("Qty");
                dtCartOptions.Columns.Add("Total", typeof(Int32));
                dtCartOptions.Columns.Add("optiongroupName");

                Session["CartOptions"] = dtCartOptions;
            }
            return dtCartOptions;
        }

        //Update Cart with new changes
        private void SyncCart()
        {
            Session["Cart"] = FoodCart();
            Session["CartOptions"] = FoodCartOptions();
        }

        //Clear Cart
        private void EmptyCart()
        {
            Session["Cart"] = null;
            Session["CartOptions"] = null;
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

        public static Boolean checkForSQLInjection(string userInput)
        {

            bool isSQLInjection = false;
            if (userInput != null)
            {
                string[] sqlCheckList = { "--","'",

                                       ";--",

                                       ";",

                                       "/*",

                                       "*/",

                                        "@@",
                                         

                                        "char",

                                       "nchar",

                                       "varchar",

                                       "nvarchar",

                                       "alter",

                                       "begin",

                                       "cast",

                                       "create",

                                       "cursor",

                                       "declare",

                                       "delete",

                                       "drop",

                                       "end",

                                       "exec",

                                       "execute",

                                       "fetch",

                                            "insert",

                                          "kill",

                                             "select",

                                           "sys",

                                            "sysobjects",

                                            "syscolumns",

                                           "table",

                                           "update"

                                       };

                string CheckString = userInput.Replace("'", "''");

                for (int i = 0; i <= sqlCheckList.Length - 1; i++)
                {

                    if ((CheckString.IndexOf(sqlCheckList[i],

        StringComparison.OrdinalIgnoreCase) >= 0))

                    { isSQLInjection = true; }
                }
            }
            return isSQLInjection;
        }

    }
}