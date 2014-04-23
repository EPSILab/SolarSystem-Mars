using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Abstract;

namespace SolarSystem.Mars.ViewController.Infrastructure.Concrete
{
    public class WebserviceAuthorizeAttribute : AuthorizeAttribute
    {
        #region Fields

        private readonly Role[] _acceptedRoles;

        #endregion


        #region Properties

        [Inject]
        public IAuthProvider AuthProvider { get; set; }

        #endregion


        #region Contructor

        public WebserviceAuthorizeAttribute(params Role[] acceptedRoles)
        {
            _acceptedRoles = acceptedRoles;
        }

        #endregion


        #region Methods

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (AuthProvider.IsSignIn)
            {
                if (!string.IsNullOrWhiteSpace(Roles))
                {
                    var userRole = AuthProvider.LoginViewModel.Role;

                    return _acceptedRoles.Any(r => r == userRole);
                }

                return true;
            }

            return false;
        }

        #endregion
    }
}