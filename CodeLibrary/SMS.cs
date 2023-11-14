using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Data;
using Jewar.CodeLibrary;

namespace Jewar.CodeLibrary
{
    public class SMS
    {


        /// <summary>
        /// Method to check status of 3rd party service, verify internet access and credit
        /// </summary>
        /// <returns>Status</returns>
        public static bool IsServiceReady()
        {
            //check balance to verify service status and credit
            if (CheckBalance() != 0){return true;}
            else{return false;}
        }

        /// <summary>
        /// Method to check 3rd party SMS service balance.
        /// </summary>
        /// <returns>Balance</returns>
        public static int CheckBalance()
        {
            WebClient client = new WebClient();
            try
            {   client.UseDefaultCredentials = true;
            return int.Parse(client.DownloadString(string.Format(@"http://{0}/api/getbalance.php?username={1}&secret={2}", System.Configuration.ConfigurationManager.AppSettings["SMSServiceSite"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SMSServiceUsername"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SMSServiceSecret"].ToString())));
            }
            catch (WebException) { return 0;}
            finally { client.Dispose(); }
        }

        /// <summary>
        /// Method to validate local phone numbers.
        /// </summary>
        /// <param name="to">One or more Phone Numbers (Cell or Phone)</param>
        /// <returns>Validity</returns>
        public static bool ValidatePhone(string to)
        {
            Regex regex = new Regex(@"(03\d{9})");
            if(regex.IsMatch(to)){ return true;}
            else { return false;}
         }

        /// <summary>
        /// Method to validate SMS content and size.
        /// </summary>
        /// <param name="message">Message (upto 160 characters)</param>
        /// <returns>Validity</returns>
        public static bool ValidateMessage(string message)
        {
            Regex regex = new Regex(@"\w{1,480}");
            if (regex.IsMatch(message)) { return true; }
            else { return false; }
        }

        /// <summary>
        /// Method to send short message using 3rd party SMS service
        /// </summary>
        /// <param name="phone">One ore more Phone Numbers (Cell or Phone)</param>
        /// <param name="message">Message (upto 160 characters)</param>
        /// <returns>Result</returns>
        public static bool SendMessage(string phones, string message)
        {
            bool result = false;
            phones = phones.ToString().Replace("92-", "0").Replace("+92", "0").Replace("0092", "0").Replace("-", "").Replace(" ", "").Replace("+", "");

            if (phones.Substring(0, 1) == "0")
            {
                phones = "92" + phones.Remove(0, 1);

            }

            WebClient client = new WebClient();
            try
            {

                DataTable dtusersbyID = DBHandler.GetData("select name , value from taxonomy where name = 'SMSCheck'");

                if (dtusersbyID.Rows.Count > 0)
                {
                    if (dtusersbyID.Rows[0]["value"].ToString().ToLower() == "true")
                    {
                         
                            client.UseDefaultCredentials = true;
                            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)");
                          
                            string request = "";

                            
                                DataTable DTSMSPortal = DBHandler.GetData("SELECT * FROM taxonomy WHERE `name` = 'SMSAPIURL'");

                                if (DTSMSPortal.Rows.Count > 0)
                                {
                                    request = string.Format(DTSMSPortal.Rows[0]["value"].ToString(), phones, HttpUtility.UrlEncode(message));
                                }
                                else
                                {
                                    request = string.Format(@"http://sms.telecard.com.pk/portal/SMSPortal.Forms?ActionID=ApplicationSendSMS&userid=FCP&password=Welcome1&mobileno={0}&message={1}", System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceUsername"].ToString(), System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceSecret"].ToString(), phones, HttpUtility.UrlEncode(message));
                                }
                           
                            System.IO.Stream response = client.OpenRead(request);
                            System.IO.StreamReader StreamReader = new System.IO.StreamReader(response);
                            string strReadFile = StreamReader.ReadToEnd();
                            if (strReadFile.ToLower().Contains("success") || strReadFile.ToLower().Contains("ok") || strReadFile.ToLower().Contains("message(s) accepted for delivery") || strReadFile.ToLower().Contains("Message accepted for delivery"))
                            {
                                result = true;

                                try
                                {
                                    long a = DBHandler.InsertDataWithIDForOrder(string.Format("insert into logsms(SMS, Phone, Created) values('{0}','{1}','{2}')", message, phones, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")));
                                }
                                catch (Exception eee)
                                { }

                            }

                        return result;
                    }
                }
            }
            catch (Exception ee)
            {
                
                return result;
            }
            finally
            {
                client.Dispose();
            }
            return result;
        }

