using SolarSystem.Mars.Model.ManagersService;

namespace SolarSystem.Mars.Model.Interfaces
{
    public interface ILogin : IManager<Member>
    {
        bool Exists(string username, string password);

        bool Exists(string username);

        Member Login(string username, string password);

        int Register(Member member, string newPassword);

        void RequestLostPassword(string username, string email);

        void SetNewPasswordAfterLost(string username, string newPassword, string key);
    }
}