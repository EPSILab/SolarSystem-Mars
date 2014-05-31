namespace SolarSystem.Mars.ViewController.ViewModels.Abstract
{
    public interface IResetPasswordViewModel : IPasswordViewModel
    {
        string Username { get; set; }
        string Key { get; set; }
    }
}
