namespace SolarSystem.Mars.ViewController.ViewModels.Abstract
{
    public interface IMemberViewModel
    {
        int Id { get; set; }
        bool IsActive { get; set; }
        string Username { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string ImageRemoteUrl { get; set; }
        string CityFrom { get; set; }
        string EPSIEmail { get; set; }
        string PersonalEmail { get; set; }
        string PhoneNumber { get; set; }
        int IdCampus { get; set; }
        int IdPromotion { get; set; }
        string Website { get; set; }
        string FacebookUrl { get; set; }
        string TwitterUrl { get; set; }
        string LinkedInUrl { get; set; }
        string ViadeoUrl { get; set; }
        string GitHubUrl { get; set; }
        string Presentation { get; set; }
    }
}
