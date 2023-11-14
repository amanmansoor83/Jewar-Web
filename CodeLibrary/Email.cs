/**
* Copyright (c) 2013, Broadway
* All rights reserved.
* @author Yasir Ahmed <yasir@Broadway.pk>
* @version 1.0.1
*/

using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Configuration.Assemblies;

namespace Jewar.CodeLibrary
{
    public class Email : System.Web.UI.Page
    {
        /// <summary>
        /// Send Email and return flag
        /// How to Use:
        /// bool sent=Email.SendMail("yasir@Broadway.pk", "yasir_914@hotmail.com", "hello", "body", "yasiir";
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public static bool SendMail(string from, string to, string subject, string body, string displayName)
        {
            //string from = ConfigurationSettings.AppSettings["EmailAdministrator"].ToString();
            bool result = false;
            try
            {
                // Configure mail client
                SmtpClient mailClient = null;

                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["MailServer"]) &&
                    !String.IsNullOrEmpty(ConfigurationManager.AppSettings["MailServerPort"]))
                {
                    int port = 0;
                    int.TryParse(ConfigurationManager.AppSettings["MailServerPort"], out port);
                    if (port != 0)
                    {

                        mailClient = new SmtpClient(ConfigurationManager.AppSettings["MailServer"],
                            port);
                    }
                    else
                    {
                        mailClient = new SmtpClient();
                    }

                }
                else
                {
                    mailClient = new SmtpClient();
                }
                mailClient.UseDefaultCredentials = false;
                if (ConfigurationManager.AppSettings["MailAuthentication"].ToLower() == "true")
                {
                    // Set credentials (for SMTP servers that require authentication)            
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["MailUserName"]) && !String.IsNullOrEmpty(ConfigurationManager.AppSettings["MailPassword"]))
                    {
                        mailClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailUserName"], ConfigurationManager.AppSettings["MailPassword"]);
                    }

                    if (ConfigurationManager.AppSettings["EnableSSL"].ToLower() == "true")
                    {
                        mailClient.EnableSsl = true;

                    }
                }
                else
                {
                    // Set Default credentials (for SMTP servers that not require authentication)
                    mailClient.UseDefaultCredentials = true;
                }

                //20 seconds timeout for sneding the mail
                mailClient.Timeout = 20000;
                // Create the mail message

                MailMessage mailMessage = new MailMessage(from, to, subject, body);
                //MailMessage mailMessage = new MailMessage();
                
                MailAddress fromAddress = new MailAddress(from, displayName);
                mailMessage.From = fromAddress;

                //MailAddress toAddress = new MailAddress(to);
                //mailMessage.to = toAddress;

                //mailMessage.Subject = subject;
                //mailMessage.Body = body;
               

                mailMessage.IsBodyHtml = true;                


                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                // Send mail
                mailClient.Send(mailMessage);

                result = true;

            }


            catch (Exception ex)
            {
                 HttpContext.Current.Response.Write("<!--" + ex.Message + "-->");
            }
            return result;
        }

        /// <summary>
        /// Send Email with Attachment List & return flag
        /// How to Use:
        /// List<string> Attachments=new List<string>();
        /// Attachments.Add("logo.jpg");
        /// Attachments.Add("button.gif");
        /// bool sent=Email.SendMail("yasir@Broadway.pk", "yasir_914@hotmail.com", "hello", "body", "yasiir", Attachments);
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="displayName"></param>
        /// <param name="Attachments"></param>
        /// <returns></returns>
        public static bool SendMail(string from, string to, string subject, string body, string displayName,List<string> Attachments)
        {
            //string from = ConfigurationSettings.AppSettings["EmailAdministrator"].ToString();
            bool result = false;
            try
            {
                // Configure mail client
                SmtpClient mailClient = null;

                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["MailServer"]) &&
                    !String.IsNullOrEmpty(ConfigurationManager.AppSettings["MailServerPort"]))
                {
                    int port = 0;
                    int.TryParse(ConfigurationManager.AppSettings["MailServerPort"], out port);
                    if (port != 0)
                    {

                        mailClient = new SmtpClient(ConfigurationManager.AppSettings["MailServer"],
                            port);
                    }
                    else
                    {
                        mailClient = new SmtpClient();
                    }

                }
                else
                {
                    mailClient = new SmtpClient();
                }

                if (ConfigurationManager.AppSettings["MailAuthentication"].ToLower() == "true")
                {
                    // Set credentials (for SMTP servers that require authentication)            
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["MailUserName"]) && !String.IsNullOrEmpty(ConfigurationManager.AppSettings["MailPassword"]))
                    {
                        mailClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailUserName"], ConfigurationManager.AppSettings["MailPassword"]);
                    }

                    if (ConfigurationManager.AppSettings["EnableSSL"].ToLower() == "true")
                    {
                        mailClient.EnableSsl = true;

                    }
                }
                else
                {
                    // Set Default credentials (for SMTP servers that not require authentication)
                    mailClient.UseDefaultCredentials = true;
                }

                //20 seconds timeout for sneding the mail
                mailClient.Timeout = 20000;
                // Create the mail message

                MailMessage mailMessage = new MailMessage(from, to, subject, body);
                //MailMessage mailMessage = new MailMessage();

                MailAddress fromAddress = new MailAddress(from, displayName);
                mailMessage.From = fromAddress;

                //MailAddress toAddress = new MailAddress(to);
                //mailMessage.to = toAddress;

                //mailMessage.Subject = subject;
                //mailMessage.Body = body;


                mailMessage.IsBodyHtml = true;


                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //Adding Attachments
                try
                {
                    foreach (string FileName in Attachments)
                    {                       
                        try
                        {
                            HttpServerUtility hsu = HttpContext.Current.Server;
                            Attachment EmailAttachment = new Attachment(hsu.MapPath(FileName));
                            mailMessage.Attachments.Add(EmailAttachment);                                  
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandling.AddSystemerrorlog(ex.Message);
                        }                        
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandling.AddSystemerrorlog(ex.Message);
                }
                // Send mail
                mailClient.Send(mailMessage);

                result = true;

            }


            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog(ex.Message);
            }
            return result;
        }


    }
}
