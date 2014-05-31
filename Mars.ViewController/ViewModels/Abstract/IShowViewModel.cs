using System;

namespace SolarSystem.Mars.ViewController.ViewModels.Abstract
{
    public interface IShowViewModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string Url { get; set; }
        DateTime StartDate { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndDate { get; set; }
        DateTime EndTime { get; set; }
        string Place { get; set; }
        string ImageRemoteUrl { get; set; }
        string Description { get; set; }
        bool IsPublished { get; set; }
        bool CanUpdate { get; set; }
        bool CanDelete { get; set; }
    }
}
