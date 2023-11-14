using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jewar.CodeLibrary
{
    public class log
    {
        #region Attributes

        public string ID;
        public string object_ID;
        public string object_type = "";
        public string user_ID;
        public string Remarks = "";
        public string Channel = "";// -- Web / Phone / Mobile
        public string logtype = "";

        #endregion


        #region Methods

        /// <summary>
        /// Insert into log 
        /// </summary>
        /// <param name="objlog"></param>
        /// <returns></returns>
        public int createlog(log objlog)
        {
            //if (objlog.user_ID == null)
            //    objlog.user_ID = "0";

            return DBHandler.InsertDataWithID(
                //string.Format("INSERT INTO `log` (object_ID, object_Type, user_ID, Remarks, Channel, logType) VALUES"+
                //                                    "({0},    '{1}',       {2},   '{3}',   '{4}',   '{5}' )",
                //                    objlog.object_ID, objlog.object_type, objlog.user_ID, objlog.Remarks, objlog.Channel, objlog.logtype));

                    string.Format("INSERT INTO `log` (object_ID, object_Type, user_ID, Remarks, Channel, logType) VALUES" +
                                                        "({0},    '{1}',       {2},   '{3}',   '{4}',   '{5}' )",
                                        objlog.object_ID, objlog.object_type, "IFNULL((SELECT ID FROM users WHERE UserName= 'mobile@broadwaypizza.com'),0)", objlog.Remarks, objlog.Channel, objlog.logtype));

        }

        /// <summary>
        /// Create Error log
        /// </summary>
        /// <param name="strURL"></param>
        /// <param name="strError"></param>
        /// <param name="strDetail"></param>
        /// <returns></returns>
        public static int createerrorlog(string strURL, string strError, string strDetail)
        {
            if (string.IsNullOrEmpty(strURL))
            {
                strURL = HttpContext.Current.Request.Url.ToString();
            }

            var headers = String.Empty;
            foreach (var key in HttpContext.Current.Request.Headers.AllKeys)
            {
                headers += key + "=" + HttpContext.Current.Request.Headers[key] + Environment.NewLine;
            }

            return DBHandler.InsertDataWithID(string.Format("INSERT INTO errorlog(URL, Error, Detail, IP, Headers) VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}')", strURL.Replace("'", "''"), strError.Replace("'", "''"), strDetail.Replace("'", "''"), HttpContext.Current.Request.UserHostAddress, headers));
        }

        #endregion Methods
    }
}