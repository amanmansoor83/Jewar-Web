using Jewar.CodeLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Broadway_New.Handler
{
    public partial class ITSPay : System.Web.UI.Page
    {
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
                        string Items = "";

                        for (int a = 0; a < dtOrderDetail.Rows.Count; a++)
                        {
                            Items += dtOrderDetail.Rows[0]["ItemName"].ToString() + ","; 
                        }

                        Items = Items.TrimEnd(',');


                        string CheckSum = "ITS-Pay:" + dtOrder.Rows[0]["ID"].ToString() + ":" + Items + ":" + dtOrder.Rows[0]["OrderAmount"].ToString();

                        byte[] ba = Encoding.Default.GetBytes(CheckSum);

                        var hexString = BitConverter.ToString(ba);

                        DataTable DTSMSPortal = DBHandler.GetData("SELECT * FROM taxonomy WHERE `name` = 'ITSPaymentURL'");

                        string request = "";
                        if (DTSMSPortal.Rows.Count > 0)
                        {
                            request = string.Format(DTSMSPortal.Rows[0]["value"].ToString(), "", dtOrder.Rows[0]["ID"].ToString(), Items, dtOrder.Rows[0]["OrderAmount"].ToString() , hexString);
                        }

                        Response.Redirect(request);


                        string b = "abc";
                        // https://stage.itspay.com.pk/?api_key=xxxx&merchant_id=xxx&item=xxx&amount=xxx&Checksum=abcdef1234567890
                    }
                }
            }
            catch (Exception ex)
            { }

        }
    }
}