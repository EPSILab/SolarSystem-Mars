using System;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Abstract;

namespace SolarSystem.Mars.ViewController.Infrastructure.Concrete
{
    public class WebserviceAuthorizeAttribute : AuthorizeAttribute
    {
        [Inject]
        public IAuthProvider AuthProvider { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (AuthProvider.IsSignIn)
            {
                if (!string.IsNullOrWhiteSpace(Roles))
                {
                    var userRole = AuthProvider.LoginViewModel.Role;
                    var neededRole = (Role)Enum.Parse(typeof(Role), Roles);

                    return userRole >= neededRole;
                }

                return true;
            }

            return false;
        }
    }
}