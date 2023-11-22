using System.Web;
using System.Web.Mvc;

namespace Mid.Auth
{
    // Custom authorization attribute named "Logged" that inherits from AuthorizeAttribute
    public class Logged : AuthorizeAttribute
    {
        // Override method to perform custom authorization logic
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            return httpContext.Session["UserEmail"] != null && httpContext.Session["Password"] != null;
        }

 

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Redirect to the Login action in the Signup controller if unauthorized
            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
            {
                controller = "Signup",
                action = "Login"
            }));
        }
    }
}
