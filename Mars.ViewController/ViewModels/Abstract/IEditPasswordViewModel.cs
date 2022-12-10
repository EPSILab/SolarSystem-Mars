namespace SolarSystem.Mars.ViewController.ViewModels.Abstract
{
    public interface IEditPasswordViewModel
    {
        string OldPassword { get; set; }
        string NewPassword { get; set; }
        string ConfirmNewPassword { get; set; }
    }
}