        /// <summary>
        /// Method to send short message using 3rd party SMS service
        /// </summary>
        /// <param name="phone">One ore more Phone Numbers (Cell or Phone)</param>
        /// <param name="message">Message (upto 160 characters)</param>
        /// <returns>Result</returns>
        public static bool SendMessage(string phones, string message, string forBulk)
        {
            bool result = false;
            phones = phones.ToString().Replace("92-", "0").Replace("-", "").Replace(" ", "").Replace("+", "");
            WebClient client = new WebClient();
            try
            {

                DataTable dtusersbyID = DBHandler.GetData("select name , value from taxonomy where name = 'SMSCheck'");

                if (dtusersbyID.Rows.Count > 0)
                {
                    if (dtusersbyID.Rows[0]["value"].ToString().ToLower() == "true")
                    {
                        if (ValidatePhone(phones) && ValidateMessage(message))
                        {

                            client.UseDefaultCredentials = true;
                            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)");
                            //string request = string.Format(@"http://{0}/api/sendsms.php?username={1}&secret={2}&to={3}&text={4}", Properties.Resources.SMSServiceSite, Properties.Resources.SMSServiceUsername, Properties.Resources.SMSServiceSecret,  phones, HttpUtility.UrlEncode(message));
                            //string request = string.Format(@"http://{0}/api/sendsms.php?username={1}&secret={2}&to={3}&text={4}", System.Configuration.ConfigurationManager.AppSettings["SMSServiceSite"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SMSServiceUsername"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SMSServiceSecret"].ToString(), phones, HttpUtility.UrlEncode(message));

                            string request = "";

                            if (System.Configuration.ConfigurationManager.AppSettings["SMSService"].ToString() == "TeleCard")
                            {
                                //Request for Tele Card
                                //request = string.Format(@"http://smart.telecard.com.pk/portal/SMSPortal.Forms?ActionID=ApplicationSendSMS&userid={0}&password={1}&mobileno={2}&message={3}", System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceUsername"].ToString(), System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceSecret"].ToString(), phones, HttpUtility.UrlEncode(message));
                                //request = string.Format(@"http://203.130.2.30/portal/SMSPortal.Forms?ActionID=ApplicationSendSMS&userid={0}&password={1}&mobileno={2}&message={3}", System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceUsername"].ToString(), System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceSecret"].ToString(), phones, HttpUtility.UrlEncode(message));
                                //request = string.Format(@"http://203.130.2.135/portal/SMSPortal.Forms2?ActionID=ApplicationSendSMS2&userid={0}&password={1}&mobileno={2}&message={3}", System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceUsername"].ToString(), System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceSecret"].ToString(), phones, HttpUtility.UrlEncode(message));
                                //request = string.Format(@"http://sms.telecard.com.pk/portal/SMS?ActionID=ApplicationSendSMS2&userid={0}&password={1}&mobileno={2}&message={3}", System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceUsername"].ToString(), System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceSecret"].ToString(), phones, HttpUtility.UrlEncode(message));
                                //request = string.Format(@"http://sms.telecard.com.pk/portal/SMSPortal.Forms?ActionID=ApplicationSendSMS&userid={0}&password={1}&mobileno={2}&message={3}", System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceUsername"].ToString(), System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceSecret"].ToString(), phones, HttpUtility.UrlEncode(message));
                                // request = string.Format(@"http://203.130.2.135/portal/SMSPortal.Forms2?ActionID=ApplicationSendSMS2&userid={0}&password={1}&mobileno={2}&message={3}", System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceUsername"].ToString(), System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceSecret"].ToString(), phones, HttpUtility.UrlEncode(message));

                                DataTable DTSMSPortal = DBHandler.GetData("SELECT * FROM taxonomy WHERE `name` = 'SMSAPIURLBULK'");

                                if (DTSMSPortal.Rows.Count > 0)
                                {
                                    request = string.Format(DTSMSPortal.Rows[0]["value"].ToString(), phones, HttpUtility.UrlEncode(message));
                                }
                                else
                                {
                                    request = string.Format(@"http://sms.telecard.com.pk/portal/SMSPortal.Forms?ActionID=ApplicationSendSMS&userid=FCP&password=Welcome1&mobileno={0}&message={1}", System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceUsername"].ToString(), System.Configuration.ConfigurationManager.AppSettings["TeleCardSMSServiceSecret"].ToString(), phones, HttpUtility.UrlEncode(message));
                                }
                            }
                            else if (System.Configuration.ConfigurationManager.AppSettings["SMSService"].ToString() == "Ufone")
                            {
                                string check = phones.Substring(0, 1);

                                if (check == "0")
                                {
                                    phones = "92" + phones.Remove(0, 1);
                                }

                                //Request for Ufone
                                request = string.Format(@"http://bsms.ufone.com/bsms_app5/sendapi-0.3.jsp?id={0}&message={1}&shortcode={2}&lang=English&mobilenum={3}&password={4}", System.Configuration.ConfigurationManager.AppSettings["UfoneID"].ToString(), HttpUtility.UrlEncode(message), System.Configuration.ConfigurationManager.AppSettings["UfoneShortCode"].ToString(), phones, System.Configuration.ConfigurationManager.AppSettings["UfonePassword"].ToString());
                            }

                            else
                            {
                                //Request for EOcean
                                request = string.Format(@"http://websms.eocean.pk/api/sendsms.php?username={0}&secret={1}&to={2}&text={3}", System.Configuration.ConfigurationManager.AppSettings["EOceanSMSServiceUsername"].ToString(), System.Configuration.ConfigurationManager.AppSettings["EOceanSMSServiceSecret"].ToString(), phones, HttpUtility.UrlEncode(message));
                            }

                            System.IO.Stream response = client.OpenRead(request);
                            System.IO.StreamReader StreamReader = new System.IO.StreamReader(response);
                            string strReadFile = StreamReader.ReadToEnd();
                            if (strReadFile.ToLower().Contains("success") || strReadFile.ToLower().Contains("ok"))
                            {
                                result = true;
                            }

                            //string response = client.Downloadstring(request.Substring(2));
                            //if (response.Contains("Success")) { result = true; }
                        }
                        return result;
                    }
                }
            }
            catch (Exception ee)
            {

                return result;
            }
            finally
            {
                client.Dispose();
            }
            return result;
        }


        public static string SendMsg(string Phone,string Message)
        {
            
            string strURL = "http://websms.eocean.pk/api/sendsms.php?username=FCPakistan&secret=12345&to="+Phone+"&text="+Message;
            WebResponse objResponse;
            WebRequest objRequest = HttpWebRequest.Create(strURL);
            objResponse = objRequest.GetResponse();
            string strResult = "";
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                strResult = sr.ReadToEnd();
                sr.Close();
            }
            return strResult;
        }

    }
}
