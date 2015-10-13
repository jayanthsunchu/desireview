using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace desireview.Helpers
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        public string AccessLevel { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            
            if (httpContext.Request.Cookies["defaultlanguage"] != null)
            {
                if (httpContext.Request.Cookies["defaultlanguage"].Value.ToString() == "Telugu")
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            HttpContext.Current.Response.Redirect("/MoviePageAdmin/NotAuthorized");
        }
    }
}
