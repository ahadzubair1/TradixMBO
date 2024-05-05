using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace LMSBackOfficeWebApplication
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            // Set session timeout to 20 minutes
            Session.Timeout = 20;
        }


        /// <summary>
        /// FRAME-SNIFFING PREVENTION
        /// DENY: do not allow any site to frame your application
        /// SAMEORIGIN: only allow same application site to frame
        ///ALLOW-FROM: only allow specific domain to frame your application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_BeginRequest(object sender, EventArgs e)
        {
            //HttpContext.Current.Response.AddHeader("x-frame-options", "SAMEORIGIN");
            HttpContext.Current.Response.AddHeader("x-frame-options", "DENY");

        }



        /// <summary>
        /// SECURITY HEADERS REMOVAL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            HttpContext.Current.Response.Headers.Remove("Server");
            //HttpContext.Current.Response.Headers.Remove("Referrer-Policy");
            HttpContext.Current.Response.Headers.Add("Permission-Policy", "geolocation=(), microphone=(), camera=()");

        }


    }
}