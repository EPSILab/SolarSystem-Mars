using SolarSystem.Mars.Model.ManagersService;

namespace SolarSystem.Mars.ViewController.ViewModels.Abstract
{
    public interface IEditMemberViewModel : IMemberViewModel
    {
        Role Role { get; set; }
        string Status { get; set; }
    }
}
