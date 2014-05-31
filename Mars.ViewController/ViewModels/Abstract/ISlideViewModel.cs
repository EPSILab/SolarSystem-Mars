namespace SolarSystem.Mars.ViewController.ViewModels.Abstract
{
    public interface ISlideViewModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string Url { get; set; }
        string ImageRemoteUrl { get; set; }
        string Presentation { get; set; }
        bool IsPublished { get; set; }
    }
}
