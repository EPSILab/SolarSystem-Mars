using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Abstract;
using SolarSystem.Mars.ViewController.ViewModels;

namespace SolarSystem.Mars.ViewController.Infrastructure.Concrete
{
    public class WebserviceAuthProvider : IAuthProvider
    {
        #region Attributes

        private readonly ILogin _membreManager;

        #endregion

        #region Constructor

        public WebserviceAuthProvider(ILogin membreManager)
        {
            _membreManager = membreManager;
        }

        #endregion

        #region Properties

        public LoginViewModel LoginViewModel { get; private set; }

        public bool IsSignIn { get { return (LoginViewModel != null); } }

        #endregion

        #region Methods

        public bool SignIn(LoginViewModel login)
        {
            if (_membreManager.Exists(login.Username, login.PasswordCrypted))
            {
                Membre membreConnecte = _membreManager.Login(login.Username, login.PasswordCrypted);

                if (membreConnecte != null)
                {
                    LoginViewModel = login;
                    return true;
                }
            }

            return false;
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