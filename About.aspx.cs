using Jewar.CodeLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Threading.Tasks;
using System.Configuration;

namespace Jewar_API
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SendNotification();


            //string input = "This is a sample sentence with words.";

            //// Split the string by the word "is"
            //string[] parts = input.Split(new[] { "is" }, StringSplitOptions.None);

            //foreach (string part in parts)
            //{
            //    Response.Write(part.Trim()); // Trim to remove leading/trailing spaces
            //}

             



            Response.Write (Cryptography.EncryptMessage("data source=bwdb.cznhp2lqqlde.ap-southeast-1.rds.amazonaws.com;user id=jewaruser; password=Abcd1234+; database=jewar ; pooling=true;Integrated Security=True")); 


            //// Get the client's IP address
            //string clientIpAddress = GetClientIpAddress();

            //// Now, you can use the clientIpAddress as needed
            //Response.Write($"Client IP Address: {clientIpAddress}");


        }

        private string GetClientIpAddress()
        {
            string ipAddress = String.Empty;

            // Check for the client IP address in different server variables
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
            {
                ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            return ipAddress;
        }


        public void SendNotification()
        {
            try
            {
              


            }

            catch (Exception ex)
            {
                Response.Write(ex.Message + " " + ex.StackTrace);

            }
        }

    }
}