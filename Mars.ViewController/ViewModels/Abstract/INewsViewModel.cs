using System;

namespace SolarSystem.Mars.ViewController.ViewModels.Abstract
{
    public interface INewsViewModel
    {
        int Id { get; set; }
        string Title { get; set; }
        string Url { get; set; }
        DateTime Date { get; set; }
        DateTime Time { get; set; }
        int AuthorId { get; set; }
        string AuthorName { get; set; }
        string ImageRemoteUrl { get; set; }
        string Keywords { get; set; }
        string ShortText { get; set; }
        string Text { get; set; }
        bool IsPublished { get; set; }
        bool CanUpdate { get; set; }
        bool CanDelete { get; set; }
    }
}
