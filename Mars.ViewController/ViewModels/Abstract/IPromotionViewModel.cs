namespace SolarSystem.Mars.ViewController.ViewModels.Abstract
{
    public interface IPromotionViewModel
    {
        int Id { get; set; }
        string Name { get; set; }
        int GraduationYear { get; set; }
        bool StillPresent { get; set; }
    }
}
