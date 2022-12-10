using System.Collections.Generic;

namespace SolarSystem.Mars.Model.Model.Abstract
{
    public interface IReader<T> : IManager<T>
    {
        T Get(int code);
        IList<T> Get();
    }
}