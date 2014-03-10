using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Infrastructure.Abstract;
using SolarSystem.Mars.ViewController.ViewModels;

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

        public LoginViewModel LoginViewModel { get; private set; }

        public bool IsSignIn { get { return (LoginViewModel != null); } }

        public bool Error { get; private set; }

        #endregion

        #region Methods

        public bool SignIn(LoginViewModel login)
        {
            Error = true;

            if (_model.Exists(login.Username, login.PasswordCrypted))
            {
                Member memberConnected = _model.Login(login.Username, login.PasswordCrypted);

                if (memberConnected != null)
                {
                    LoginViewModel = login;
                    Error = false;
                }
            }

            return !Error;
        }

        public bool SignOut()
        {
            if (LoginViewModel == null)
                return false;

            LoginViewModel = null;
            return true;
        }

        #endregion
    }
}