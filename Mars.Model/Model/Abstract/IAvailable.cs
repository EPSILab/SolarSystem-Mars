using System.Collections.Generic;

namespace SolarSystem.Mars.Model.Interfaces
{
    public interface IAvailable<T> : IManager<T>
    {
        IList<T> GetAvailables();
    }
}