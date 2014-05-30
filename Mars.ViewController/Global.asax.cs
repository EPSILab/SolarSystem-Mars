using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SolarSystem.Mars.ViewController.App_Start;
using SolarSystem.Mars.ViewController.Controllers;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;

namespace SolarSystem.Mars.ViewController
{
    // Remarque : pour obtenir des instructions sur l'activation du mode classique IIS6 ou IIS7, 
    // visitez http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            DependencyResolver.SetResolver(new NinjectDependencyResolver());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            // If the user is not authorised to see this page or access this function, go to to the error page
            if (Context.Response.StatusCode == 401)
            {
                Response.Clear();

                var rd = new RouteData();
                rd.Values["controller"] = "Error";
                rd.Values["action"] = "Unauthorized";

                IController c = new ErrorController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }

            // If the user target an inexistant page, go to the error page
            if (Context.Response.StatusCode == 404)
            {
                Response.Clear();

                var rd = new RouteData();
                rd.Values["controller"] = "Error";
                rd.Values["action"] = "NotFound";

                IController c = new ErrorController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }
        }
    }
}