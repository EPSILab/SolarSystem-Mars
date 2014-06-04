using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Abstract;
using SolarSystem.Mars.ViewController.ViewModels.Concrete;

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
            LoginViewModel login = null;
            if (httpContext.Session != null)
                login = httpContext.Session["Login"] as LoginViewModel;

            if (login != null)
            {
                if (_acceptedRoles.Any())
                {
                    Role userRole = login.Role;
                    return _acceptedRoles.Any(r => r == userRole);
                }

                return true;
            }

            return false;
        }

        #endregion
    }
}