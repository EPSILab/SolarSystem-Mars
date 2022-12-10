using System.Collections.Generic;

namespace SolarSystem.Mars.Model.Model.Abstract
{
    public interface IAvailable<T> : IManager<T>
    {
        IList<T> GetAvailables();
    }
}