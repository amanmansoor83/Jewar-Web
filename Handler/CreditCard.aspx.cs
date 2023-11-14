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
using Jewar.Handler;

namespace Jewar.Handler
{
    public partial class CreditCard : System.Web.UI.Page
    {
        //public string access_key = "2f32ce50f35d32959b98e6c5cbc74141";                        //Test Credentials
        //public string profile_id = "DDC7F5A8-DCC8-448B-829E-FFAF561216A5";                    //Test Credentials
        public string access_key = "46c16faec7433b40b8b219d16e189f82";                          //Live Credentials
        public string profile_id = "0EDDB61F-C726-40C9-93BE-A4C7C633D03D";                      //Live Credentials
        public string transaction_uuid = "";
        public string NewGUID = "";
        public string bill_to_address_country = "PK";
        
        public string signed_field_names = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency,bill_to_address_country,bill_to_phone,bill_to_address_line1,bill_to_address_city,bill_to_email,bill_to_forename,bill_to_surname,customer_ip_address,consumer_id,merchant_defined_data1,device_fingerprint_id,merchant_defined_data1,merchant_defined_data2,merchant_defined_data3,merchant_defined_data4,merchant_defined_data5,merchant_defined_data6,merchant_defined_data7,merchant_defined_data8,merchant_defined_data20,ship_to_address_city,ship_to_address_country,ship_to_address_line1,ship_to_address_postal_code,ship_to_address_state,ship_to_forename,ship_to_phone,ship_to_surname,ship_to_email";
        //public string signed_field_names = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency,bill_to_address_country,bill_to_phone,bill_to_address_line1,bill_to_address_city,bill_to_email,bill_to_forename,bill_to_surname,customer_ip_address,consumer_id,merchant_defined_data1,device_fingerprint_id,merchant_defined_data1,merchant_defined_data2,merchant_defined_data3,merchant_defined_data4,merchant_defined_data5,merchant_defined_data6,merchant_defined_data7,merchant_defined_data8,merchant_defined_data20,ship_to_address_city,ship_to_address_country,ship_to_address_line1,ship_to_address_postal_code,ship_to_address_state,ship_to_forename,ship_to_phone,ship_to_surname,ship_to_email,payment_token";
        
        public string unsigned_field_names = "";
        public string signed_date_time = "";
        public string locale = "en";
        
        //public string transaction_type = "sale,create_payment_token";
        public string transaction_type = "sale";//"sale,create_payment_token";
        
        public string reference_number = "";
        public string amount = "";
        public string currency = "PKR";
        public string bill_to_phone = "";
        public string bill_to_address_line1 = "";
        public string bill_to_address_city = "";
        public string bill_to_email = "contact@broadway.org.pk";
        public string bill_to_forename = "";
        public string bill_to_surname = "";

        public string ship_to_address_city = "";
        public string ship_to_address_country = "pk";
        public string ship_to_address_line1 = "";
        public string ship_to_address_postal_code = "";
        public string ship_to_address_state = "";
        public string ship_to_forename = "";
        public string ship_to_phone = "";
        public string ship_to_surname = "";
        public string ship_to_email = "";    

        public string customer_ip_address = "192.168.1.1";
        public string consumer_id = "";
        public string merchant_defined_data1 = "WC";
        public string merchant_defined_data2 = "Yes";
        public string merchant_defined_data3 = "Food";
        public string merchant_defined_data4 = "";    
        public string merchant_defined_data5 = "No";     
        public string merchant_defined_data6 = "Standard";
        public string merchant_defined_data7 = "";
        public string merchant_defined_data8 = "pk";
        public string merchant_defined_data20 = "No";

