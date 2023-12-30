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

                    if (Request.Url.AbsoluteUri.ToLower().Contains("/index.aspx"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');

                        MyContext.RewritePath("/index");

                        return;
                    }


                    if (Request.Url.AbsoluteUri.ToLower().Contains("/index"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');

                        MyContext.RewritePath("/index.aspx");

                        return;
                    }
                    if (Request.Url.AbsoluteUri.ToLower().Contains("/Agent/Login"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');

                        MyContext.RewritePath("/Agent/Login.aspx");

                        return;
                    }
                    if (Request.Url.AbsoluteUri.ToLower().Contains("/listing"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');

                        MyContext.RewritePath("/listing.aspx");

                        return;
                    }
                    if (Request.Url.AbsoluteUri.ToLower().Contains("/agent/dashboard"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');

                        MyContext.RewritePath("/Agent/Dashboard.aspx");

                        return;
                    }
                    if (Request.Url.AbsoluteUri.ToLower().Contains("/agent/addproperty"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');

                        MyContext.RewritePath("/Agent/AddProperty.aspx");

                        return;
                    }

                    if (Request.Url.AbsoluteUri.ToLower().Contains("/agent/myproperties"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');

                        MyContext.RewritePath("/Agent/MyProperties.aspx");

                        return;
                    }

                    if (Request.Url.AbsoluteUri.ToLower().Contains("/agent/myfavourites"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');

                        MyContext.RewritePath("/Agent/MyFavourites.aspx");

                        return;
                    }

                    if (Request.Url.AbsoluteUri.ToLower().Contains("/agent/reviews"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');

                        MyContext.RewritePath("/Agent/Reviews.aspx");

                        return;
                    }

                    if (Request.Url.AbsoluteUri.ToLower().Contains("/agent/profile"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');

                        MyContext.RewritePath("/Agent/Profile.aspx");

                        return;
                    }

                    if (Request.Url.AbsoluteUri.ToLower().Contains("/agent/message"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');

                        MyContext.RewritePath("/Agent/Message.aspx");

                        return;
                    }

                    if (Request.Url.AbsoluteUri.ToLower().Contains("/agent/login"))
                    {
                        HttpContext MyContext = HttpContext.Current;
                        //string[] Outlet = UrlParams[1].Split('/');

                        MyContext.RewritePath("/Agent/Login.aspx");

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
