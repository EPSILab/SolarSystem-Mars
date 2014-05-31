namespace SolarSystem.Mars.ViewController.ViewModels.Abstract
{
    public interface ILinkViewModel
    {
        int Id { get; set; }
        string Label { get; set; }
        string Url { get; set; }
        int? Order { get; set; }
        string ImageRemoteUrl { get; set; }
        string Description { get; set; }
    }
}
