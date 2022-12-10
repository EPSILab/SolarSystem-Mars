using SolarSystem.Mars.Model.ManagersService;

namespace SolarSystem.Mars.ViewController.ViewModels.Abstract
{
    public interface ILoginViewModel
    {
        string Username { get; set; }
        string PasswordNonCrypted { get; set; }
        Role Role { get; set; }
        string PasswordCrypted { get; }
    }
}
