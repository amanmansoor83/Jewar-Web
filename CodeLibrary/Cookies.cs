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
using Jewar.Handler;

namespace Jewar.CodeLibrary
{
    public class Cookies
    {
        /// <summary>
        /// Create Cookies
        /// How to Use:
        /// OvrLod.Cookies.CreateCookies("OvrLodCookies","abc",1);
        /// </summary>
        /// <param name="CookieName">Name of the cookie.</param>
        /// <param name="CookieValue">The cookie value.</param>
        /// <param name="iDaysToExpire">The i days to expire.</param>
        public static void CreateCookie(string CookieName, string CookieValue, int iDaysToExpire)
        {
            try
            {
                HttpContext.Current.Response.Cookies.Clear();
                HttpCookie objCookie = new HttpCookie(CookieName);
                HttpContext.Current.Response.Cookies[CookieName].Domain = "Broadway.pk";
                HttpContext.Current.Response.Cookies.Add(objCookie);
                objCookie.Values.Add(CookieName, CookieValue);
                DateTime dtExpiry = DateTime.Now.AddDays(iDaysToExpire);
                HttpContext.Current.Response.Cookies[CookieName].Expires = dtExpiry;

            }
            catch (Exception ee)
            {
                ExceptionHandling.AddSystemerrorlog("Broadway.Cookies.CreateCookie :-" + ee.Message);
            }
           
        }

        public static string  CreateCookie(string CookieName, string CookieValue, int iDaysToExpire, string type)
        {
            try
            {
                HttpContext.Current.Response.Cookies.Clear();
                HttpCookie objCookie = new HttpCookie(CookieName);
                HttpContext.Current.Response.Cookies[CookieName].Domain = "Broadway.pk";
                HttpContext.Current.Response.Cookies.Add(objCookie);
                objCookie.Values.Add(CookieName, CookieValue);
                DateTime dtExpiry = DateTime.Now.AddDays(iDaysToExpire);
                HttpContext.Current.Response.Cookies[CookieName].Expires = dtExpiry;

                return GetCookie(CookieName);
 

            }
            catch (Exception ee)
            {
                ExceptionHandling.AddSystemerrorlog("Broadway.Cookies.CreateCookie :-" + ee.Message);

                return ee.Message;
            }
       }
        /// <summary>
        /// Return Cookies Value
        /// How to Use:
        /// string CookieValue=OvrLod.Cookies.GetCookie("BroadwayCookies");
        /// </summary>
        /// <param name="CookieName"></param>
        /// <returns></returns>
        public static string GetCookie(string CookieName)
        {
            string cookyval = "";
            try
            {
                cookyval = HttpContext.Current.Request.Cookies[CookieName].Value;
            }
            catch (Exception e)
            {
                cookyval = "";
            }
            return cookyval;
        }

        /// <summary>
        /// Delete the Cookie
        /// How to Use:
        /// OvrLod.Cookies.DeleteCookie("BroadwayCookies");
        /// </summary>
        /// <param name="CookieName"></param>
        public static void DeleteCookie(string CookieName)
        {
            try
            {
                HttpCookie myCookie = new HttpCookie(CookieName);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
            catch(Exception ee)
            {
                ExceptionHandling.AddSystemerrorlog("Broadway.Cookies.DeleteCookie :-" + ee.Message);
            }
        }

        public static string GetCookieVal(string CookieName)
        {
            string cookyval = "";
            try
            {
                cookyval = HttpContext.Current.Request.Cookies[CookieName].Value;
                cookyval = cookyval.IndexOf("=") > 0 ? cookyval.Split('=')[1] : "";
            }
            catch (Exception e)
            {
                cookyval = "";
            }
            return cookyval;
        }

        public static bool CheckCookie(string City)
        {
            string s = AppDB.GetTaxValue(Cryptography.DecryptMessage("pl4nm!5iutBo5q95XxiPHw=="));
            return s != "" ? true : false;
        }
    }
}