        //public string payment_token = "C8CEEE0B42FC0BFCE053AF598E0A5F8B";
        public string signature = ""; 

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["OrderID"] != null)
                {
                    DataTable dtOrder = DBHandler.GetData(string.Format("SELECT o.*, c.`IsCreditCardVerified` FROM orders o INNER JOIN customer c ON o.`CustomerID` = c.`ID` WHERE o.id  = '{0}'", Request.QueryString["OrderID"]));

                    DataTable dtOrderDetail = DBHandler.GetData(string.Format("SELECT * FROM orderdetail WHERE orderid = '{0}'", Request.QueryString["OrderID"]));

                    if (dtOrder.Rows.Count > 0)
                    {
                        transaction_uuid = dtOrder.Rows[0]["ID"].ToString();
                        reference_number = dtOrder.Rows[0]["ID"].ToString();
                        NewGUID = Guid.NewGuid().ToString();

                        int c = DBHandler.InsertDataWithID(string.Format("update orders set guid = '{0}' where id ='{1}' ", NewGUID, Request.QueryString["OrderID"]));


                        //signed_date_time = Convert.ToDateTime(dtOrder.Rows[0]["Created"]).ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
                        signed_date_time = Convert.ToDateTime(DateTime.Now).ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
                        amount = dtOrder.Rows[0]["OrderAmount"].ToString();

                        if (dtOrder.Rows[0]["DeliveryAddress"].ToString().Trim().Length >= 60)
                        {
                            bill_to_address_line1 = dtOrder.Rows[0]["DeliveryAddress"].ToString().Substring(0, 59);
                            ship_to_address_line1 = dtOrder.Rows[0]["DeliveryAddress"].ToString().Substring(0, 59);
                        }
                        else
                        {
                            bill_to_address_line1 = dtOrder.Rows[0]["DeliveryAddress"].ToString();
                            ship_to_address_line1 = dtOrder.Rows[0]["DeliveryAddress"].ToString();
                        }

                        bill_to_address_city = dtOrder.Rows[0]["City"].ToString();
                        ship_to_address_city = dtOrder.Rows[0]["City"].ToString();
                        bill_to_forename = dtOrder.Rows[0]["CustomerName"].ToString();
                        ship_to_forename = dtOrder.Rows[0]["CustomerName"].ToString();
                        consumer_id = dtOrder.Rows[0]["CustomerID"].ToString();
                        customer_ip_address = GetUser_IP().Split(',')[0];
                        bill_to_email = dtOrder.Rows[0]["CustomerMobile"].ToString() + "@broadway.com";
                        ship_to_email = dtOrder.Rows[0]["CustomerMobile"].ToString() + "@broadway.com";

                        if (dtOrderDetail.Rows.Count > 0)
                        {
                            int TotalItems = 0;
                            for (int a = 0; a < dtOrderDetail.Rows.Count; a++)
                            {
                                //set item names
                                merchant_defined_data4 += dtOrderDetail.Rows[a]["ItemName"].ToString() + ",";
                                TotalItems += Convert.ToInt32(dtOrderDetail.Rows[a]["Quantity"]);
                            }

                            merchant_defined_data4 = merchant_defined_data4.TrimEnd(',').TrimEnd();

                            //Set the quantity of all the purchased items
                            merchant_defined_data7 = TotalItems.ToString();
                        }

                        //set yes/no if customer is varified/unverified
                        if (Convert.ToBoolean(dtOrder.Rows[0]["IsCreditCardVerified"]))
                        {
                            merchant_defined_data5 = "Yes";
                        }
                        else
                        {
                            merchant_defined_data5 = "No";
                        }
                        bill_to_surname = "Broadway";
                        signature = "";

                        DataTable dtIPAddress = DBHandler.GetData("SELECT * FROM ipaddress ORDER BY RAND() LIMIT 1");

                        if (dtIPAddress.Rows.Count > 0)
                        {
                            customer_ip_address = dtIPAddress.Rows[0]["ipaddress"].ToString();
                        }

                         
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected string GetUser_IP()
        {
            string IPAddress = "";
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return IPAddress = VisitorsIPAddr;
        }


        //[WebMethod]
        //public static string ProcessCreditCard(string TransactionAmount)
        //{
        //    int Insert = 0;
        //    DateTime time = DateTime.Now.ToUniversalTime();
        //    try
        //    { 
        //        string access_key = "dd8557cd34393e059aca6fbb1262204f";
        //        string profile_id = "AB99DC39-E004-4B9A-AAD0-6276FF0ECE21";
        //        string transaction_uuid = System.Guid.NewGuid().ToString();
        //        string bill_to_address_country = "PK";
        //        string signed_field_names = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency,bill_to_address_country";
        //        string unsigned_field_names = "";
        //        string signed_date_time = time.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        //        string locale = "en";
        //        string transaction_type = "authorization";
        //        string reference_number = "1415606289642";
        //        string amount = TransactionAmount;
        //        string currency = "PKR";

        //        IDictionary<string, string> parameters = new Dictionary<string, string>();

        //        parameters.Add("access_key", "dd8557cd34393e059aca6fbb1262204f");
        //        parameters.Add("profile_id","AB99DC39-E004-4B9A-AAD0-6276FF0ECE21");
        //        parameters.Add("transaction_uuid", transaction_uuid);
        //        parameters.Add("bill_to_address_country", "PK");
        //        parameters.Add("signed_field_names", "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency,bill_to_address_country");
        //        parameters.Add("unsigned_field_names", "");
        //        parameters.Add("signed_date_time", time.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
        //        parameters.Add("locale", "en");
        //        parameters.Add("transaction_type", "authorization");
        //        parameters.Add("reference_number", "dd8557cd34393e059aca6fbb1262204f");
        //        parameters.Add("amount", "100");
        //        parameters.Add("currency", "PKR");
        //        parameters.Add("submit", "Submit");
        //        //secureacceptance.Security.sign(parameters);

        //        //Response.Write("<input type=\"hidden\" id=\"signature\" name=\"signature\" value=\"" + secureacceptance.Security.sign(parameters) + "\"/>\n");
        //    }
        //    catch (Exception ex)
        //    { }
          
        //    return Insert > 0 ? "{ \"success\": true}" : "{ \"success\": false}";
        //}


        //public string GenerateRandom()
        //{
        //    Random rand = new Random((int)DateTime.Now.Ticks);
        //    long RandomNumber;
        //    RandomNumber = rand.Next(100000000, 999999999);

        //    return RandomNumber.ToString();
        //}
       
    }

}