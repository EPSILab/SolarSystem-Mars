using System.Web;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Infrastructure.Abstract;
using SolarSystem.Mars.ViewController.ViewModels.Concrete;

namespace SolarSystem.Mars.ViewController.Infrastructure.Concrete
{
    public class WebserviceAuthProvider : IAuthProvider
    {
        #region Attributes

        private readonly ILogin _model;

        #endregion

        #region Constructor

        public WebserviceAuthProvider(ILogin membreManager)
        {
            _model = membreManager;
        }

        #endregion

        #region Properties

        public LoginViewModel LoginViewModel { get { return HttpContext.Current.Session["Login"] as LoginViewModel; } }

        public bool IsSignIn { get { return (LoginViewModel != null); } }

        public bool Error { get; private set; }

        #endregion

        #region Methods

        public void SignIn(LoginViewModel login)
        {
            if (_model.Exists(login.Username, login.PasswordCrypted))
            {
                Member memberConnected = _model.Login(login.Username, login.PasswordCrypted);

                if (memberConnected != null)
                {
                    login.Role = memberConnected.Role;

                    HttpContext.Current.Session.Add("CurrentUser", memberConnected);
                    HttpContext.Current.Session.Add("Login", login);
                }
            }
        }

        public bool SignOut()
        {
            if (LoginViewModel == null)
                return false;

            HttpContext.Current.Session.Remove("CurrentUser");
            HttpContext.Current.Session.Remove("Login");

            return true;
        }

        #endregion
    }
}