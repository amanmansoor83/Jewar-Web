using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Web.Script.Services;
using System.Text.RegularExpressions;
using System.Globalization;
using Jewar.CodeLibrary;
using System.Text;

namespace Jewar.Handler
{
    public partial class Actions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check for tranfer of key, log entry.
        }
 
        ///////////////////////////////////////////////////////////////////////////
        // Added deliverytime By Junaid Hassan 
        // Dated : 2013-11-20
        // Purpose To send Delivery Time on SMS We are not calculating DeliveryTime Again.
        // Reference Ticket : EAT-67
        ///////////////////////////////////////////////////////////////////////////
        [WebMethod]
        public static string ProcessOrder(string OutletID, string OutletName, string customerName, string customerPhone, string DeliveryAddress, string DeliveryFee, string OrderType, string DeliveryArea, string Notes, string discount, string otherdiscount, string tax, string deliverytime, string PreOrderDeliveryTime, string PaymentType, string ConvenienceFeePercentage, string ConvenienceFee, string CreditCardDiscountPercentage, string CreditCardDiscountAmount, string Referrer, string TotalAmount)
        {
            int NewOrder = 0;
            string PaymentType1 = PaymentType;
            try
            {
                customerPhone = ValidationManager.validateMobileNumber(customerPhone);// ValidationManager. validateMobileNumber()

                string ResponseIPAddress = "";
                try
                {
                    ResponseIPAddress = HttpContext.Current.Session["ResponseIPAddress"].ToString();
                }
                catch (Exception eeee)
                { }
                
                //int a = Convert.ToInt32("abc");
                /// ////////////////////////////////////////////////// START ////////////////////////////////////////////////// 
                /// Added By            : Junaid Hassan
                /// Purpose             : tohandle PreOrder
                /// TaskID              : EAT-586
                string strIsPreOrder = "";
                if (PreOrderDeliveryTime != "ASAP")
                {
                    strIsPreOrder = "Pre-Order";

                    if (PreOrderDeliveryTime.Split(' ')[1] == "PM")
                    {
                        PreOrderDeliveryTime = DateTime.Now.ToString("yyyy-MM-dd") + " " + PreOrderDeliveryTime;
                    }
                    else
                    {
                        if (DateTime.Now.TimeOfDay.Hours > 12)
                        {
                            PreOrderDeliveryTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " " + PreOrderDeliveryTime;
                        }
                        else
                        {
                            PreOrderDeliveryTime = DateTime.Now.ToString("yyyy-MM-dd") + " " + PreOrderDeliveryTime;
                        }
                    }
                }
                /// ////////////////////////////////////////////////// START ////////////////////////////////////////////////// 

                /// ---------------------------- START -------------------------------------------------
                /// Added By Junaid Hassan 
                /// dated   : 20131218
                /// TaskID  : EAT-181
                //customerName = customerName.Replace("'", "''");
                //DeliveryAddress = DeliveryAddress.Replace("'", "''");
                /// ---------------------------- END  -------------------------------------------------

                bool send = false;
                DeliveryFee = DeliveryFee != "" ? DeliveryFee : "0";
                customerName = Process.TextCase(customerName);
                DataTable dtorderdetail = (DataTable)HttpContext.Current.Session["Cart"];
                DataTable dtorderdetailOption = (DataTable)HttpContext.Current.Session["CartOptions"];

                HttpContext.Current.Session["Cart"] = null;
                HttpContext.Current.Session["CartOptions"] = null;

                if (dtorderdetail.Rows.Count > 0)
                {

                    dtorderdetail.Columns.Add("NewColumn", typeof(System.Int32));

                    foreach (DataRow row in dtorderdetail.Rows)
                    {
                        //need to set value to NewColumn column
                        row["NewColumn"] = 0;   // or set it to some other value
                    }

                }
                /// ------------------------------------------------------------- START ------------------------------------------------------------- 
                //// Modified By Junaid Hassan 
                /// Dated       : 20131218
                /// Purpose     : Remove the Area Wise search in case of TakeAway
                /// Task ID     : EAT-171
                // -- OLD
                ////load area wise branch
                // DataTable dtoutletinfo = DBHandler.GetData("select * from outlets where name like '%" + OutletName.Split(',')[0].Replace("'", "''") + "%' and delivery_localities like '%" + DeliveryArea.Replace("'", "''") + "%' and is_delivers=1");
                // -- OLD
                DataTable dtoutletinfo = new DataTable();
                if (OrderType == "TakeAway")
                {
                    //dtoutletinfo = DBHandler.GetData("select *,'' AS IsOpen from outlets where ID = " + OutletID);
                    dtoutletinfo = DBHandler.GetData("select *,'' AS IsOpen from outlets where name = '" + DeliveryArea + "'");
                }
                else
                {
                    // Modified Added City Check 
                    // dtoutletinfo = DBHandler.GetData("select * from outlets where name like '%" + OutletName.Split(',')[0].Replace("'", "''") + "%' and delivery_localities like '%" + DeliveryArea.Replace("'", "''") + "%' and is_delivers=1");
                    if (!string.IsNullOrEmpty(Process.CityName))
                    {
                        //Added By Aman Mansoor on 15-Aug-2014 to check if Cookie has Pakistan then extract the City from Outlet ID and set cookie EAT-795
                        if (Process.CityName == "Pakistan")
                        {
                            DataTable dtCity = DBHandler.GetData("select city from outlets where id = " + OutletID);

                            //Cookies.CreateCookie("UserCity", dtCity.Rows[0]["City"].ToString().ToLower(), 30);

                            Process.CityName = dtCity.Rows[0]["City"].ToString();

                        }
                        //Added end By Aman Mansoor on 15-Aug-2014 to check if Cookie has Pakistan then extract the City from Outlet ID and set cookie EAT-795

                        // EAT-763 dtoutletinfo = DBHandler.GetData("select *, '' AS IsOpen from outlets where name like '%" + OutletName.Split(',')[0].Replace("'", "''") + "%' and delivery_localities like '%" + DeliveryArea.Replace("'", "''") + "%' and is_delivers=1 and City = '" + Process.CityName + "'");
                        dtoutletinfo = DBHandler.GetData("select *, '' AS IsOpen from outlets where name like '%" + OutletName.Split(',')[0].Replace("'", "''") + "%' and delivery_localities like '%" + DeliveryArea.Replace("'", "''") + "%' and is_delivers=1 and City = '" + Process.CityName + "' AND outletstatus = 'In Business' AND Vendor_ID = ( SELECT Vendor_ID FROM outlets WHERE ID = " + OutletID + ")");
                    }
                    else
                    {
                        // EAT-763 dtoutletinfo = DBHandler.GetData("select *, '' AS IsOpen from outlets where name like '%" + OutletName.Split(',')[0].Replace("'", "''") + "%' and delivery_localities like '%" + DeliveryArea.Replace("'", "''") + "%' and is_delivers=1 "); 
                        dtoutletinfo = DBHandler.GetData("select *, '' AS IsOpen from outlets where name like '%" + OutletName.Split(',')[0].Replace("'", "''") + "%' and delivery_localities like '%" + DeliveryArea.Replace("'", "''") + "%' and is_delivers=1 AND outletstatus = 'In Business' AND Vendor_ID = ( SELECT Vendor_ID FROM outlets WHERE ID = " + OutletID + ")");
                    }
                }
                
                OutletID = dtoutletinfo.Rows[0]["id"].ToString();
                // START : EAT-783 // Added By Junaid Hassan
                string strDSPID="0";
                strDSPID = dtoutletinfo.Rows[0]["DSPID"].ToString();

                string strDeliveryCommissionPercentage = "0";
                 strDeliveryCommissionPercentage = dtoutletinfo.Rows[0]["Delivery_Commision"].ToString();

                // END : EAT-783

                OutletName = dtoutletinfo.Rows[0]["name"].ToString();

                string DeliveryTime = "0", DeliveryTax = "0", MinimumOrder = "0", Discount = "0";

                if (!string.IsNullOrEmpty(dtoutletinfo.Rows[0]["delivery_time"].ToString()))
                {
                    DeliveryTime = dtoutletinfo.Rows[0]["delivery_time"].ToString();

                    if (OrderType == "TakeAway")
                    {
                        DeliveryTime = "20";
                    }
                }
                if (!string.IsNullOrEmpty(dtoutletinfo.Rows[0]["delivery_tax"].ToString()))
                {
                    DeliveryTax = dtoutletinfo.Rows[0]["delivery_tax"].ToString();
                }
                if (!string.IsNullOrEmpty(dtoutletinfo.Rows[0]["delivery_minimum"].ToString()))
                {
                    MinimumOrder = dtoutletinfo.Rows[0]["delivery_minimum"].ToString();
                }
                if (!string.IsNullOrEmpty(dtoutletinfo.Rows[0]["delivery_discount"].ToString()))
                {
                    Discount = dtoutletinfo.Rows[0]["delivery_discount"].ToString();
                }

                //check if the area has special minimum order/deliver fees
                string strArea = string.Format("select * from outletdeliveryareas where OutletID = '{0}' and area = '{1}'", OutletID, DeliveryArea);
                DataTable dtArea = DBHandler.GetData(strArea);

                //if it has then replace with original MO/DF
                if (dtArea.Rows.Count > 0)
                {
                    DeliveryTime = dtArea.Rows[0]["DeliveryTime"].ToString();

                    MinimumOrder = dtArea.Rows[0]["MinimumOrder"].ToString();
                }


                //Cookies.CreateCookie("UserAddress", DeliveryAddress, 365);

                int Delivery = Int32.Parse(DeliveryFee);

              

                //Added By Aman Mansoor on 17-Dec-2013 to check if order type is take away then set it as Pick Up (Issue # EAT-178)
                string MyOrderType = OrderType == "TakeAway" ? "Pick Up" : OrderType;
                //End By Aman (Issue # EAT-178)

                string IsOpen = "";
                string strScreenMsg = "";

                if (Convert.ToInt32(DeliveryTime) > 120) // Added By Junaid Hassan For Pre-Order Don't Need to check if Outlet Is open or Not if DeliveryTime is less than 2 hrs
                {
                    //Added By Aman Mansoor on 28-Nov-2013 for check if the restaurant time is open or close (Issue # EAT-97)
                    string day = DateTime.Now.DayOfWeek.ToString();

                    if (day.Substring(0, 2) == "Su" || day.Substring(0, 2) == "Sa")
                    {
                        IsOpen = Process.IsOpen(dtoutletinfo.Rows[0]["weekend_timing"].ToString().Split('-')[0], dtoutletinfo.Rows[0]["weekend_timing"].ToString().Split('-')[1]);
                    }
                    else
                    {
                        IsOpen = Process.IsOpen(dtoutletinfo.Rows[0]["weekday_timing"].ToString().Split('-')[0], dtoutletinfo.Rows[0]["weekday_timing"].ToString().Split('-')[1]);
                    }

                    strScreenMsg = "Your order has been sent to the restaurant, we will confirm delivery time by SMS shortly.";
                }
                else
                {
                    IsOpen = "open";
                }

                if (IsOpen == "open")
                {
                    //End By Aman
                    if (dtorderdetail.Rows.Count > 0)
                    {

                        /// /////////////////////////////////////////////////////////// START ///////////////////////////////////////////////////////////// 
                        /// Modified By     : Junaid Hassan
                        /// Dated           : 2014-07-23
                        /// Purpose         : get the isVerified Field from method
                        //Add customer
                        string[] strArr = Process.customer(customerName, customerPhone);
                        Int64 customerID = Convert.ToInt64(strArr[0]);
                        string strCustVerified = strArr[1];
                        /// /////////////////////////////////////////////////////////// END ///////////////////////////////////////////////////////////// 


                        /// /////////////////////////////////////////////////////////// START ///////////////////////////////////////////////////////////// 
                        /// Added By        : Junaid Hassan & Aman Mansoor 
                        /// Purpose         : Remove the Global Menu Items from Cart and Place seperate Order to appropriate outlet.
                        /// Task ID         : EAT-423

                        // dtorderdetail = placeGlobalOrder(dtorderdetail, dtorderdetailOption, customerID.ToString(), customerName, customerPhone, DeliveryAddress, DeliveryArea, Notes);
                        //DataSet DSFiltered = new DataSet();
    
                        //dtorderdetail = DSFiltered.Tables["DTItems"];
                        //dtorderdetailOption = DSFiltered.Tables["DTOptions"];
                        string SMSMessage = "";
                        /// /////////////////////////////////////////////////////////// END ///////////////////////////////////////////////////////////// 
                        /// 
                        if (dtorderdetail.Rows.Count > 0)
                        {
                            //Add Order
                            int OrderAmount = 0;

                            if (!string.IsNullOrEmpty(TotalAmount))
                            {
                                TotalAmount = TotalAmount.Replace("Total: Rs ", "");
                                OrderAmount = Convert.ToInt32(TotalAmount);
                            }

                            //int NewOrder = DBHandler.InsertDataWithID(String.Format("insert into orders(customerID,DeliveryAddress,DeliveryFee,OrderAmount,OutletID,Status,Source,Channel,OrderType,UserArea,Remarks) values('{0}','{1}','{2}','{3}','{4}','Pending','broadwaypizza.com.pk','Web','{5}','{6}','{7}')", customerID, DeliveryAddress, DeliveryFee, OrderAmount, OutletID, OrderType, DeliveryArea,Notes));


                            //Added By Aman Mansoor on 15-Aug-2014 to extract the City from Outlet ID EAT-795
                            string CityName = "";
                            DataTable dtCity = DBHandler.GetData("select city from outlets where id = " + OutletID);
                            if (dtCity.Rows.Count > 0)
                            {
                                CityName = dtCity.Rows[0]["City"].ToString();
                            }
                            else
                            {
                                CityName = Process.CityName;
                            }

                            /// /////////////////////////////////////// START /////////////////////////////////////// 
                            /// Modified By         : Junaid 
                            /// To Add Status = Pre-Order 
                            /// Task EAt-586 
                            // Commented By Junaid Under Task EAt-586 NewOrder = DBHandler.InsertDataWithID(String.Format("insert into orders(customerID,DeliveryAddress,DeliveryFee,OrderAmount,OutletID,Status,Source,Channel,OrderType,UserArea,Remarks,customerName,customerMobile,City, Discount, DeliveryTime, DeliveryTax, MinimumOrder) values('{0}','{1}','{2}','{3}','{4}','Pending','broadwaypizza.com.pk','Web','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", customerID, DeliveryAddress.Replace("'", "''"), DeliveryFee, OrderAmount, OutletID, MyOrderType, DeliveryArea, Notes, customerName.Replace("'", "''"), customerPhone, Process.CityName, Discount, DeliveryTime, DeliveryTax, MinimumOrder));
                            string strCurrentStatus = "";
                            int DSPCommissionPercentage = 0;
                            int DSPFee = 0;
                            decimal DSPCommissionAmount = 0;

                            //Added by Aman Mansoor on 10-Oct-2014 to check referrer and set it in session EAT-814
                            string contactSource = Utilities.CheckReferrer();
                            if (Referrer != "")
                            {
                                contactSource = Referrer;
                            }
                            //Added end by Aman Mansoor on 10-Oct-2014 to check referrer and set it in session EAT-814

                            if (strIsPreOrder == "Pre-Order")
                            {
                                //NewOrder = DBHandler.InsertDataWithID(String.Format("insert into orders(customerID,DeliveryAddress,DeliveryFee,OrderAmount,OutletID,Status,Source,Channel,OrderType,UserArea,Remarks,customerName,customerMobile,City, Discount, DeliveryTime, DeliveryTax, MinimumOrder, OrderDateTime, notes) values('{0}','{1}','{2}','{3}','{4}','Pre-Order" + strCustVerified + "','broadwaypizza.com.pk','Web','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}', '{15}', 'Pre-Order, Delivery required at {16}')", customerID, DeliveryAddress.Replace("'", "''"), DeliveryFee, OrderAmount, OutletID, MyOrderType, DeliveryArea, Notes, customerName.Replace("'", "''"), customerPhone, Process.CityName, Discount, DeliveryTime, DeliveryTax, MinimumOrder, Convert.ToDateTime(PreOrderDeliveryTime).ToString("yyyy-MM-dd H:mm"), Convert.ToDateTime(PreOrderDeliveryTime).ToString("yyyy-MM-dd hh:mm tt")));

                                /// ////////////////////////////////////////////// START ////////////////////////////////////////////// 
                                /// Added By        : Junaid Hassan
                                /// Purpose         : check if outlet has any DSP(Delivery Service Provider) 
                                ///                   if yes the save the current status in OriginalStatus column and add "Delivery" as Status in order table.
                                /// TaskID          : EAT-783  
                                // strCurrentStatus = "Pre-Order" + strCustVerified;
                                strCurrentStatus = "Pre-Order" +strCustVerified;
                                string strOriginalStatus = "";
                                //int DSPCommissionPercentage = 0;
                                //int DSPFee = 0;
                                //decimal DSPCommissionAmount = 0;

                                if (strDSPID != "0" && MyOrderType.ToLower() == "delivery")
                                {
                                    strCurrentStatus = strCurrentStatus.Replace(" Unverified", ""); // incase of DSP don't put Unverfied in OrignalStatus

                                    strOriginalStatus = strCurrentStatus;
                                    strCurrentStatus = "Delivery Pre-Order" + strCustVerified;

                                    //DataTable dtDSP = DBHandler.GetData(string.Format("SELECT IFNULL(CommissionPercentage,0) as CommissionPercentage, IFNULL(DSPFee,0) as DSPFee FROM deliveryserviceprovider where id = '{0}'", strDSPID));

                                    //if (dtDSP.Rows.Count > 0)
                                    //{
                                    //    DSPCommissionPercentage = Convert.ToInt32(dtDSP.Rows[0]["CommissionPercentage"]);
                                    //    DSPFee = Convert.ToInt32(dtDSP.Rows[0]["DSPFee"]);

                                    //    int intDeliveryFee = 0;
                                    //    int.TryParse(DeliveryFee, out intDeliveryFee);

                                    //    DSPCommissionAmount = ((Convert.ToDecimal(OrderAmount - intDeliveryFee) * DSPCommissionPercentage) / 100) + DSPFee;
                                    //}
                                }
                                // NewOrder = DBHandler.InsertDataWithID(String.Format("insert into orders(customerID,DeliveryAddress,DeliveryFee,OrderAmount,OutletID,Status,Source,Channel,OrderType,UserArea,Remarks,customerName,customerMobile,City, Discount, DeliveryTime, DeliveryTax, MinimumOrder, OrderDateTime, notes) values('{0}','{1}','{2}','{3}','{4}','Pre-Order" + strCustVerified + "','broadwaypizza.com.pk','Web','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}', '{15}', 'Pre-Order, Delivery required at {16}')", customerID, DeliveryAddress.Replace("'", "''"), DeliveryFee, OrderAmount, OutletID, MyOrderType, DeliveryArea, Notes, customerName.Replace("'", "''"), customerPhone, CityName, Discount, DeliveryTime, DeliveryTax, MinimumOrder, Convert.ToDateTime(PreOrderDeliveryTime).ToString("yyyy-MM-dd H:mm"), Convert.ToDateTime(PreOrderDeliveryTime).ToString("yyyy-MM-dd hh:mm tt")));
                                // NewOrder = DBHandler.InsertDataWithID(String.Format("insert into orders(customerID,DeliveryAddress,DeliveryFee,OrderAmount,OutletID,Status,Source,Channel,OrderType,UserArea,Remarks,customerName,customerMobile,City, Discount, DeliveryTime, DeliveryTax, MinimumOrder, OrderDateTime, notes, OriginalStatus, DSPID, DeliveryCommissionPercentage) values('{0}','{1}','{2}','{3}','{4}','{17}','broadwaypizza.com.pk','Web','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}', '{15}', 'Pre-Order, Delivery required at {16}', '{18}', '{19}', '{20}')", customerID, DeliveryAddress.Replace("'", "''"), DeliveryFee, OrderAmount, OutletID, MyOrderType, DeliveryArea, Notes, customerName.Replace("'", "''"), customerPhone, CityName, Discount, DeliveryTime, DeliveryTax, MinimumOrder, Convert.ToDateTime(PreOrderDeliveryTime).ToString("yyyy-MM-dd H:mm"), Convert.ToDateTime(PreOrderDeliveryTime).ToString("yyyy-MM-dd hh:mm tt"), strCurrentStatus, strOriginalStatus, strDSPID, strDeliveryCommissionPercentage));

                                if (PaymentType == "CreditCard")
                                {
                                    PaymentType1 = PaymentType + " - Unpaid";
                                    strCurrentStatus = strCurrentStatus + "- Card Required";
                                }
                                else
                                {
                                    ConvenienceFeePercentage = "0";
                                    CreditCardDiscountPercentage = "0";
                                }

                                NewOrder = DBHandler.InsertDataWithIDForOrder(String.Format("insert into orders(customerID,DeliveryAddress,DeliveryFee,OrderAmount,OutletID,Status,Source,Channel,OrderType,UserArea,Remarks,customerName,customerMobile,City, Discount, DeliveryTime, DeliveryTax, MinimumOrder, OrderDateTime, notes, OriginalStatus, DSPID, DeliveryCommissionPercentage, DSPCommissionPercentage, DSPFee, DSPCommissionAmount, contactSource, SessionID, PaymentType, ConveniencePercentage, ConvenienceAmount, CreditCardDiscountPercentage, CreditCardDiscountAmount,IPAddress)" +
                                                        "values('{0}','{1}','{2}','{3}','{4}','{17}','broadwaypizza.com.pk','Web','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}', '{15}', 'Pre-Order, Delivery required at {16}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', {27}, {28}, {29}, {30},'{31}')"
                                                        , customerID, DeliveryAddress.Replace("'", "''"), DeliveryFee, OrderAmount, OutletID, MyOrderType, DeliveryArea, Notes, customerName.Replace("'", "''"), customerPhone, CityName, Discount, DeliveryTime, DeliveryTax, MinimumOrder, Convert.ToDateTime(PreOrderDeliveryTime).ToString("yyyy-MM-dd H:mm"), Convert.ToDateTime(PreOrderDeliveryTime).ToString("yyyy-MM-dd hh:mm tt"), strCurrentStatus, strOriginalStatus, strDSPID, strDeliveryCommissionPercentage, DSPCommissionPercentage, DSPFee, DSPCommissionAmount, contactSource, HttpContext.Current.Session.SessionID, PaymentType1, ConvenienceFeePercentage, ConvenienceFee, CreditCardDiscountPercentage, CreditCardDiscountAmount, ResponseIPAddress));

                                /// ////////////////////////////////////////////// END  ////////////////////////////////////////////// 
                            }
                            else
                            {
                                /// ////////////////////////////////////////////// START ////////////////////////////////////////////// 
                                /// Added By        : Junaid Hassan
                                /// Purpose         : check if outlet has any DSP(Delivery Service Provider) 
                                ///                   if yes the save the current status in OriginalStatus column and add "Delivery" as Status in order table.
                                /// TaskID          : EAT-783  

                                strCurrentStatus = "Pending" + strCustVerified;
                                string strOriginalStatus = "";
                                

                                if (strDSPID != "0" && MyOrderType.ToLower() == "delivery")
                                {
                                    strCurrentStatus = strCurrentStatus.Replace(" Unverified", "");// incase of DSP don't put Unverfied in OrignalStatus
                                    strOriginalStatus = strCurrentStatus;
                                    strCurrentStatus = "Delivery" + strCustVerified;

                                    //DataTable dtDSP = DBHandler.GetData(string.Format("SELECT IFNULL(CommissionPercentage,0) as CommissionPercentage, IFNULL(DSPFee,0) as DSPFee FROM deliveryserviceprovider where id = '{0}'", strDSPID));

                                    //if (dtDSP.Rows.Count > 0)
                                    //{
                                    //    DSPCommissionPercentage = Convert.ToInt32(dtDSP.Rows[0]["CommissionPercentage"]);
                                    //    DSPFee = Convert.ToInt32(dtDSP.Rows[0]["DSPFee"]);

                                    //    int intDeliveryFee = 0;
                                    //    int.TryParse(DeliveryFee, out intDeliveryFee);

                                    //    DSPCommissionAmount = ((Convert.ToDecimal(OrderAmount - intDeliveryFee) * DSPCommissionPercentage) / 100) + DSPFee;
                                    //}
                                }
                                
                                // NewOrder = DBHandler.InsertDataWithID(String.Format("insert into orders(customerID,DeliveryAddress,DeliveryFee,OrderAmount,OutletID,Status,Source,Channel,OrderType,UserArea,Remarks,customerName,customerMobile,City, Discount, DeliveryTime, DeliveryTax, MinimumOrder) values('{0}','{1}','{2}','{3}','{4}','Pending" + strCustVerified + "','broadwaypizza.com.pk','Web','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", customerID, DeliveryAddress.Replace("'", "''"), DeliveryFee, OrderAmount, OutletID, MyOrderType, DeliveryArea, Notes, customerName.Replace("'", "''"), customerPhone, CityName, Discount, DeliveryTime, DeliveryTax, MinimumOrder));
                                // NewOrder = DBHandler.InsertDataWithID(String.Format("insert into orders(customerID,DeliveryAddress,DeliveryFee,OrderAmount,OutletID,Status,Source,Channel,OrderType,UserArea,Remarks,customerName,customerMobile,City, Discount, DeliveryTime, DeliveryTax, MinimumOrder, OriginalStatus, DSPID, DeliveryCommissionPercentage) values('{0}','{1}','{2}','{3}','{4}','{5}','broadwaypizza.com.pk','Web','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}', '{16}', '{17}', '{18}')", customerID, DeliveryAddress.Replace("'", "''"), DeliveryFee, OrderAmount, OutletID, strCurrentStatus, MyOrderType, DeliveryArea, Notes, customerName.Replace("'", "''"), customerPhone, CityName, Discount, DeliveryTime, DeliveryTax, MinimumOrder, strOriginalStatus, strDSPID, strDeliveryCommissionPercentage));

                                if (PaymentType == "CreditCard")
                                {
                                    PaymentType1 = PaymentType + " - Unpaid";
                                    strCurrentStatus = strCurrentStatus + "- Card Required";
                                }
                                else
                                {
                                    ConvenienceFeePercentage = "0";
                                    CreditCardDiscountPercentage = "0";
                                }

                                NewOrder = DBHandler.InsertDataWithIDForOrder(String.Format("insert into orders(customerID,DeliveryAddress,DeliveryFee,OrderAmount,OutletID,Status,Source,Channel,OrderType,UserArea,Remarks,customerName,customerMobile,City, Discount, DeliveryTime, DeliveryTax, MinimumOrder, OriginalStatus, DSPID, DeliveryCommissionPercentage, DSPCommissionPercentage, DSPFee, DSPCommissionAmount, contactSource, SessionID, PaymentType, ConveniencePercentage, ConvenienceAmount, CreditCardDiscountPercentage, CreditCardDiscountAmount, Created, ModifiedDate, IPAddress )" +
                                    "values('{0}','{1}','{2}','{3}','{4}','{5}','broadwaypizza.com.pk','Web','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', {25}, {26}, {27}, {28},'{29}','{29}','{30}')"
                                    , customerID, DeliveryAddress.Replace("'", "''"), DeliveryFee, OrderAmount, OutletID, strCurrentStatus, MyOrderType, DeliveryArea, Notes, customerName.Replace("'", "''"), customerPhone, CityName, Discount, DeliveryTime, DeliveryTax, MinimumOrder, strOriginalStatus, strDSPID, strDeliveryCommissionPercentage, DSPCommissionPercentage, DSPFee, DSPCommissionAmount, contactSource, HttpContext.Current.Session.SessionID, PaymentType1, ConvenienceFeePercentage, ConvenienceFee, CreditCardDiscountPercentage, CreditCardDiscountAmount, DateTime.Now.AddHours(Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["AddHours"])).ToString("yyyy-MM-dd H:mm"), ResponseIPAddress));
                                


                                /// ////////////////////////////////////////////// START ////////////////////////////////////////////// 
                                //NewOrder = DBHandler.InsertDataWithID(String.Format("insert into orders(customerID,DeliveryAddress,DeliveryFee,OrderAmount,OutletID,Status,Source,Channel,OrderType,UserArea,Remarks,customerName,customerMobile,City, Discount, DeliveryTime, DeliveryTax, MinimumOrder) values('{0}','{1}','{2}','{3}','{4}','Pending" + strCustVerified + "','broadwaypizza.com.pk','Web','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", customerID, DeliveryAddress.Replace("'", "''"), DeliveryFee, OrderAmount, OutletID, MyOrderType, DeliveryArea, Notes, customerName.Replace("'", "''"), customerPhone, Process.CityName, Discount, DeliveryTime, DeliveryTax, MinimumOrder));
                                // NewOrder = DBHandler.InsertDataWithID(String.Format("insert into orders(customerID,DeliveryAddress,DeliveryFee,OrderAmount,OutletID,Status,Source,Channel,OrderType,UserArea,Remarks,customerName,customerMobile,City, Discount, DeliveryTime, DeliveryTax, MinimumOrder) values('{0}','{1}','{2}','{3}','{4}','Pending" + strCustVerified + "','broadwaypizza.com.pk','Web','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", customerID, DeliveryAddress.Replace("'", "''"), DeliveryFee, OrderAmount, OutletID, MyOrderType, DeliveryArea, Notes, customerName.Replace("'", "''"), customerPhone, CityName, Discount, DeliveryTime, DeliveryTax, MinimumOrder));
                            }
                            /// /////////////////////////////////////// END  ///////////////////////////////////////                        
                            //Added end By Aman Mansoor on 15-Aug-2014 to extract the City from Outlet ID EAT-795

                            //Add Order Detail 
                            int GrossTotal = 0;     //Aman Testing
                            try
                            {   
                                string ItemName = "", CategoryName = "", optiongroupName = "";
                                foreach (DataRow r in dtorderdetail.Rows)
                                {
                                    //Added By aman mansoor to get menu name and other related details to save in orders/orderdetails/orderdetailsoption tables                       
                                    DataTable dtMenuInfo = DBHandler.GetData("SELECT mi.name AS ItemName, mi.Description , mc.name AS CategoryName FROM menuitems mi INNER JOIN menucategories mc ON mi.categoryid = mc.id WHERE mi.id =  " + r["ItemID"]);

                                    if (dtMenuInfo.Rows.Count > 0)
                                    {
                                        ItemName = dtMenuInfo.Rows[0]["ItemName"].ToString().Replace("'", "");

                                        try
                                        {
                                            if (ItemName == "WINTER WONDERS DEAL 20 INCH")
                                            {
                                                if (r["Size"].ToString() == "20INCH HALF&HALF")
                                                {
                                                    ItemName = "WINTER WONDERS DEAL 20 INCH HALF & HALF";
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        { }


                                        CategoryName = dtMenuInfo.Rows[0]["CategoryName"].ToString().Replace("'", "");
                                    }
                                    else
                                    {
                                        ItemName = "";
                                        CategoryName = "";
                                    }

                                    //int Neworderdetail = DBHandler.InsertDataWithID(String.Format("INSERT INTO orderdetail(`OrderID`,`ItemID`,`Quantity`,`Detail`,`SizeID`,`Price`,`Size`, `ItemName`, `Category`)  values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", NewOrder, r["ItemID"], r["Qty"], r["Item"].ToString().Replace("'", ""), r["SizeID"], r["Price"], r["Size"]));
                                    int Neworderdetail = DBHandler.InsertDataWithIDForOrder(String.Format("INSERT INTO orderdetail(`OrderID`,`ItemID`,`Quantity`,`Detail`,`SizeID`,`Price`,`Size`, `ItemName`, `Category`, `OriginalPrice`)  values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", NewOrder, r["ItemID"], r["Qty"], dtMenuInfo.Rows[0]["Description"].ToString().Replace("'", ""), r["SizeID"], r["Price"], r["Size"], ItemName, CategoryName, r["OriginalPrice"]));

                                    //Add Order Detail Options
                                    DataTable dtFilterOption = AppDB.RefineTable(dtorderdetailOption, "CartID='" + r["ID"] + "'");
                                    foreach (DataRow ro in dtFilterOption.Rows)
                                    {
                                        try
                                        {
                                            //Added By aman mansoor to get menu name and other related details to save in orders/orderdetails/orderdetailsoption tables
                                            DataTable dtMenuOptionInfo = DBHandler.GetData("SELECT og.name,og.description from menuoptions mo inner join optiongroup og on mo.optiongroupid = og.id and mo.id = " + ro["OptionID"]);

                                            if (dtMenuOptionInfo.Rows.Count > 0)
                                            {
                                                optiongroupName = dtMenuOptionInfo.Rows[0]["name"].ToString().Replace("'", "");
                                            }
                                            else
                                            {
                                                dtMenuOptionInfo = DBHandler.GetData("SELECT og.name,og.description from menuoptionsadditional mo inner join optiongroupadditional og on mo.optiongroupid = og.id and mo.id = " + ro["OptionID"]);

                                                if (dtMenuOptionInfo.Rows.Count > 0)
                                                {
                                                    optiongroupName = dtMenuOptionInfo.Rows[0]["name"].ToString().Replace("'", "");
                                                }
                                                else
                                                {
                                                    optiongroupName = "";
                                                }
                                            }

                                            int NeworderdetailOption = DBHandler.InsertDataWithIDForOrder(String.Format("INSERT INTO orderdetailoptions(`orderdetailID`,`OptionID`,`Price`,`Quantity`,`OptionName`,`optiongroupName`)  values('{0}','{1}','{2}','{3}','{4}','{5}')", Neworderdetail, ro["OptionID"], ro["Price"], ro["Qty"], ro["Option"], optiongroupName));
                                        }
                                        catch (Exception ex)
                                        {
                                            DBHandler.InsertDataForOrder(string.Format("insert into errorlog(URL, Error,Detail)values('{0}', '{1}', '{2}')", HttpContext.Current.Request.Url, "Catch By Aman. Items Option Block. OrderID = " + NewOrder + " - " + ex.Message, ex.StackTrace));
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                DBHandler.InsertDataForOrder(string.Format("insert into errorlog(URL, Error,Detail)values('{0}', '{1}', '{2}')", HttpContext.Current.Request.Url, "Catch By Aman. Items Block. OrderID = " + NewOrder + " - " + ex.Message, ex.StackTrace));
                            }

                            decimal CommissionAmount = 0, CommissionPercentage = 0; int FirstTimeDiscountAmount = 0;
                            string NetTotal = "0";
                            string SubTotal = "0";
                            try
                            {
                                //Calculation
                                NetTotal = dtorderdetail.Compute("Sum(Total)", "").ToString();
                                NetTotal = NetTotal != "" ? NetTotal : "0";
                                string OptionTotal = dtorderdetailOption.Compute("Sum(Total)", "").ToString();
                                OptionTotal = OptionTotal != "" ? OptionTotal : "0";
                                NetTotal = Int32.Parse(NetTotal) + Int32.Parse(OptionTotal) + "";
                                SubTotal = NetTotal;
                                int DiscountAmount = 0;
                                discount = discount != "" ? discount : "0";
                                if (Int32.Parse(discount) > 0)
                                {
                                    DiscountAmount = (Int32.Parse(NetTotal) * Int32.Parse(discount) / 100);
                                    NetTotal = Int32.Parse(NetTotal) - DiscountAmount + "";
                                }

                               

                                //if (HttpContext.Current.Session["CustomerMobile"] != null)
                                //{
                                //    DataTable dtcustomerdiscountcheck = Jewar.CodeLibrary.DBHandler.GetData("SELECT id,  LoginDiscount  FROM customer WHERE mobile='" + HttpContext.Current.Session["CustomerMobile"] + "'");

                                //    if (dtcustomerdiscountcheck.Rows.Count > 0)
                                //    {
                                //        string LoginDiscount = dtcustomerdiscountcheck.Rows[0]["LoginDiscount"].ToString();


                                //        if (Convert.ToBoolean(LoginDiscount) == false)
                                //        {
                                //            FirstTimeDiscountAmount = 200;
                                //            NetTotal = Int32.Parse(NetTotal) - FirstTimeDiscountAmount + "";

                                //            DBHandler.InsertDataForOrder(string.Format("update customer set LoginDiscount={0} where mobile = '{1}'", true, HttpContext.Current.Session["CustomerMobile"]));
                                //        }
                                //    }
                                //}


                                tax = tax != "" ? tax : "0";
                                int taxAmount = 0;
                                if (Int32.Parse(tax) > 0)
                                {
                                    taxAmount = (Int32.Parse(NetTotal) * Int32.Parse(tax) / 100);
                                    NetTotal = Int32.Parse(NetTotal)  + "";
                                }

                                GrossTotal = Int32.Parse(DeliveryFee) + Int32.Parse(NetTotal) + Int32.Parse(ConvenienceFee) - Int32.Parse(CreditCardDiscountAmount);


                                //Added by Aman Mansoor on 21-Nov-2013 to get delivery commission of the outlet (Issue # EAT-70)
                                DataTable dtoutletcommission = DBHandler.GetData(string.Format("SELECT `delivery_commision` FROM outlets WHERE id = '{0}'", OutletID));
                                if (dtoutletcommission.Rows.Count > 0)
                                {
                                    CommissionPercentage = Convert.ToDecimal(dtoutletcommission.Rows[0]["delivery_commision"]);
                                    CommissionAmount = (GrossTotal * CommissionPercentage) / 100;
                                }
                                //Added End by Aman Mansoor

                                /// ////////////////////////////////////////////// START ////////////////////////////////////////////// 
                                /// Added By        : Junaid Hassan
                                /// Purpose         : check if outlet has any DSP(Delivery Service Provider) 
                                ///                   if yes the save the current status in OriginalStatus column and add "Delivery" as Status in order table.
                                /// TaskID          : EAT-783  
                                if (strDSPID != "0" && MyOrderType.ToLower() == "delivery")
                                {
                                    DataTable dtDSP = DBHandler.GetData(string.Format("SELECT IFNULL(CommissionPercentage,0) as CommissionPercentage, IFNULL(DSPFee,0) as DSPFee FROM deliveryserviceprovider where id = '{0}'", strDSPID));

                                    if (dtDSP.Rows.Count > 0)
                                    {
                                        DSPCommissionPercentage = Convert.ToInt32(dtDSP.Rows[0]["CommissionPercentage"]);
                                        DSPFee = Convert.ToInt32(dtDSP.Rows[0]["DSPFee"]);

                                        int intDeliveryFee = 0;
                                        int.TryParse(DeliveryFee, out intDeliveryFee);

                                        DSPCommissionAmount = ((Convert.ToDecimal(GrossTotal - intDeliveryFee) * DSPCommissionPercentage) / 100) + DSPFee;
                                    }
                                }
                                /// TaskID          : EAT-783  
                                /// ////////////////////////////////////////////// END  ////////////////////////////////////////////// 
                                /// 
                            }
                            catch (Exception ex)
                            {
                                DBHandler.InsertDataForOrder(string.Format("insert into errorlog(URL, Error,Detail)values('{0}', '{1}', '{2}')", HttpContext.Current.Request.Url, "Catch By Aman. Calculation Block. OrderID = " + NewOrder + " - " + ex.Message, ex.StackTrace));
                            }
                            try
                            {
                                //Added by Aman Mansoor on 17-Feb-2015 to check if PTCL verification is required
                                bool IsPTCLRequired = false;
                                string Status = "";
                                string PTCLNotes = "";

                                //Added By Aman Mansoor on 04-June-2015 to check cash loss only on selected outlets
                                if (Convert.ToBoolean(dtoutletinfo.Rows[0]["IsCheckCashLoss"]))
                                {
                                    if (GrossTotal >= 2000)
                                    {
                                        IsPTCLRequired = CheckCashLoss(customerPhone);
                                        if (IsPTCLRequired) // Added By Aman Mansoor on 06-Apr-2015 as Final logic on Rai Request that if customer has placed big order then he should go without Unverified 
                                        {
                                            if (!strCurrentStatus.Contains("Unverified"))
                                            {
                                                Status = " , Status = Concat(Status, ' Unverified')";
                                            }

                                            PTCLNotes = " , Notes = Concat(IFNULL(Notes,''), ' - PTCL Number Required for Big orders')";
                                        }
                                    }
                                }
                                //Added end By Aman Mansoor on 04-June-2015 to check cash loss only on selected outlets
                                //Added end by Aman Mansoor on 17-Feb-2015 to check if PTCL verification is required
                           


                                //Update Order Amount 
                                //Commented and added by aman mansoor on 21-Nov-2013 to update delivery commission with order (Issue # EAT-70)
                                //int Update = DBHandler.InsertData("update orders set OrderAmount='"+GrossTotal+"' where id='"+NewOrder+"'");
                                // int Update = DBHandler.InsertData("update orders set OrderAmount='" + GrossTotal + "' , CommissionAmount='" + CommissionAmount + "' where id='" + NewOrder + "'");

                                int Update = DBHandler.InsertDataForOrder("update orders set OrderAmount='" + SubTotal + "' , CommissionAmount=" + CommissionAmount +
                                    ",DSPCommissionPercentage = " + DSPCommissionPercentage + ", DSPFee=" + DSPFee + ", DSPCommissionAmount = " + DSPCommissionAmount + ", Discount = " + FirstTimeDiscountAmount +
                                    " , IsPTCLRequired = " + Convert.ToInt32(IsPTCLRequired) + Status + PTCLNotes + " , SubTotal = " + SubTotal + " , ModifiedDate = '" + DateTime.Now.AddHours(Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["AddHours"])).ToString("yyyy-MM-dd H:mm") + "'  WHERE ID='" + NewOrder + "'");

                                //Added End by Aman Mansoor

                                /// ///////////////////////////////////////////////// START ///////////////////////////////////////////////// 
                                /// Added By            : Junaid Hassan
                                /// Purpose             : log the Pre-Order Properly
                                if (strIsPreOrder == "Pre-Order")
                                {
                                    DBHandler.InsertDataForOrder("insert into log(object_id,object_type, Channel, logType) values('" + NewOrder + "','New Pre-Order " + strCurrentStatus + "','Web', 'Order')");
                                }
                                else
                                {
                                    DBHandler.InsertDataForOrder("insert into log(object_id,object_type, Channel, logType,date) values('" + NewOrder + "','New Order " + strCurrentStatus + "','Web', 'Order', '" + DateTime.Now.AddHours(Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["AddHours"])).ToString("yyyy-MM-dd H:mm") + "')");
                                }
                            }
                            catch (Exception ex)
                            {
                                DBHandler.InsertDataForOrder(string.Format("insert into errorlog(URL, Error,Detail)values('{0}', '{1}', '{2}')", HttpContext.Current.Request.Url, "Catch By Aman. OrderID = " + NewOrder + " - " + ex.Message, ex.StackTrace));
                            }
                            /// ///////////////////////////////////////////////// END  ///////////////////////////////////////////////// 
                            /// 
                            send = true;
                            HttpContext.Current.Session["Cart"] = null;
                            HttpContext.Current.Session["CartOptions"] = null;

                            //send sms
                            customerPhone = customerPhone.ToString().Replace("92-", "0").Replace("-", "").Replace(" ", "").Replace("+", "");

                            //DataTable dtChkUser = DBHandler.GetData(string.Format("SELECT IFNULL(c.iscustomerVerified,0) AS iscustomerVerified , COUNT(o.ID) AS MyCount, IFNULL(c.Fakeorders,'0') AS  FakeOrder FROM customer c INNER JOIN orders o ON o.customerID = c.ID WHERE c.mobile = '{0}' AND o.status = 'Confirmed' ", customerPhone));
                            DataTable dtChkUser = DBHandler.GetData(string.Format("SELECT IFNULL(c.iscustomerVerified,0) AS iscustomerVerified , IFNULL(c.Fakeorders,'0') AS  FakeOrder FROM customer c  WHERE c.mobile = '{0}'", customerPhone));

                            if (Convert.ToInt32(dtChkUser.Rows[0]["iscustomerVerified"]) == 0)
                            {
                                string VerificationCode = GenerateVerificationCode();

                                //lblCallUser.Text = "New user, call to verify. <a href='sip:" + txtPhone.Value + "' > " + txtPhone.Value + "</a> <br/>";
                                SMSMessage = string.Format("Hey {0} Your order #{1} has been received by broadwaypizza! We will call you shortly to verify your phone number. Your bill is Rs. {2}. Track your order online: https://broadwaypizza.com.pk/track.aspx?t={1}", customerName, NewOrder, GrossTotal);
                                //SMSMessage = string.Format("Hey {0} Your order #{1} has been received by broadwaypizza! Enter verification code: {2} on the website for quick processing of your order. Your bill is Rs. {3}", customerName, NewOrder, VerificationCode, GrossTotal);

                                strScreenMsg = "This is your first order, we will call you shortly from 111-486-479 to verify your mobile number.";
                                //strScreenMsg = "As this is your first order with us we have sent you a SMS text with a verification PIN please enter below for quick processing.";

                                DBHandler.InsertDataForOrder(string.Format("update customer set VerificationCode='{0}' where id = {1}", VerificationCode, customerID));
                            }
                            else
                            {

                                //Commented and added by aman mansoor on 26-Feb-2014 to change pending message EAT-404
                                //string SMSMessage = string.Format("Dear {0}, your food delivery order at {1} for a total of Rs. {2} has been received and will be confirmed shortly. orders cannot be cancelled. In case you have any difficulty with your order please contact broadwaypizza at 111-486479. Your Delivery Order #{3}", customerName, OutletName, GrossTotal.ToString(), NewOrder);                    
                                //SMSMessage = string.Format("Hey {0} Your order #{1} has been received by {2} and we will notify you of the {3} time shortly.", customerName, NewOrder, OutletName.Split(',')[0], MyOrderType.Replace(" ", "").ToLower());
                                SMSMessage = string.Format("Hey {0}, {2} is preparing your order #{1} and will confirm delivery shortly. Your bill is Rs. {3}. Track your order online: https://broadwaypizza.com.pk/track.aspx?t={1}", customerName, NewOrder, OutletName.Split(',')[0], GrossTotal);
                                // Message update by Junaid with reference of EAT-67 (Again old pending message was implemented by Aman Mansoor on 21-Feb-2014 to)
                                //Commented and added end by aman mansoor on 26-Feb-2014 EAT-404

                                if (strIsPreOrder == "Pre-Order")
                                {
                                    SMSMessage = string.Format("Hey {0}, your order #{1} will be sent to {2} as soon as the restaurant opens, the {3} time will be confirmed only then. Your bill is Rs. {4}. Track your order online: https://broadwaypizza.com.pk/track.aspx?t={1}", customerName, NewOrder, OutletName.Split(',')[0], MyOrderType.Replace(" ", "").ToLower(), GrossTotal);
                                }
                                else
                                {
                                    //SMSMessage = string.Format("Hey {0} Your order #{1} has been received by {2} and we will notify you of the {3} time shortly.", customerName, NewOrder, OutletName.Split(',')[0], MyOrderType.Replace(" ", "").ToLower());
                                    SMSMessage = string.Format("Hey {0}, {2} is preparing your order #{1} and will confirm delivery shortly. Your bill is Rs. {3}. Track your order online: https://broadwaypizza.com.pk/track.aspx?t={1}", customerName, NewOrder, OutletName.Split(',')[0], GrossTotal);
                                    
                                }
                            }
                        }
                         
                        DateTime CurrTime = DateTime.Now;
                        string[] arrDelvTime = deliverytime.Split(' ');

                        CurrTime = CurrTime.AddMinutes(Double.Parse(arrDelvTime[0]));

                        //string SMSMessage = string.Format("Your order# {0} at {1} for a total of Rs. {2} has been accepted by the restaurant and will be delivered by {3}. orders once processed can not be canceled. In case you have any difficulty with your order, please contact broadwaypizza at 111-486479.", NewOrder, OutletName, GrossTotal.ToString(), CurrTime);

                        //if order is inserted then update the transaction and order count for the customer
                        if (NewOrder > 0)
                        {
                            string InsertSQL = string.Format("UPDATE customer set `Transactions` = {0}, `orders` = {1} where `ID` = '{2}'", "IFNULL(Transactions,0) + " + 1, "IFNULL(orders,0) + " + 1, customerID);
                            DBHandler.InsertDataForOrder(InsertSQL);
                        }

                        //SMS.SendMessage(customerPhone, SMSMessage);

                        //Cookies.CreateCookie("customerName", customerName.Replace(",", ""), 365);
                        //Cookies.CreateCookie("customerMobile", customerPhone, 365);
                        //Cookies.CreateCookie("customerAddress", DeliveryAddress, 365);
                        //Commented and Added end By Aman Mansoor on 20-Aug-2014 to change cookie functionality EAT-760
                    }
                }
                if (send == true)
                {
                    /// ///////////////////////////////////////////////////// START ////////////////////////////////////////////////
                    /// Added By            : Junaid Hassan
                    /// Dated               : 2014-05-16
                    /// Purpose             : Show Different Mesg To New customer      
                    /// 
                    HttpContext.Current.Session["OrderID"] = NewOrder;

                    string strreturn = "{\"success\":true,\"NewOrderID\":\"" + NewOrder + "\",\"ScreenMsg\":\"" + strScreenMsg + "\",\"PaymentType\":\"" + PaymentType + "\",\"FailVerification\":\"\"}";
                    
                    if (PaymentType == "CreditCard")
                    {
                        strreturn = "{\"success\":true,\"RedirectURL\":\"/handler/creditcard.aspx?OrderID=" + NewOrder + "\",\"NewOrderID\":\"" + NewOrder + "\",\"PaymentType\":\"" + PaymentType + "\",\"FailVerification\":\"\"}";
                    }
                    return strreturn;
                }
                else
                {
                    return "{\"success\":false}";
                }

                string InsertSQL1 = string.Format(@"UPDATE orderdetail a, menusizes b
                                                    SET a.`OriginalPrice` = b.`OriginalPrice`
                                                    WHERE a.`SizeID` = b.`ID` AND a.`OrderID` = '{0}'", NewOrder);
                DBHandler.InsertDataForOrder(InsertSQL1);
            }
            catch (Exception ex)
            {
                DBHandler.InsertDataForOrder(string.Format("insert into errorlog(URL, Error,Detail)values('{0}', '{1}', '{2}')", HttpContext.Current.Request.Url, "OrderID = " + NewOrder + " - " + ex.Message, ex.StackTrace));
                return "{\"success\":false}";
            }

           
        }
         
        public static Int32 ISUserExistByFbIDOREmail(string fbID, string email,string name)
        {
            string strEmailWhereClause = "";
            Handler.Process.GetUserCookie();
            string Phone = Handler.Process._Number;

            if (email.Trim() != string.Empty)
            {
                strEmailWhereClause = string.Format(" AND email='{0}'", email);
            }
            string strQuery = string.Format("SELECT * FROM customer where ( fbID = '{0}' {1} )", fbID, strEmailWhereClause,Phone);

            DataTable DT = DBHandler.GetData(strQuery);
            if (DT.Rows.Count > 0)
            {
                Handler.Process._Number = DT.Rows[0]["mobile"].ToString();
                return Convert.ToInt32(DT.Rows[0]["ID"]);
            }
            else
            {
                DataTable DTMobile = DBHandler.GetData("select * from customer where mobile='"+Phone+"' and name like '%"+name+"%'");
                if (DTMobile.Rows.Count > 0)
                {
                    return Convert.ToInt32(DTMobile.Rows[0]["ID"]);
                }
            }

            return 0;
        }

        public static bool ValidateEmail(string email)
        {

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }
         
        /// <summary>
        /// Method to generate random alphanumeric string
        /// </summary>                
        public static string GenerateVerificationCode()
        {
            string VerificationCode = "";
            try
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                VerificationCode = new string(
                    Enumerable.Repeat(chars, 10)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());             
            }
            catch (Exception ex)
            { }
            return VerificationCode;
        }

        /// <summary>
        /// Method to determine if customer need to be verified by PTCL to prevent cash loss
        /// </summary>                
        public static bool CheckCashLoss(string customerMobile)
        {
            bool IsPTCLRequired = true;
            try
            {
                DataTable dtCheckCashLoss = DBHandler.GetData(string.Format("select ID as MyCount from orders where customermobile = '{0}' and OrderAmount >= 2000 and Status = 'Confirmed' UNION SELECT ID as MyCount FROM ordersarchive WHERE customermobile = '{0}' AND OrderAmount >= 2000 AND STATUS = 'Confirmed'", customerMobile));

                if (dtCheckCashLoss.Rows.Count > 0)
                {
                    IsPTCLRequired = false;
                }
            }
            catch (Exception ex)
            {
                DBHandler.InsertData(string.Format("INSERT INTO errorlog (URL, Error, Detail) VALUES ('{0}', '{1}', '{2}')", HttpContext.Current.Request.Url.AbsoluteUri, "Error in CheckCashLoss() customerMobile = " + customerMobile, ex.Message + " - " + ex.StackTrace));
            }
            return IsPTCLRequired;
        }

        [WebMethod]
        public static string ProcessPhoneUnsubscribe(string UnsubscribeType, string UnsubscribeNumber)
        {
            int Insert = 0;

            //replace special characters from phone number
            UnsubscribeNumber = UnsubscribeNumber.ToString().Replace("92-", "0").Replace("-", "").Replace(" ", "").Replace("+", "");

            if (UnsubscribeNumber.Length == 11)
            {
                Insert = DBHandler.InsertData(string.Format("insert into unsubscribe(MobileNumber, UnsubscribeType) values('{0}', '{1}')", UnsubscribeNumber, UnsubscribeType));

                if (Insert > 0)
                {
                    //lblResult.Text = "You are succesfully unsubsceribed";
                }
                else
                {
                    //lblResult.Text = "Sorry!! There was an error while unsubsceribing you. Please try again later.";
                }
            }
            else
            {
                //lblResult.Text = "Sorry!! The provided number is not correct.";
            }

            return Insert > 0 ? "{ \"success\": true}" : "{ \"success\": false}";
        }

        [WebMethod]
        public static string Processsubscribers(string email, string City)
        {
            //check existing email
            DataTable dtSubscribe = DBHandler.GetData("select * from subscribers where email='" + email + "'");
            if (dtSubscribe.Rows.Count > 0)
            {
                return "{ \"success\": false, \"message\" : \"You are already subscibed to our weekly newsletter.\" }";
            }
            //add new email
            string InsertSQL = "Insert into subscribers(email,city,source,channel) values('" + email + "','" + City + "','broadwaypizza.com.pk','Web')";
            int Insert = DBHandler.InsertDataWithoutLogin(InsertSQL);

            //send email to user
            //CodeLibrary.Email.SendMail("auto@broadwaypizza.com.pk", email, "Subscription Successfull!", "Welcome to broadwaypizza! \n\n You have been subscribed to our weekly newsletter", "broadwaypizza!");
            return "{ \"success\": true, \"message\" : \"You have been subscribed to our weekly newsletter.\" }";
        }


        [WebMethod]
        public static string Login(string Email, string Password)
        {
            string message = "";
            if (!checkForSQLInjection(Email) && !checkForSQLInjection(Password))
            {
                //check existing email
                DataTable dtSubscribe = DBHandler.GetData("SELECT * FROM users WHERE email = '" + Email.Replace("'", "''") + "' and password = '" + Cryptography.EncryptMessage(Password.Replace("'", "''")) + "'");
                if (dtSubscribe.Rows.Count > 0)
                {
                    HttpContext.Current.Session["ID"] = dtSubscribe.Rows[0]["ID"].ToString();
                    HttpContext.Current.Session["Type"] = dtSubscribe.Rows[0]["Type"].ToString();
                    message = "{ \"success\": true, \"message\" : \"Login successfull.\" }";
                }
                else
                {
                    message = "{ \"success\": false, \"message\" : \"Invalid phone number or password.\" }";
                }
            }
            return message;
        }

        [WebMethod]
        public static string ProcessNewAccount(string Name, string Number, string Password)
        {
             string message = "";
             if (!checkForSQLInjection(Name) && !checkForSQLInjection(Number) && !checkForSQLInjection(Password))
             {
                 //check existing email
                 DataTable dtSubscribe = DBHandler.GetData("SELECT * FROM customer WHERE mobile = '" + Number.Replace("'", "''") + "'");
                 if (dtSubscribe.Rows.Count > 0)
                 {
                     string VerificationCode = GenerateVerificationCode();

                     message = "{ \"success\": false, \"message\" : \"This number is already registered with us.\" }";
                 }
                 //add new email
                 else
                 {
                     Name = Process.TextCase(Name);

                     string VerificationCode = GenerateVerificationCode();

                     int customerID = Jewar.CodeLibrary.DBHandler.InsertDataWithID("insert into customer(name,mobile,password,verificationcode) values('" + Name.Replace("'", "''") + "','" + Number.Replace("'", "''") + "','" + Password.Replace("'", "''") + "','" + VerificationCode.Replace("'", "''") + "')");

                     HttpContext.Current.Session["CustomerMobile"] = Number.Replace("'", "''");

                     string SMSMessage = string.Format("Your Broadway website verification code is : {0} ", VerificationCode);

                     SMS.SendMessage(Number.Replace("'", "''"), SMSMessage);

                     message = "{ \"success\": true, \"message\" : \"You account is created successfully.\" }";
                 }
             }
             return message;
        }

        [WebMethod]
        public static string ProcessVerificationCode(string VerificationCode)
        {
              string message = "";
              if (!checkForSQLInjection(VerificationCode))
              {
                  //check existing email
                  DataTable dtSubscribe = DBHandler.GetData("SELECT * FROM customer WHERE mobile = '" + HttpContext.Current.Session["CustomerMobile"].ToString().Replace("'", "''") + "' and verificationcode = '" + VerificationCode.Replace("'", "''") + "'");
                  if (dtSubscribe.Rows.Count > 0)
                  {
                      HttpContext.Current.Session["CustomerMobile"] = dtSubscribe.Rows[0]["Mobile"].ToString();

                      int customerID = Jewar.CodeLibrary.DBHandler.InsertDataWithID(string.Format("update customer set iscustomerverified = 1 where mobile = '{0}'", HttpContext.Current.Session["CustomerMobile"].ToString().Replace("'", "''")));


                      message = "{ \"success\": true, \"message\" : \"You account is verified successfully.\" }";
                  }
                  else
                  {
                      message = "{ \"success\": false, \"message\" : \"invalid verification code.\" }";
                  }
                  //add new email           
              }
              return message;
        }


        [WebMethod]
        public static string ResendCode()
        {
            string message = "";

            //check existing email
            DataTable dtSubscribe = DBHandler.GetData("SELECT * FROM customer WHERE mobile = '" + HttpContext.Current.Session["CustomerMobile"].ToString().Replace("'", "''") + "'");
            if (dtSubscribe.Rows.Count > 0)
            {
                string VerificationCode = dtSubscribe.Rows[0]["VerificationCode"].ToString();

                string SMSMessage = string.Format("Your Broadway website verification code is : {0} ", VerificationCode);

                SMS.SendMessage(HttpContext.Current.Session["CustomerMobile"].ToString(), SMSMessage);

                message = "{ \"success\": true, \"message\" : \"You verification code is sent to you number.\" }";
            }
            else
            {
                message = "{ \"success\": false, \"message\" : \"invalid verification code.\" }";
            }
            //add new email           

            return message;
        }

        [WebMethod]
        public static string SendPassword(string Number)
        {
            string message = "";
            if (!checkForSQLInjection(Number))
            {
                //check existing email
                DataTable dtSubscribe = DBHandler.GetData("SELECT * FROM customer WHERE mobile = '" + Number.Replace("'", "''") + "'");
                if (dtSubscribe.Rows.Count > 0)
                {
                    string Password = dtSubscribe.Rows[0]["Password"].ToString();

                    if (Password == "")
                    {

                        Password = GenerateVerificationCode();

                        int customerID = Jewar.CodeLibrary.DBHandler.InsertDataWithID(string.Format("update customer set Password = '{0}' where mobile = '{1}'", Password, Number.Replace("'", "''")));


                    }

                    string SMSMessage = string.Format("Your Broadway website passcode is {0} ", Password);

                    SMS.SendMessage(Number.Replace("'", "''"), SMSMessage);

                    message =  "{ \"success\": true, \"message\" : \"Your password is sent to you number.\" }";
                }
                else
                {
                    message =  "{ \"success\": false, \"message\" : \"invalid number.\" }";
                }
                //add new email           
            }
            return message;
        }


        public static Boolean checkForSQLInjection(string userInput)
        {

            bool isSQLInjection = false;

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

            return isSQLInjection;
        }


    }

    public class SearchItem
    {

        public string Name
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string logo
        {
            get;
            set;
        }

        public SearchItem(string name, string address, string logo)
        {
            this.Name = name;
            this.Address = address;
            this.logo = logo;
        }
    }

    public class MyDataTables
    {
        public DataTable DTmyOrder;
        public DataTable DTmyorderdetailOptions;
    }
}