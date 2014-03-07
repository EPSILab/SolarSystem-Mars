using SolarSystem.Mars.ViewController.ViewModels;

namespace SolarSystem.Mars.ViewController.Infrastructure.Abstract
{
    public interface IAuthProvider
    {
        LoginViewModel LoginViewModel { get; }
        bool IsSignIn { get; }

        bool Error { get; }

        bool SignIn(LoginViewModel login);
        bool SignOut();
    }
}