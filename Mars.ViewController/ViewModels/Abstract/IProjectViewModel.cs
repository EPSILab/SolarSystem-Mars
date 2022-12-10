namespace SolarSystem.Mars.ViewController.ViewModels.Abstract
{
    public interface IProjectViewModel
    {
        int Id { get; set; }
        string Name { get; set; }
        int Progression { get; set; }
        int CampusId { get; set; }
        string CampusName { get; set; }
        string ImageRemoteUrl { get; set; }
        string Description { get; set; }
        bool CanUpdate { get; set; }
        bool CanDelete { get; set; }
    }
}
