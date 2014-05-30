using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// GET: /Error/
        /// GET: /Error/Index
        /// Basic error page
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: /Error/Unauthorized
        /// 401 error page
        /// </summary>
        /// <returns></returns>
        public ViewResult Unauthorized()
        {
            Response.StatusCode = 401;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        /// <summary>
        /// GET: /Error/NotFound
        /// 404 error page
        /// </summary>
        /// <returns></returns>
        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true; 
            return View();
        }
    }
}
