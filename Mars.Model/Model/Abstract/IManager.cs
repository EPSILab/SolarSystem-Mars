namespace SolarSystem.Mars.Model.Model.Abstract
{
    public interface IManager<in T>
    {
        int Add(T element, string username, string password);

        void Edit(T element, string username, string password);

        void Delete(int code, string username, string password);
    }
}