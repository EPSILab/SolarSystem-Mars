using System.Collections.Generic;

namespace SolarSystem.Mars.Model.Model.Abstract
{
    public interface IReaderLimit<T> : IManager<T>
    {
        T Get(int code);
        IList<T> Get(int indexFirstElement, int numberOfResults);
    }
}