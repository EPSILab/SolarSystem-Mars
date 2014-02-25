using System.Web;
using System.Web.Mvc;
using Ninject;
using SolarSystem.Mars.ViewController.Infrastructure.Abstract;

namespace SolarSystem.Mars.ViewController.Infrastructure.Concrete
{
    public class WebserviceAuthorizeAttribute : AuthorizeAttribute
    {
        [Inject]
        public IAuthProvider AuthProvider { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return AuthProvider.IsSignIn;
        }
    }
}