using System.Collections.Generic;

namespace SolarSystem.Mars.Model.Interfaces
{
    public interface IReaderFilters<T, in TService> : IReader<T>
    {
        IList<T> Get(TService filter1);
    }
}