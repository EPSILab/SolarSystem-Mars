using SolarSystem.Mars.Model.ManagersService;

namespace SolarSystem.Mars.Model.Interfaces
{
    public interface ILogin : IManager<Membre>
    {
        bool Exists(string username, string password);

        bool Exists(string username);

        Membre Login(string username, string password);

        int Register(Membre membre);

        void RequestLostPassword(string username, string email);

        void SetNewPasswordAfterLost(string username, string newPassword, string key);
    }
}