using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Jewar_API;

namespace Jewar_API
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        { 

            try
            {
                //Get current path
              string CurrentPath = Request.Path;
                string[] UrlParams = Request.Url.AbsoluteUri.Split(new[] { "AppAPIV3/" }, StringSplitOptions.None);// CurrentPath.Split('/');
                // string[] UrlQueryString = CurrentPath.Split('?');
                string[] UrlQueryString = Request.RawUrl.Split('?');
                if (!CurrentPath.Contains(".aspx"))
                {
                    if (Request.Url.AbsoluteUri.ToLower().Contains("/index"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');



                        MyContext.RewritePath("/index.aspx");

                        
                        
                        return;
                    }
                    if (Request.Url.AbsoluteUri.ToLower().Contains("/agent/dashboard"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');



                        MyContext.RewritePath("/Agent/Dashboard.aspx");



                        return;
                    }

                   
                }
            }
            catch (Exception ee) { }



        }



            void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
