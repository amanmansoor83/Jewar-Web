/**
* Copyright (c) 2013, Broadway
* All rights reserved.
* @author Yasir Ahmed <yasir@Broadway.pk>
* @version 1.0.1
*/

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Configuration.Assemblies;


namespace Jewar.CodeLibrary
{
    public class ExceptionHandling
    {
        public enum logErrors
        {
            Information = 0,
            Warning = 1,
            Errors = 2
        }

        
        /// <summary>
        ///Post error in the Windows Error log.
        /// How to Use:
        /// ExceptionHandling.AddSystemerrorlog(ex.Message,OvrLod.logErrors.Warning);                    
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="EventStatus"></param>
        /// <returns></returns>
        public static bool AddSystemerrorlog(string Message, logErrors EventStatus)
        {
            bool IsOperationSuccessful = true;
            string logProjectName = ConfigurationManager.AppSettings["logProjectName"];
            try
            {

                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["IsloggingEnabled"]) && ConfigurationManager.AppSettings["IsloggingEnabled"].ToLower() == "true")
                {

                    if (!String.IsNullOrEmpty(logProjectName))
                    {
                        // Create the source, if it does not already exist.
                        if (!EventLog.SourceExists(logProjectName))
                        {
                            //An event log source should not be created and immediately used.
                            //There is a latency time to enable the source, it should be created
                            //prior to executing the application that uses the source.
                            //Execute this sample a second time to use the new source.
                            EventLog.CreateEventSource(logProjectName, logProjectName + "log");
                        }

                        // Create an EventLog instance and assign its source.
                        EventLog mylog = new EventLog();
                        mylog.Source = logProjectName;

                        // Write an informational entry to the event log.
                        if (EventStatus == logErrors.Information)
                        {
                            mylog.WriteEntry(Message, EventLogEntryType.Information);

                        }
                        else if (EventStatus == logErrors.Warning)
                        {
                            mylog.WriteEntry(Message, EventLogEntryType.Warning);
                        }
                        else if (EventStatus == logErrors.Errors)
                        {
                            mylog.WriteEntry(Message, EventLogEntryType.Error);
                        }
                    }
                }
            }
            catch
            {
                IsOperationSuccessful = false;

            }
            return IsOperationSuccessful;
        }

        /// <summary>
        /// Post error in the Windows Error log.
        /// How to Use:
        /// ExceptionHandling.AddSystemerrorlog(ex.Message);                    
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static bool AddSystemerrorlog(string Message)
        {
            bool IsOperationSuccessful = true;
            string logProjectName = ConfigurationManager.AppSettings["logProjectName"];
            try
            { 
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["IsloggingEnabled"]) && ConfigurationManager.AppSettings["IsloggingEnabled"].ToLower() == "true")
                {

                    if (!String.IsNullOrEmpty(logProjectName))
                    {
                        // Create the source, if it does not already exist.
                        if (!EventLog.SourceExists(logProjectName))
                        {
                            //An event log source should not be created and immediately used.
                            //There is a latency time to enable the source, it should be created
                            //prior to executing the application that uses the source.
                            //Execute this sample a second time to use the new source.
                            EventLog.CreateEventSource(logProjectName, logProjectName + "log");
                        }

                        // Create an EventLog instance and assign its source.
                        EventLog mylog = new EventLog();
                        mylog.Source = logProjectName;

                        // Write an informational entry to the event log.
                                                
                        mylog.WriteEntry(Message, EventLogEntryType.Error);                        
                    }
                }
            }
            catch
            {
                IsOperationSuccessful = false;
            }
            return IsOperationSuccessful;
        }

        

        /// <summary>
        /// Send Email to the Admin when any program raise exceptions and return flag.
        /// How to Use:
        /// ExceptionHandling.SendErrorEmail(exception);                    
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static void SendErrorEmail(Exception ex)
        {
            try
            {
                string ErrorMsg = "<b>" + ex.Message + "</b><br>";
                string ErrorDetail = ex.StackTrace+"<br>";
                string ErrorPage = HttpContext.Current.Request.Url.ToString();
                string AdminEmail=ConfigurationSettings.AppSettings["EmailAdministrator"].ToString();
                bool SendMail=Email.SendMail(AdminEmail, AdminEmail, ex.Message, ErrorMsg + ErrorDetail + ErrorPage, "OvrLod");
               // EmailSent = true;
            }
            catch (Exception ee)
            {
                AddSystemerrorlog(ee.Message);
            }
        }



        

    }
}
